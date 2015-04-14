using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    class HermiteReducer : ConstrainedReducer
    {
        public HermiteReducer()
        {
            PropertyChanged +=  base_PropertyChanged;
            RefreshLabel();
        }

        protected override bool ReduceInner(Vector2D[] V, Vector2D[] W, int k, int l)
        {
            int n = V.Length - 1;
            int m = W.Length - 1;

            if (m - k - l + 1 == 0) return true;

            // We have curve of degree <= n
            //
            //                         n
            //                      ,-----,
            //                       \           n
            //                   v =  )    V[i] B  .
            //                       /           i
            //                      '-----'
            //                       i = 0
            //
            // And already computed points
            //
            //      W[i], i = 0, 1, ..., k - 1, m - l + 1, m - l + 2, ..., m
            //
            // of a curve of degree <= m
            //
            //                         m
            //                      ,-----,
            //                       \           m
            //                   w =  )    W[i] B  ,
            //                       /           i
            //                      '-----'
            //                       i = 0
            //
            // that ensure
            //
            //         (i)     (i)
            //        w (0) = v (0)  for  i = 0, 1, ..., k - 1,
            //
            //         (i)     (i)
            //        w (1) = v (1)  for  i = m - l + 1, m - l + 2, ..., m.
            //
            // No matter what the remaining W[i]'s are.

            // Generate nodes x[i], i = 0, 1, ..., m - k - l, where
            //
            //   0 < x[0] < x[1] < ... < x[m - k - l - 1] < x[m - k - l] < 1.

            double[] x = new double[m - k - l + 1];
            for (int i = 0; i <= m - k - l; ++i)
                x[i] = (double)(i + 1) / (double)(m - k - l + 2);

            // Our goal is to construct W[k], W[k + 1], ..., W[m - l + 1], such
            // that w interpolates v in points x[i], i.e. w(x[i]) = v(x[i]).

            // We now compute values y[i] = v(x[i]), i = 0, 1, ..., m - k - l
            // and to save up memory, we store then in array W: W[k + i] = y[i].

            for (int i = 0; i <= m - k - l; ++i)
                W[k + i] = BezierCurve.Eval(V, x[i]);

            // Compute auxiliary values d[i], i = 0, 1, ..., m - k - l defined
            // by recurrence relation
            //
            //              (0)         z[i]
            //             d [i] = ----------------,
            //                         k          l
            //                     x[i] (1 - x[i])
            //
            //                      (j - 1)     (j - 1)
            //             (j)     d     [i] - d     [i - 1]
            //             d [i] = -------------------------
            //                         x[i] - x[i - j]
            //
            // for j = 1, 2, ..., m - k - l and i = j, j + 1, ..., m - k - l,
            // where
            //                    k - 1                  m
            //                   ,-----,              ,-----,
            //                    \          m         \           m
            //      z[i] = y[i] -  )   W[j] B (x[i]) -  )    W[j] B (x[i]).
            //                    /          j         /           j
            //                   '-----'              '-----'
            //                    j = 0            j = m - l + 1
            //
            // which gives
            //
            //
            //           (0)          y[i]
            //          d [i] = ----------------
            //                      k          l
            //                  x[i] (1 - x[i])
            //
            //                  k - 1
            //                 ,-----,                    m - j - l
            //                  \         / m \ (1 - x[i])        
            //                -  )   W[j] |   | -------------------
            //                  /         \ j /         k - j
            //                 '-----'              x[i]
            //                  j = 0
            //
            //                    m
            //                 ,-----,                   j - k
            //                  \         / m \      x[i]
            //                -  )   W[j] |   | -------------------
            //                  /         \ j /           l + j - m
            //                 '-----'          (1 - x[i])
            //              j = m - l + 1
            //
            //                                      (i)
            // And after all that we define d[i] = d [i]. Again, to save up
            // space we store values d[i] in array W: W[k + i] = d[i].

            for (int i = 0; i <= m - k - l; ++i)
            {
                double a = x[i];
                double c = 1F - a;
                double aoc = a / c;
                double coa = c / a;

                W[k + i] /= MyMath.Power(a, k) * MyMath.Power(c, l);

                int b = 1;
                double f = MyMath.Power(c, m - l) / MyMath.Power(a, k);
                for (int j = 0; j < k; ++j)
                {
                    W[k + i] -= W[j] * b * f;
                    b *= m - j;
                    b /= j + 1;
                    f *= aoc;
                }

                b = 1;
                f = MyMath.Power(a, m - k) / MyMath.Power(c, l);
                for (int j = m; j > m - l; --j)
                {
                    W[k + i] -= W[j] * b * f;
                    b *= j;
                    b /= m - j + 1;
                    f *= coa;
                }
            }

            for (int j = 1; j <= m - k - l; ++j)
                for (int i = m - k - l; i >= j; --i)
                    W[k + i] = (W[k + i] - W[k + i - 1]) / (x[i] - x[i - j]);

            // Compute auxiliary values u[i], i = 0, 1, ..., m - k - l, given by
            // recurrence relation for j = 1, 2, ..., m - k - l
            // and i = 0, 1, ..., j:
            //
            //             (0)
            //            u  [0] = d[0],
            //
            //             (j)       i    (j - 1)       
            //            u  [i] =  ---  u     [i - 1] 
            //                       j         
            //
            //                     j - i  (j - 1)          (j)
            //                   + ----- u     [i] + d[i] t [i],
            //                       j
            //               (j)
            // where values t [i] are defined by
            //
            //          (0)
            //         t  [0] = 1,
            //
            //          (j)       i                   (j - 1)
            //         t  [i] =  ---  (1 - x[j - 1]) t     [i - 1]
            //                    j
            //
            //                  j - i           (j - 1)
            //                - ----- x[j - 1] t     [i].
            //                    j

            Vector2D[] u = new Vector2D[m - k - l + 1];
            double[] t = new double[m - k - l + 1];
            u[0] = W[k];
            t[0] = 1F;
            for (int j = 1; j <= m - k - l; ++j)
            {
                double a = x[j - 1];
                double c = 1F - a;
                for (int i = j; i > 0; --i)
                {
                    t[i]
                        = (double)i / (double)j * c * t[i - 1]
                        - (double)(j - i) / (double)j * a * t[i];
                    u[i]
                        = (double)i / (double)j * u[i - 1]
                        + (double)(j - i) / (double)j * u[i]
                        + W[k + j] * t[i];
                }
                t[0] = -a * t[0];
                u[0] = u[0] + W[k + j] * t[0];
            }

            // Finally compute remaining points W[i], i = k, k + 1, ..., m - l,
            // using auxiliary values
            //
            //                             / m - k - l \ / m \-1
            //             W[i] = u[i - k] |           | |   |   
            //                             \   i - k   / \ i /

            double s = 1F;
            for (int i = 0; i < k; ++i)
            {
                s *= i + 1;
                s /= m - i;
            }
            for (int i = k; i <= m - l; ++i)
            {
                W[i] = u[i - k] * s;
                s *= (double)((i + 1) * (m - l - i))
                   / (double)((i - k + 1) * (m - i));
            }

            return true;
        }

        private void base_PropertyChanged(object sender,
            PropertyChangedEventArgs args)
        {
            if (
                    args.PropertyName == "ContinuityClassAt0"
                || args.PropertyName == "ContinuityClassAt1"
            )
                RefreshLabel();
        }

        void RefreshLabel()
        {
            Label = string.Format(
                "Hermite Interpolation ({0}, {1})",
                ContinuityClassAt0,
                ContinuityClassAt1
            );
        }
    }
}
