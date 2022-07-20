

namespace ModelSMP.CoordsSupportEntities
{
    class Point
    {
        public float X { get; }
        public float Y { get; } 

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float GetDistance(Point destinyPoint)
        {
            float distance = 0;
            float x, y;
            x = destinyPoint.X - X;
            y = destinyPoint.Y - Y;
            distance = (float)Math.Sqrt(x * x + y * y);
            return distance;
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator /(Point p1, int divider)
        {
            return new Point(p1.X / divider, p1.Y / divider);
        }
    }
}
