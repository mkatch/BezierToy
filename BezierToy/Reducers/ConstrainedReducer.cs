using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    abstract partial class ConstrainedReducer : Reducer
    {
        private int continuityClassAt0 = 0;
        public int ContinuityClassAt0
        {
            get { return continuityClassAt0; }
            set
            {
                if (continuityClassAt0 != value)
                {
                    continuityClassAt0 = value;
                    OnPropertyChanged("ContinuityClassAt0");
                }
            }
        }

        private int continuityClassAt1 = 0;
        public int ContinuityClassAt1
        {
            get { return continuityClassAt1; }
            set
            {
                if (continuityClassAt1 != value)
                {
                    continuityClassAt1 = value;
                    OnPropertyChanged("ContinuityClassAt1");
                }
            }
        }

        public override bool Reduce(Vector2D[] V, Vector2D[] W)
        {
            int n = V.Length - 1;
            int m = W.Length - 1;
            int k = ContinuityClassAt0 + 1;
            int l = ContinuityClassAt1 + 1;

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
            // We are looking for control points
            //
            //      W[i], i = 0, 1, ..., k - 1, m - l + 1, m - l + 2, ..., m
            //
            // of a curve of degree <= m
            //
            //                         m
            //                      ,-----,
            //                       \           m
            //                   w =  )    W[i] B  .
            //                       /           i
            //                      '-----'
            //                       i = 0
            //
            // Satisfying
            //
            //         (i)     (i)
            //        w (0) = v (0)  for  i = 0, 1, ..., k - 1,
            //
            //         (i)     (i)
            //        w (1) = v (1)  for  i = m - l + 1, m - l + 2, ..., m.
            //
            // This is possible because these conditions are not dependent on
            // the points W[k], W[k + 1], ..., W[m - l].

            if (k + l > m + 1) return false;

            // W[i] for i = 0, 1, ..., k - 1 can be computed by evaluating
            //
            //                                i
            //                (n - i + 1)  ,-----,
            //                           i  \        i + j / i \
            //         W[i] = ------------   )   (-1)      |   | V[j]
            //                (m - i + 1)   /              \ j /
            //                           i '-----'
            //                              j = 0
            //                i - 1
            //               ,-----,
            //                \       i + j / i \
            //              -  )   (-1)     |   | W[j]
            //                /             \ j /
            //               '-----'
            //                j = 0

            float p = 1F;
            for (int i = 0; i < k; ++i)
            {
                int b = (i % 2 == 0) ? 1 : -1;
                for (int j = 0; j <= i; ++j)
                {
                    W[i] += b * V[j]; // We assume W[i] = 0 initially.
                    b *= j - i;
                    b /= j + 1;
                }

                W[i] *= p;
                p *= (float)(n - i) / (float)(m - i);

                b = (i % 2 == 0) ? 1 : -1;
                for (int j = 0; j < i; ++j)
                {
                    W[i] -= b * W[j];
                    b *= j - i;
                    b /= j + 1;
                }
            }

            // Similarily W[m - i]'s are computed by evaluating
            //
            //                                i
            //                (n - i + 1)  ,-----,
            //                           i  \        j / i \
            //     W[m - i] = ------------   )   (-1)  |   | V[n - i + j]
            //                (m - i + 1)   /          \ j /
            //                           i '-----'
            //                              j = 0
            //                i - 1
            //               ,-----, 
            //                \        j / i \
            //              -  )   (-1)  |   | W[m - i + j]
            //                /          \ j /
            //               '-----'
            //                j = 1
            //

            p = 1F;
            for (int i = 0; i < l; ++i)
            {
                int b = 1;
                for (int j = 0; j <= i; ++j)
                {
                    W[m - i] += b * V[n - i + j]; // We assume W[m - i] = 0
                    b *= j - i;                   // initially.
                    b /= j + 1;
                }

                W[m - i] *= p;
                p *= (float)(n - i) / (float)(m - i);

                b = -i;
                for (int j = 1; j <= i; ++j)
                {
                    W[m - i] -= b * W[m - i + j];
                    b *= j - i;
                    b /= j + 1;
                }
            }

            // Now call the child class' ReduceInner method to determine the
            // remaining control points.

            return ReduceInner(V, W, k, l);
        }

        abstract protected bool ReduceInner(Vector2D[] basePoints,
            Vector2D[] reducedPoints, int k, int l);
    }
}
