using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    public static class MyMath
    {
        public static double Power(double t, int n)
        {
            if (n <= 0) return 1F;
            if (n == 1) return t;
            double even = t;
            double odd = 1F;
            while (n > 1)
            {
                if (n % 2 == 0)
                {
                    even *= even;
                    n /= 2;
                }
                else
                {
                    odd *= even;
                    --n;
                }
            }
            return even * odd;
        }

        public static int Poch(int a, int n)
        {
            int p = 1;
            for (int i = 0; i < n; ++i)
                p *= a + i;
            return p;
        }

        public static int Fact(int n)
        {
            int f = 1;
            while (n > 1)
            {
                f *= n;
                --n;
            }
            return f;
        }
    }
}
