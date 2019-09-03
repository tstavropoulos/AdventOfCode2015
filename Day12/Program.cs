using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day12
{
    class Program
    {
        private const string inputFile = @"../../../../input12.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Day 12");
            Console.WriteLine("Star 1");
            Console.WriteLine();

            JSONObject document;
            using (StreamReader reader = new StreamReader(inputFile))
            {
                document = new JSONObject(reader);
            }

            Console.WriteLine($"The sum of the numbers is: {document.TotalValues()}");

            Console.WriteLine();
            Console.WriteLine("Star 2");
            Console.WriteLine();

            Console.WriteLine($"The sum of the redless numbers is: {document.TotalValuesRedless()}");

            Console.WriteLine();
            Console.ReadKey();
        }

        public abstract class JSONBase
        {
            public abstract int TotalValues();
            public abstract int TotalValuesRedless();

            public static JSONBase ParseNext(StreamReader reader)
            {
                if (reader.EndOfStream)
                {
                    return null;
                }

                char next = (char)reader.Peek();

                //No whitespace present at all
                //while (char.IsWhiteSpace(next))
                //{
                //    reader.Read();
                //    next = (char)reader.Peek();
                //}

                switch (next)
                {
                    case '{': return new JSONObject(reader);
                    case '[': return new JSONArray(reader);
                    case '"': return new JSONString(reader);
                    default: return new JSONInt(reader);
                }
            }

            protected static string ReadString(StreamReader reader)
            {
                StringBuilder builder = new StringBuilder();

                if (reader.Read() != '"')
                {
                    throw new Exception();
                }

                char next;
                while (true)
                {
                    next = (char)reader.Read();

                    switch (next)
                    {
                        case '"':
                            return builder.ToString();

                        case '\\':
                            builder.Append((char)reader.Read());
                            break;

                        default:
                            builder.Append(next);
                            break;
                    }
                }
            }

            protected static int ReadInt(StreamReader reader)
            {
                StringBuilder builder = new StringBuilder();

                char next = (char)reader.Peek();
                while (char.IsDigit(next) || next == '-')
                {
                    builder.Append(next);

                    reader.Read();
                    next = (char)reader.Peek();
                }

                return int.Parse(builder.ToString());
            }
        }

        public class JSONObject : JSONBase
        {
            public readonly Dictionary<string, JSONBase> values = new Dictionary<string, JSONBase>();

            public JSONObject() { }

            public JSONObject(StreamReader objectData)
            {
                //Parse
                if (objectData.Read() != '{')
                {
                    throw new Exception();
                }

                while (true)
                {
                    if (objectData.Peek() == '}')
                    {
                        objectData.Read();
                        break;
                    }

                    string key = ReadString(objectData);

                    if (objectData.Read() != ':')
                    {
                        throw new Exception();
                    }

                    JSONBase value = ParseNext(objectData);

                    values[key] = value;

                    if (objectData.Peek() == ',')
                    {
                        objectData.Read();
                    }
                    else if (objectData.Peek() != '}')
                    {
                        throw new Exception();
                    }
                }
            }

            private bool HasRed()
            {
                foreach (JSONBase baseVal in values.Values)
                {
                    if (baseVal is JSONString stringVal)
                    {
                        if (stringVal.value == "red")
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public override int TotalValues() => values.Values.Select(x => x.TotalValues()).Sum();
            public override int TotalValuesRedless() => HasRed() ? 0 : values.Values.Select(x => x.TotalValuesRedless()).Sum();
        }

        public class JSONArray : JSONBase
        {
            public readonly List<JSONBase> values = new List<JSONBase>();

            public JSONArray() { }

            public JSONArray(StreamReader arrayData)
            {
                //Parse
                if (arrayData.Read() != '[')
                {
                    throw new Exception();
                }

                while (true)
                {
                    if (arrayData.Peek() == ']')
                    {
                        arrayData.Read();
                        break;
                    }

                    JSONBase value = ParseNext(arrayData);

                    values.Add(value);

                    if (arrayData.Peek() == ',')
                    {
                        arrayData.Read();
                    }
                    else if (arrayData.Peek() != ']')
                    {
                        throw new Exception();
                    }
                }
            }

            public override int TotalValues() => values.Select(x => x.TotalValues()).Sum();
            public override int TotalValuesRedless() => values.Select(x => x.TotalValuesRedless()).Sum();
        }

        public class JSONInt : JSONBase
        {
            public readonly int value;

            public JSONInt(int value) => this.value = value;

            public JSONInt(StreamReader intData) => value = ReadInt(intData);

            public override int TotalValues() => value;
            public override int TotalValuesRedless() => value;
        }

        public class JSONString : JSONBase
        {
            public readonly string value;

            public JSONString(string value) => this.value = value;

            public JSONString(StreamReader stringData) => value = ReadString(stringData);

            public override int TotalValues() => 0;
            public override int TotalValuesRedless() => 0;
        }

    }
}
