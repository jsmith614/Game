using Microsoft.Xna.Framework;
using System;

namespace GameEngine2017
{
    public static class GameMath
    {
        public static bool PointInCircle(Vector2 point, Vector2 circle, float radius)
        {
            var dx = Math.Abs(point.X - circle.X);
            var dy = Math.Abs(point.Y - circle.Y);

            return ((dx + dy) <= radius);
        }

        public static bool PointInRect(Vector2 point, Vector2 rect, float width, float height)
        {
            if (point.X > rect.X &&
                point.X < (rect.X + width) &&
                point.Y > rect.Y &&
                point.Y < (rect.Y + height))
            {
                return true;
            }

            return false;
        }
    }
}
