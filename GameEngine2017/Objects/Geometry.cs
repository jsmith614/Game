using Microsoft.Xna.Framework;
using RectangleFLib;
using System;
using System.Collections.Generic;

namespace GameEngine2017
{
    [Serializable]
    public enum ShapeType
    {
        Circle,
        Rectangle
    }

    [Serializable]
    public class Shape
    {
        public int ID { get; set; }

        public Vector2 Position { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Radius { get; set; }

        public ShapeType Type { get; set; }

        public RectangleF GetRect()
        {
            RectangleF shapeRect;
            if (Type == ShapeType.Rectangle)
            {
                shapeRect = new RectangleF(Position.X, Position.Y, Width, Height);
            }
            else
            {
                shapeRect = new RectangleF((Position.X - Radius), (Position.Y - Radius), (Radius * 2), (Radius * 2));
            }
            return shapeRect;
        }
    }

    [Serializable]
    public class Handle
    {
        public Shape Shape { get; set; }

        public float Radius { get; set; }

        public Vector2 Position
        {
            get
            {
                if (Shape.Type == ShapeType.Circle)
                {
                    return new Vector2(Shape.Position.X + Shape.Radius, Shape.Position.Y);
                }
                else // if (Shape.Type == ShapeType.Rectangle)
                {
                    return new Vector2(Shape.Position.X + (Shape.Width), Shape.Position.Y + (Shape.Height));
                }
            }
        }
    }
    
    [Serializable]
    public class MapIOObject
    {
        public int CurrentID { get; set; }

        public string MapTextureName { get; set; }

        public List<Handle> Handles { get; set; }
    }
}
