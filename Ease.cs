using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode
{
    public static class Ease
    {
        public static float QuadIn(float x)
        {
            return 1 - ((1 - x) * (1 - x));
        }

        public static float QuadOut(float x)
        {
            return x * x;
        }

        public static float QuadInOut(float x)
        {
            return x < 0.5 ? 2 * x * x : 1 - ((-2 * x + 2) * (-2 * x + 2)) / 2;
        }
    }
}
