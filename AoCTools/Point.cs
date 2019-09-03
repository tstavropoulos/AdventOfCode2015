using System;

namespace AoCTools
{
    public readonly struct Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Point((int x, int y) point) => new Point(point.x, point.y);
        public static implicit operator (int x, int y)(Point point) => (point.x, point.y);

        public override bool Equals(object obj)
        {
            if (!(obj is Point other))
            {
                return false;
            }

            return x == other.x && y == other.y;
        }

        public override int GetHashCode() => HashCode.Combine(x, y);
        public override string ToString() => $"({x}, {y})";

        public static bool operator ==(in Point lhs, in Point rhs) =>
            lhs.x == rhs.x && lhs.y == rhs.y;

        public static bool operator !=(in Point lhs, in Point rhs) =>
            lhs.x != rhs.x || lhs.y != rhs.y;

        public static Point operator +(in Point lhs, in Point rhs) =>
            new Point(lhs.x + rhs.x, lhs.y + rhs.y);
        public static Point operator -(in Point lhs, in Point rhs) =>
            new Point(lhs.x - rhs.x, lhs.y - rhs.y);

        public int Length => Math.Abs(x) + Math.Abs(y);
    }
}
