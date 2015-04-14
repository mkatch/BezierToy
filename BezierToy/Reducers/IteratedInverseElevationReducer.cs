using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    class IteratedInverseElevationReducer : IteratedReducer
    {
        public IteratedInverseElevationReducer()
        {
            base.Label = "Iterated Inverse Elevation";
        }

        public override bool ReduceByOne(Vector2D[] V, Vector2D[] W, int n)
        {
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
            // that is similar to curve v.

            // The idea is to use the fact that *if* v is in fact degree < n
            // curve, then w = v and the control points satisfy
            //
            //                      k             n - k
            //              V[k] = --- W[k - 1] + ----- W[k] .
            //                      n               n
            //
            // Our approach is to compute W[k]'s by naively applying
            //
            //                      n            k
            //             W[k] = ----- V[k] - ----- W[k - 1]
            //                    n - k        n - k
            //
            // for k from 0 to about n / 2 and
            //
            //                          n         n - k
            //              W[k - 1] = --- V[k] - ----- W[k]
            //                          k           k
            //
            // for k from n down to about n / 2, and we take average of those
            // two solutions in the middle if n - 1 is even.

            if (n == 1)
                W[0] = 0.5F * (V[0] + V[1]);
            else if (n == 2)
            {
                W[0] = 0.5F * (V[0] + V[1]);
                W[1] = 0.5F * (V[1] + V[2]);
            }
            else if (n > 2)
            {
                int c1 = (n - 1) / 2;
                W[0] = V[0];
                for (int k = 1; k < c1; ++k)
                    W[k]
                        = (double)(n) / (double)(n - k) * V[k]
                        - (double)(k) / (double)(n - k) * W[k - 1];
                Vector2D centerPoint1
                    = (double)(n) / (double)(n - c1) * V[c1]
                    - (double)(c1) / (double)(n - c1) * W[c1 - 1];

                int c2 = n - 1 - c1;
                W[n - 1] = V[n];
                for (int k = n - 2; k > c2; --k)
                    W[k]
                        = (double)(n) / (double)(k + 1) * V[k + 1]
                        - (double)(n - k - 1) / (double)(k + 1) * W[k + 1];
                Vector2D centerPoint2
                    = (double)(n) / (double)(c2 + 1) * V[c2 + 1]
                    - (double)(n - c2 - 1) / (double)(c2 + 1) * W[c2 + 1];

                if (c1 == c2)
                    W[c1] = 0.5F * (centerPoint1 + centerPoint2);
                else
                {
                    W[c1] = centerPoint1;
                    W[c2] = centerPoint2;
                }
            }

            return true;
        }
    }
}
