

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
    }
}
