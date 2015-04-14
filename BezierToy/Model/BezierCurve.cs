using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    public class BezierCurve : INotifyPropertyChanged
    {
        public static Vector2D Eval(Vector2D[] points, double t)
        {
            int n = points.Length - 1;
            if (t <= 0.5)
            {
                double u = t / (1F - t);
                int b = 1;
                Vector2D result = points[n];
                for (int k = n - 1; k >= 0; --k)
                {
                    b *= k + 1;
                    b /= n - k;
                    result = u * result + b * points[k];
                }
                return MyMath.Power(1F - t, n) * result;
            }
            else
            {
                double u = (1F - t) / t;
                int b = 1;
                Vector2D result = points[0];
                for (int k = 1; k <= n; ++k)
                {
                    b *= n - k + 1;
                    b /= k;
                    result = u * result + b * points[k];
                }
                return MyMath.Power(t, n) * result;
            }
        }

        public class PointCollection : IEnumerable<Vector2D>
        {
            private List<Vector2D> points = new List<Vector2D>();

            public event Action CountChanged;

            public event Action MovedPoint;

            public int Count { get { return points.Count; } }

            public void Add(Vector2D point)
            {
                points.Add(point);
                CountChanged();
            }

            public void Remove(IEnumerable<int> selected)
            {
                foreach (int i in selected)
                    points.RemoveAt(i);
                CountChanged();
            }

            public void Clear()
            {
                if (points.Count > 0)
                {
                    points.Clear();
                    CountChanged();
                }
            }

            public void MoveBy(IEnumerable<int> selected, Vector2D offset)
            {
                foreach (int i in selected)
                    points[i] += offset;
                if (MovedPoint != null) MovedPoint();
            }

            public int Hit(Vector2D point, double radius)
            {
                radius *= radius;
                return points.FindIndex(p => (p - point).LengthSq() <= radius);
            }

            public IEnumerator<Vector2D> GetEnumerator()
            {
                return points.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }

        public enum ShapeChangedType
        {
            DegreeChanged,
            PointMoved
        }

        public class ShapeChangedArgs : EventArgs
        {
            public ShapeChangedType ShapeChangedType { get; private set; }

            public ShapeChangedArgs(ShapeChangedType type)
            {
                ShapeChangedType = type;
            }
        }

        public delegate void ShapeChangedHandler(object sender,
            ShapeChangedArgs args);

        public event ShapeChangedHandler ShapeChanged;
        private void OnShapeChanged(ShapeChangedType type)
        {
            if (ShapeChanged != null)
                ShapeChanged(this, new ShapeChangedArgs(type));
        }

        private PointCollection points = new PointCollection();
        public PointCollection Points { get { return points; } }

        private Color color = Color.DarkBlue;
        public Color Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        
        public int Degree { get { return Math.Max(Points.Count - 1, 0); } }

        public BezierCurve()
        {
            Points.CountChanged += () => OnPropertyChanged("Degree");
            Points.MovedPoint += () => OnShapeChanged(ShapeChangedType.PointMoved);
            Points.CountChanged += () => OnShapeChanged(ShapeChangedType.DegreeChanged);
        }
    }
}
