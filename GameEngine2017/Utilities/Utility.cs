using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine2017.Utilities
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
    }
}
