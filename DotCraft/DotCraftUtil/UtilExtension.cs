using System;

namespace DotCraftUtil
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

        public static int GetJavaHashCode(this string str)
        {
            int h = 0;
            
            for (int i = 0; i < str.Length; i++) {
                h = 31*h + str[i];
            }

            return h;
        }
    }
}