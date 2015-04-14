using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    class IteratedOptimalReducer : IteratedReducer
    {
        public IteratedOptimalReducer()
        {
            Label = "Iterated Optimal";
        }

        public override bool ReduceByOne(Vector2D[] V, Vector2D[] W, int n)
        {
            if (n == 0) return true;

            // We have curve of degree <= n
            //
            //                         n
            //                      ,-----,
            //                       \           n
            //                   v =  )    V[k] B  .
            //                       /           k
            //                      '-----'
            //                       k = 0
            //
            // We are looking for control points W[k], k = 0, 1, ..., n - 1
            // of degree <= n - 1 curve w,
            //
            //                         m
            //                      ,-----,
            //                       \           m
            //                   w =  )    W[k] B  ,
            //                       /           k
            //                      '-----'
            //                       k = 0
            //
            // that is the optimal approximation of v.

            // It is clear that the difference v - w is degree n Chebyshev
            // polynomial -- scaled if necessary and possibly 0. We therefore
            // compute a control point A which assures that
            //
            //                n
            //             ,-----,
            //              \           n
            //               )    V[k] B (t)  -  A T (2t - 1)              (1)
            //              /           k           n
            //             '-----'
            //              k = 0
            //
            // is a polynomial of at most degree n - 1. It can be evaluated the
            // following way:
            //
            //                             n
            //                        ,-----,
            //               -(2n - 1) \         n - k / n \
            //          A = 2           )    (-1)      |   | V[k].
            //                         /               \ k /
            //                        '-----'
            //                         k = 0

            int b = (n % 2 == 0) ? 1 : -1;
            Vector2D A = new Vector2D(0, 0);
            for (int k = 0; k <= n; ++k)
            {
                A += (double) b * V[k];
                b *= k - n;
                b /= k + 1;
            }
            A /= 1L << (2 * n - 1);

            // We now compute the control points C[k] of the curve (1) which are
            // given by
            //
            //                              n - k   / 2n \ / n \-1
            //            C[k] = V[k] - (-1)      A |    | |   |  .
            //                                      \ 2k / \ k /
            //
            // So save up memory, we store C[k]'s in array V.

            double c = (n % 2 == 0) ? 1 : -1;
            for (int k = 0; k <= n; ++k)
            {
                V[k] -= c * A;
                c *= 2 * k - 2 * n + 1;
                c /= 2 * k + 1;
            }

            // We know that the Bezier curve with control points C[k] has in
            // fact degree n - 1, so we perform degree reduction to find W[k].

            W[0] = V[0];
            W[n - 1] = V[n];
            for (int k = 1; k <= n - k - 1; ++k)
            {
                W[k]
                    = (double)(n) / (double)(n - k) * V[k]
                    - (double)(k) / (double)(n - k) * W[k - 1];
                W[n - k - 1]
                    = (double)(n) / (double)(n - k) * V[n - k]
                    - (double)(k) / (double)(n - k) * W[n - k];
            }

            return true;
        }
    }
}
