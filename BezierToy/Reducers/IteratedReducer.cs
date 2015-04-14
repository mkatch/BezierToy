using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    abstract class IteratedReducer : Reducer
    {
        public override bool Reduce(Vector2D[] basePoints, Vector2D[] reducedPoints)
        {
            int n = basePoints.Length - 1;
            int m = reducedPoints.Length - 1;
            Vector2D[] points1 = basePoints;

            if (n > m + 1)
            {
                points1 = new Vector2D[n];
                if (!ReduceByOne(basePoints, points1, n)) return false;
                --n;
            }
            if (n > m + 1)
            {
                Vector2D[] points2 = new Vector2D[n];
                while (n > m + 1)
                {
                    if (!ReduceByOne(points1, points2, n)) return false;
                    Vector2D[] tmp = points1;
                    points1 = points2;
                    points2 = tmp;
                    --n;
                }
            }

            return ReduceByOne(points1, reducedPoints, n);
        }

        abstract public bool ReduceByOne(Vector2D[] basePoints,
            Vector2D[] reducedPoints, int n);
    }
}
