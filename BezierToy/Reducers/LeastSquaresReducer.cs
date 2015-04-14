using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    class LeastSquaresReducer : ConstrainedReducer
    {
        // This reducer uses auxiliary values that are dependent only on the
        // degrees of the base and reduced curves and continuity constraints.
        // Therefore it reuses computed results if possible. These here are the
        // arguments passed during the last call and the auxiliary values
        // array P.

        private int nPrev = -1;
        private int mPrev = -1;
        private int kPrev = -1;
        private int lPrev = -1;
        private double[,] Phi = new double[0, 0];

        public LeastSquaresReducer()
        {
            PropertyChanged +=  base_PropertyChanged;
            RefreshLabel();
        }

        protected override bool ReduceInner(Vector2D[] V, Vector2D[] W,
            int k, int l)
        {
            int n = V.Length - 1;
            int m = W.Length - 1;

            if (m - k - l + 1 == 0) return true;

            // We have curve of degree n
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
            // of a curve of degree m
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

            // First thing we do is find the Bezier control points of the curve
            //
            //                    k - 1            m
            //                   ,-----,        ,-----,
            //                    \          m   \          m
            //            u = v -  )   W[i] B  -  )   W[i] B  .
            //                    /          i   /          i
            //                   '-----'        '-----'
            //                    i = 0       i = m - l + 1
            //
            // Since we know the constraints hold, some of the control points
            // are 0, therefore we can represent u be reduced set of points
            // U[i], i = k, k + 1, ..., n - l,
            //
            //                       n - l
            //                      ,-----,
            //                       \          n
            //                   u =  )   U[i] B  .
            //                       /          i
            //                      '-----'
            //                       i = k
            //
            // We find U[i]'s using degree elevation on W[i]'s and subtracting
            // them from V[i]'s.

            Vector2D[] U = new Vector2D[n + 1];
            for (int i = 0; i <= m; ++i)
                U[i] = W[i];
            for (int h = m + 1; h <= n; ++h)
                for (int i = h; i > 0; --i)
                    U[i] = (double)i / (double)h * U[i - 1]
                         + (double)(h - i) / (double)h * U[i];
            for (int i = k; i <= n - l; ++i)
                U[i] = V[i] - U[i];

            // Now we will use auxiliary values Phi[i, j] so here we ensure they
            // are up to date.

            UpdatePhi(n, m, k, l);

            // From now on finding W[i]'s is straight forward. We just do
            //
            //                    n - l
            //                   ,-----,
            //                    \
            //             W[i] =  )   U[j] * Phi[i - k, j - k] .
            //                    /
            //                   '-----'
            //                    j = k
            //
            // for i = k, k + 1, ..., m - l.

            for (int i = k; i <= m - l; ++i)
                for (int j = k; j <= n - l; ++j)
                    W[i] += U[j] * Phi[i - k, j - k]; // We assume W[i] = 0
                                                      // initially.
            return true;
        }

        private void UpdatePhi(int n, int m, int k, int l)
        {
            if (nPrev == n && mPrev == m && kPrev == k && lPrev == l) return;

            nPrev = n;  mPrev = m; kPrev = k; lPrev = l;

            // We want to compute values
            //
            //                       / n       (m, k, l) \
            //          Phi[i, j] = < B     , D           >
            //                       \ k + j   k + i     /
            //
            // for i = 0, 1, ... m - k - l and j = 0, 1, ..., n - k - l, where
            //  (m, k, l)
            // D          denote the constrained dual Berstein polynomials.
            //  k + i

            if (
                    m - k - l + 1 != Phi.GetLength(0)
                ||  n - k - l + 1 != Phi.GetLength(1)
            )
                Phi = new double[m - k - l + 1, n - k - l + 1];

            // First we compute auxiliary values Psi[i, j] for
            // i = 0, 1, ..., m - k - l, j = 0, 1, ..., n - k - l.
            // To save up space we store Psi in the array Phi.

            double[,] Psi = Phi;

            // The First row Psi[0, j] satisfies
            //
            //       Psi[0, j + 1] = E(j) Psi[0][j] + F(j) Psi[0][j - 1],
            //
            // where
            //
            //                           (m - k - l) (m + k + l + 2)
            //        E(j) = 1 - F(j) - ---------------------------- ,
            //                          (n - k - l - j) (2k + j + 1)
            //
            //               (1 - j) (n + l - k - j + 1)
            //        F(j) = ---------------------------- .
            //               (n - k - l - j) (2k + j + 1)
            //
            // The starting values are
            //                          
            // Psi[0, 0] = (k + l - n) C *
            //
            //             m - k - l
            //             ,-----,                       (m + k + l + 2)
            //              \        h / m - k - l \                    h
            //           *   )   (-1)  |           | -------------------------
            //              /          \     h     / (k + l + h - n) (2l + 1)
            //             '-----'                                           h
            //              h = 0
            //                             (2k + 2)
            //                 m - k - l           m - k - l
            // Psi[0, 1] = (-1)          C ----------------- ,
            //                             (2l + 1)
            //                                     m - k - l
            //
            //             (n - k - l)! (k + l - n + 1)
            //                                         m - k - l
            //         C = -------------------------------------.
            //             (m - k - l)! (m + k + l + 2)
            //                                         n - k - l

            double C = 1F;
            for (int i = 0; i < n - k - l; ++i)
                C *= (double)(n - k - l - i) / (double)(m + n - i + 1);
            for (int i = 0; i < m - k - l; ++i)
                C *= (double)(m - n - i) / (double)(m - k - l - i);
            
            double f = 1F / (double)(k + l - n);
            for (int h = 0; h <= m - k - l; ++h)
            {
                Psi[0, 0] += f;
                f *= (double)(k + l + h - m)
                   / (double)(h + 1)
                   * (double)(k + l + h - n)
                   / (double)(k + l + h - n + 1)
                   * (double)(m + k + l + h + 2)
                   / (double)(2 * l + h + 1);
            }
            Psi[0, 0] *= C * (double)(k + l - n);

            Psi[0, 1] = (m - k - l) % 2 == 0 ? C : -C;
            for (int i = 0; i < m - k - l; ++i)
                Psi[0, 1] *= (double)(2 * k + i + 2) / (double)(2 * l + i + 1);

            for (int j = 1; j < n - k - l; ++j) 
            {
                double F = (double)(1 - j)
                        / (double)(n - k - l - j)
                        * (double)(n + l - k - j + 1)
                        / (double)(2 * k + j + 1);

                double E = 1F - F
                        - (double)(m - k - l)
                        / (double)(n - k - l - j)
                        * (double)(m + k + l + 2)
                        / (double)(2 * k + j + 1);

                Psi[0, j + 1] = E * Psi[0, j] + F * Psi[0, j - 1];
            }

            // The remaining values can be computed using relation
            //
            //                     /
            //     Psi[i + 1, j] = | A(n, j) Psi[i, j - 1]
            //                     \
            //
            //                     + C(i, j) Psi[i, j]
            //
            //                     + B(n, j) Psi[i, j + 1]
            //
            //                                             \  /
            //                     - A(m, i) Psi[i - 1, j] | / B(m, i) ,
            //                                             //
            // where
            //
            //          A(r, s) = s (k + s - l - r - 1),
            //
            //          B(r, s) = (k + l + s - r) (2k + s + 1),
            //
            //          C(i, j) = A(m, i) + B(m, i) - A(n, j) - B(n, j)

            double Bm0 = (k + l - m) * (2 * k + 1);
            double Bn0 = (k + l - n) * (2 * k + 1);
            double C00 = Bm0 - Bn0;
            double Ane = (k + l - n) * (2 * l + 1);
            double C0e = Bm0 - Ane;

            if (m - k - l > 0)
            {
                Psi[1, 0] = C00 * Psi[0, 0]
                          + Bn0 * Psi[0, 1];
                Psi[1, 0] /= Bm0;

                for (int j = 1; j < n - k - l; ++j)
                {
                    double Anj = j * (k + j - l - n - 1);
                    double Bnj = (k + l + j - n) * (2 * k + j + 1);
                    double C0j = Bm0 - Anj - Bnj;

                    Psi[1, j] = Anj * Psi[0, j - 1]
                              + C0j * Psi[0, j    ]
                              + Bnj * Psi[0, j + 1];
                    Psi[1, j] /= Bm0;
                }

                Psi[1, n - k - l] = Ane * Psi[0, n - k - l - 1]
                                  + C0e * Psi[0, n - k - l];
                Psi[1, n - k - l] /= Bm0;
            }

            for (int i = 1; i < m - k - l; ++i)
            {
                double Ami = i * (k + i - l - m - 1);
                double Bmi = (k + l + i - m) * (2 * k + i + 1);
                double Ci0 = Ami + Bmi - Bn0;

                Psi[i + 1, 0] = Ci0 * Psi[i    , 0]
                              + Bn0 * Psi[i    , 1]
                              - Ami * Psi[i - 1, 0];
                Psi[i + 1, 0] /= Bmi;

                for (int j = 1; j < n - k - l; ++j)
                {
                    double Anj = j * (k + j - l - n - 1);
                    double Bnj = (k + l + j - n) * (2 * k + j + 1);
                    double Cij = Ami + Bmi - Anj - Bnj;

                    Psi[i + 1, j] = Anj * Psi[i    , j - 1]
                                  + Cij * Psi[i    , j    ]
                                  + Bnj * Psi[i    , j + 1]
                                  - Ami * Psi[i - 1, j    ];
                    Psi[i + 1, j] /= Bmi;
                }

                double Cie = Ami + Bmi - Ane;

                Psi[i + 1, n - k - l] = Ane * Psi[i    , n - k - l - 1]
                                      + Cie * Psi[i    , n - k - l    ]
                                      - Ami * Psi[i - 1, n - k - l    ];
                Psi[i + 1, n - k - l] /= Bmi;
            }

            // Now after these hardcore berserk crazy evil computations the only
            // thing there is to be done is computing Phi[i, j] the following
            // way:
            //
            //             / m - k - l \ /   n   \ /   m   \-1
            // Phi[i, j] = |           | |       | |       |   *
            //             \     i     / \ k + j / \ k + i /
            //
            //             (2k + 1)  (2l + 1)
            //                     j         n - k - l - j
            //           * ------------------------------ Psi[i, j] .
            //                      (n - k - l)!
            //
            // Please note that since we used array Phi for computing Psi, now
            // it is filled with the values of Psi.

            double p = 1F;
            for (int i = 0; i < k; ++i)
                p *= (double)(n - i) / (double)(m - i);
            for (int i = 0; i < n - k - l; ++i)
                p *= (double)(n - k + l - i) / (double)(n - k - l - i);

            for (int i = 0; i <= m - k - l; ++i)
            {
                double q = p;
                for (int j = 0; j <= n - k - l; ++j)
                {
                    Phi[i, j] *= q;
                    q *= (double)(n - k - j)
                       / (double)(k + j + 1)
                       * (double)(2 * k + j + 1)
                       / (double)(n + l - k - j);
                }

                p *= (double)(m - k - l - i)
                   / (double)(i + 1)
                   * (double)(k + i + 1)
                   / (double)(m - k - i);
            }

            // Thank You Mr. Woźny -- that was a pleasant ride.
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
                "Least Squares ({0}, {1})",
                ContinuityClassAt0,
                ContinuityClassAt1
            );
        }

    }
}
