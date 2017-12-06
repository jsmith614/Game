using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GameEngine2017
{
    public static class Utility
    {
        private static unsafe int FloatToInt32Bits(float f)
        {
            return *((int*)&f);
        }

        public static bool FloatIsEqual(float a, float b)
        {
            int maxDeltaBits = 6;
            int aInt = FloatToInt32Bits(a);
            if (aInt < 0)
                aInt = Int32.MinValue - aInt;

            int bInt = FloatToInt32Bits(b);
            if (bInt < 0)
                bInt = Int32.MinValue - bInt;

            int intDiff = Math.Abs(aInt - bInt);
            return intDiff <= (1 << maxDeltaBits);
        }

        public static List<string> GetEnumNameList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(t => t.ToString()).ToList();
        }

        public static MemoryStream SerializeToStream(MapIOObject ioobject)
        {
            MemoryStream stream = new MemoryStream();
            var formatter = new DataContractSerializer(typeof(MapIOObject));
            formatter.WriteObject(stream, ioobject);
            return stream;
        }

        public static MapIOObject DeserializeFromStream(MemoryStream stream)
        {
            var formatter = new DataContractSerializer(typeof(MapIOObject));
            stream.Seek(0, SeekOrigin.Begin);
            var ioobject = formatter.ReadObject(stream) as MapIOObject;
            return ioobject;
        }

        public static List<VertexPositionNormalTexture> GetVertsForBoundingBox(Vector2 position, float width, float height)
        {
            var verts = new List<VertexPositionNormalTexture>();

            //var rect = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
            
            //verts.Add(new VertexPositionNormalTexture(new Vector2(rect.Top, rect.Left), Vector2.Normalize(new Vector2(rect.Top, rect.Left)),);
            //verts.Add(new VertexPositionNormalTexture(rect.Top, rect.Right));
            //verts.Add(new VertexPositionNormalTexture(rect.Bottom, rect.Right));
            //verts.Add(new VertexPositionNormalTexture(rect.Top, rect.Left));
            //verts.Add(new VertexPositionNormalTexture(rect.Bottom, rect.Left));
            //verts.Add(new VertexPositionNormalTexture(rect.Bottom, rect.Right));

            return verts;
        }
    }
}
