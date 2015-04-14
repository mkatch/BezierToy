using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace BezierToy
{
    partial class Model : INotifyPropertyChanged
    {
        public class SelectedPointsCollection : IEnumerable<int>
        {
            private HashSet<int> points = new HashSet<int>();

            public event Action Changed;

            public int Count { get { return points.Count; } }

            public bool Add(int i)
            {
                if (points.Add(i))
                {
                    Changed();
                    return true;
                }
                else return false;
            }

            public bool Remove(int i)
            {
                if (points.Remove(i))
                {
                    Changed();
                    return true;
                }
                else return false;
            }

            public void Clear()
            {
                if (points.Count > 0)
                {
                    points.Clear();
                    Changed();
                }
            }

            public bool Contains(int i) { return points.Contains(i); }

            public IEnumerator<int> GetEnumerator()
            {
                return points.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        private static Model instance;
        public static Model Instance { get {
            if (instance == null)
                instance = new Model();
            return instance;
        } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(propertyName)
                );
        }

        public string FileName { get; set; }

        private bool connectPoints = true;
        public bool ConnectPoints
        {
            get { return connectPoints; }
            set
            {
                if (connectPoints != value)
                {
                    connectPoints = value;
                    RaisePropertyChanged("ConnectPoints");
                }
            }
        }

        private bool showPoints = true;
        public bool ShowPoints
        {
            get { return showPoints; }
            set
            {
                if (showPoints != value)
                {
                    showPoints = value;
                    RaisePropertyChanged("ShowPoints");
                }
            }
        }
        
        private List<ReducedBezierCurve> reducedCurves
            = new List<ReducedBezierCurve>();
        public List<ReducedBezierCurve> ReducedCurves
        {
            get { return reducedCurves; }
        }

        public ReducedBezierCurve SelectedCurve { get; set; }

        private SelectedPointsCollection selectedPoints
            = new SelectedPointsCollection();
        public SelectedPointsCollection SelectedPoints
        { get { return selectedPoints; } }

        private int hoveredOverPoint = -1;
        public int HoveredOverPoint
        {
            get { return hoveredOverPoint; }
            set
            {
                if (hoveredOverPoint != value)
                {
                    hoveredOverPoint = value;
                    RaisePropertyChanged("HoveredOverPoint");
                }
            }
        }

        private BezierCurve baseCurve = new BezierCurve();
        public BezierCurve BaseCurve { get { return baseCurve; } }

        private List<ReducerRecord> reducers = new List<ReducerRecord>();
        public List<ReducerRecord> Reducers
        {
            get { return reducers; }
        }

        private Model()
        {
            SelectedPoints.Changed
                += () => RaisePropertyChanged("SelectedPoints");

            Reducers.Add(new ReducerRecord(
                "Iterated Inverse Elevation",
                "inverse-elevation",
                new ReducerFactory<IteratedInverseElevationReducer>(),
                null
            ));
            Reducers.Add(new ReducerRecord(
                "Iterated Optimal",
                "optimal",
                new ReducerFactory<IteratedOptimalReducer>(),
                null
            ));
            Reducers.Add(new ReducerRecord(
                "Hermite Interpolation",
                "hermite",
                new ReducerFactory<HermiteReducer>(),
                new ConstrainedReducerView()
            ));
            Reducers.Add(new ReducerRecord(
                "Least Squares",
                "least-squares",
                new ReducerFactory<LeastSquaresReducer>(),
                new ConstrainedReducerView()
            ));
        }
    }
}
