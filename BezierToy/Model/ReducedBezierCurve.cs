using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    public class ReducedBezierCurve : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(propertyName)
                );
        }

        public event Action ShapeChanged;
        private void OnShapeChanged()
        {
            if (ShapeChanged != null) ShapeChanged();
        }

        private BackgroundWorker asyncReducer = new BackgroundWorker();

        private BezierCurve baseCurve;
        private bool reductionPending = false;

        private Vector2D[] points = new Vector2D[0];
        public IEnumerable<Vector2D> Points { get { return points; } }

        private Reducer reducer;
        public Reducer Reducer
        {
            get { return reducer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (reducer != null)
                    reducer.PropertyChanged -= reducer_PropertyChanged;
                reducer = value;
                reducer.PropertyChanged += reducer_PropertyChanged;
                OnPropertyChanged("Label");
                Refresh();
            }
        }

        void reducer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Label")
                OnPropertyChanged("Label");
            else
                Refresh();
        }

        public string Label { get { return Reducer.Label; } }

        public int ProcessedDegree
        {
            get { return Math.Max(points.Length - 1, 0); }
        }

        int degree = 0;
        public int Degree
        {
            get { return degree; }
            set
            {
                if (value < 0 || value > MaxDegree)
                    throw new ArgumentOutOfRangeException(
                        "Degree",
                        "Only values between 0 and MaxDegree accepted"
                    );
                if (degree != value)
                {
                    degree = value;
                    OnPropertyChanged("Degree");
                    Refresh();
                }
            }
        }

        private bool valid = true;
        public bool Valid
        {
            get { return valid; }
            set
            {
                if (valid != value)
                {
                    valid = value;
                    OnPropertyChanged("Valid");
                }
            }
        }

        public int MaxDegree { get { return Math.Max(baseCurve.Degree - 1, 0); } }

        Color color = Color.Red;
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

        public ReducedBezierCurve(BezierCurve baseCurve, Reducer reducer)
        {
            this.baseCurve = baseCurve;
            this.baseCurve.ShapeChanged += baseCurve_ShapeChanged;

            asyncReducer.DoWork += AsyncReduceBegin;
            asyncReducer.RunWorkerCompleted += AsyncReduceFinalize;

            Reducer = reducer;

            Degree = Math.Max(baseCurve.Degree - 1, 0);
        }

        void baseCurve_ShapeChanged(object sender, BezierCurve.ShapeChangedArgs args)
        {
            switch (args.ShapeChangedType)
            {
                case BezierCurve.ShapeChangedType.DegreeChanged:
                    if (MaxDegree < Degree)
                        Degree = MaxDegree;
                    else
                        Refresh();
                    OnPropertyChanged("MaxDegree");
                    break;

                case BezierCurve.ShapeChangedType.PointMoved:
                    Refresh();
                    break;
            }
        }

        void Refresh()
        {
            if (asyncReducer.IsBusy)
                reductionPending = true;
            else
                asyncReducer.RunWorkerAsync();

        }

        public void DisconnectFromEvents()
        {
            baseCurve.ShapeChanged -= baseCurve_ShapeChanged;
            reducer.PropertyChanged -= reducer_PropertyChanged;
        }

        private void AsyncReduceBegin(object sender, DoWorkEventArgs args)
        {
            Vector2D[] newPoints = new Vector2D[Degree + 1];
            try
            {
                if (Reducer.Reduce(baseCurve.Points.ToArray(), newPoints))
                    args.Result = newPoints;
                else
                    args.Result = null;
            }
            catch { args.Result = null; }
        }

        private void AsyncReduceFinalize(object sender,
            RunWorkerCompletedEventArgs args)
        {
            Vector2D[] newPoints = args.Result as Vector2D[];

            if (newPoints == null)
            {
                Valid = false;
                OnShapeChanged();
            }
            else
            {
                bool processedDegreeChanged
                    = (ProcessedDegree != newPoints.Length - 1);
                points = newPoints;
                if (processedDegreeChanged)
                    OnPropertyChanged("ProcessedDegree");
                OnShapeChanged();
                Valid = true;
            }

            if (reductionPending)
            {
                reductionPending = false;
                Refresh();
            }
        }
    }
}
