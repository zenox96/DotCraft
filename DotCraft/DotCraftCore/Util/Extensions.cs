using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCraftCore.nUtil
{
    public static class Extensions
    {
        public static float NextFloat(this Random rand)
        {
            return (float)rand.NextDouble();
        }
    }
}
