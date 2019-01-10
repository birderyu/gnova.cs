using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Core
{
    public static class Equals
    {

        public static bool FloatEquals(float f1, float f2, float tolerance)
        {
            return Math.Abs(f1 - f2) <= Math.Abs(tolerance);
        }

        public static bool DoubleEquals(double d1, double d2, double tolerance)
        {
            return Math.Abs(d1 - d2) <= Math.Abs(tolerance);
        }

    }
}
