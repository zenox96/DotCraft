using System;

namespace DotCraftCore.nUtil
{
    public static class UtilExtension
    {
        public static float NextFloat(this Random rand)
        {
            return (float)(rand.NextDouble());
        }

        public static long NextLong(this Random rand)
        {
            long tmp = rand.Next(Int32.MaxValue);
            tmp = tmp << 32;
            tmp += rand.Next(Int32.MaxValue);
            return tmp;
        }

        public static bool NextBoolean(this Random rand)
        {
            return rand.Next(1) > 0 ? true : false;
        }

        public static double NextGaussian(this Random rand)
        {
            return Math.Sqrt(-2.0 * Math.Log(rand.NextDouble( ))) * Math.Sin(2.0 * Math.PI * rand.NextDouble( ));
        }
    }
}