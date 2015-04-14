using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierToy
{
    class View
    {
        private static View instance;
        public static View Instance { get {
            if (instance == null)
                instance = new View();
            return instance;
        } }

        private Pen axesPen = Pens.LightGray;
        private Pen connectPen = new Pen(Color.Gray, 2)
            { DashStyle = DashStyle.Dot };
        private Pen curvePen = new Pen(Color.Black, 3);
        private SolidBrush normalBrush = new SolidBrush(Color.Black);
        private SolidBrush highlightBrush = new SolidBrush(Color.Gray);
        private SolidBrush reducedPointBrush = new SolidBrush(Color.Red);

        private Model Model { get { return Model.Instance; } }
        private MainWindow MainWindow { get { return MainWindow.Instance; } }

        private float minScale = 0.2F;
        public float MinScale
        {
            get { return minScale; }
            set
            {
                if (value <= 0F || value > MaxScale)
                    throw new ArgumentException("Only positive values smaller "
                        + "than MaxScale accepted for MinScale");
                minScale = value;
                Scale = scale;
            }
        }

        private float maxScale = 10F;
        public float MaxScale
        {
            get { return maxScale; }
            set
            {
                if (value < minScale)
                    throw new ArgumentException("MaxScale cannot be smaller "
                        + "than MinScale");
                maxScale = value;
                Scale = scale;
            }
        }

        private float pointSize = 12F;
        public float PointSize
        {
            get { return pointSize; }
            set
            {
                if (value <= 0F)
                    throw new ArgumentOutOfRangeException("PointSize",
                        "Only values greater than 0 accepted");
                pointSize = value;
            }
        }

        public float SelectedPointSize { get { return 1.3F * pointSize; } }

        private float scale = 1F;
        public float Scale
        {
            get { return scale; }
            set
            { 
                float newScale = Math.Min(Math.Max(value, MinScale), MaxScale);
                if (newScale != scale)
                {
                    origin = MainWindow.Canvas.Center()
                        + newScale * (Origin - MainWindow.Canvas.Center()) / scale;
                    scale = newScale;
                    MainWindow.Canvas.Invalidate();
                }
            }
        }

        private Vector2D origin = new Vector2D(0, 0);
        public Vector2D Origin
        {
            get { return origin; }
            set
            {
                if (origin != value)
                {
                    origin = value;
                    MainWindow.Canvas.Invalidate();
                }
            }
        }

        private View()
        {
            Model.BaseCurve.PropertyChanged += ModelChanged;
            Model.BaseCurve.ShapeChanged += (s, a) => MainWindow.Canvas.Invalidate();
            Model.PropertyChanged += ModelChanged;
        }

        void ModelChanged(object sender, PropertyChangedEventArgs e)
        {
            string p = e.PropertyName;
            if (sender.Equals(Model.BaseCurve))
            {
                if (p == "Color")
                    MainWindow.Canvas.Invalidate();
            }
            else if (sender.Equals(Model))
            {
                if (p == "HoveredOverPoint" || p == "SelectedPoints")
                    MainWindow.Canvas.Invalidate();
            }
        }

        public Vector2D ToModel(Vector2D point)
        {
            return (point - origin) / scale;
        }

        public void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.DrawLine(
                axesPen,
                0, (float)origin.Y,
                MainWindow.Canvas.Width, (float)origin.Y
            );
            e.Graphics.DrawLine(
               axesPen,
               (float)origin.X, 0,
               (float)origin.X, MainWindow.Canvas.Height
            );

            if (Model.BaseCurve.Points.Count == 0) return;

            Vector2D[] points = new Vector2D[Model.BaseCurve.Points.Count];
            int p = 0;
            foreach (Vector2D point in Model.BaseCurve.Points)
                points[p++] = origin + scale * point;

            if (Model.ConnectPoints)
            {
                connectPen.Color = Model.BaseCurve.Color.Brightened(0.5F);
                Vector2D prevPoint = points[0];
                foreach (Vector2D point in points)
                {
                    e.Graphics.DrawLine(connectPen, prevPoint, point);
                    prevPoint = point;
                }
            }

            curvePen.Color = Model.BaseCurve.Color;
            e.Graphics.DrawBezierCurve(curvePen, points);

            if (Model.ShowPoints)
            {
                normalBrush.Color = Model.BaseCurve.Color;
                highlightBrush.Color = Model.BaseCurve.Color.Brightened(0.5F);
                p = 0;
                foreach (Vector2D point in points)
                {
                    Brush brush = Model.SelectedPoints.Contains(p)
                        || p == Model.HoveredOverPoint
                        ? highlightBrush : normalBrush;
                    float size = Model.SelectedPoints.Contains(p)
                        ? SelectedPointSize : pointSize;
                    e.Graphics.FillEllipse(
                        brush,
                        (float)point.X - size / 2, (float)point.Y - size / 2,
                        size, size
                    );
                    ++p;
                }
            }

            foreach (ReducedBezierCurve curve in Model.ReducedCurves)
            {
                if (!curve.Valid) continue;

                points = new Vector2D[curve.ProcessedDegree + 1];
                p = 0;
                foreach (Vector2D point in curve.Points)
                {
                    points[p] = origin + scale * point;
                    ++p;
                }
                curvePen.Color = curve.Color;

                if (Model.ConnectPoints && curve == Model.SelectedCurve)
                {
                    Vector2D prevPoint = points[0];
                    foreach (Vector2D point in points)
                    {
                        connectPen.Color = curve.Color.Brightened(0.5F);
                        e.Graphics.DrawLine(connectPen, prevPoint, point);
                        prevPoint = point;
                    }
                }

                e.Graphics.DrawBezierCurve(curvePen, points);

                if (Model.ShowPoints && curve == Model.SelectedCurve)
                {
                    reducedPointBrush.Color = curve.Color;
                    foreach (Vector2D point in points)
                        e.Graphics.FillEllipse(
                            reducedPointBrush,
                            (float)point.X - 4, (float)point.Y - 4,
                            8, 8
                        );
                }
            }
        }

        public void curveDataRepeater_ItemTemplate_Paint(object sender,
            PaintEventArgs e)
        {
            DataRepeaterItem item = sender as DataRepeaterItem;
            Control checkBox = item.Controls["curveSelectedCheckBox"];
            Control nameLabel = item.Controls["curveNameLabel"];
            int y = (checkBox.Top + checkBox.Bottom) / 2;
            e.Graphics.DrawLine(
                item.Tag as Pen,
                checkBox.Location.X + checkBox.Width + 10, y,
                nameLabel.Location.X - 10, y
            );
        }
    }

    public static class GraphicsBezierExtension
    {
        private const int SEGMENT_COUNT = 256;

        public static void DrawBezierCurve(this Graphics g, Pen pen, Vector2D[] controlPoints)
        {
            if (controlPoints.Length == 0) return;

            int n = controlPoints.Length - 1;
            PointF[] pathPoints = new PointF[SEGMENT_COUNT + 1];

            for (int i = 0; i <= SEGMENT_COUNT; ++i)
            {
                float t = (float)i / (float)SEGMENT_COUNT;
                pathPoints[i] = BezierCurve.Eval(controlPoints, t); 
            }

            g.DrawLines(pen, pathPoints);
        }
    }

    public static class PictureBoxExtensions
    {
        public static Vector2D Center(this PictureBox box)
        {
            return new Vector2D(box.Width / 2, box.Height / 2);
        }
    }

    public static class ColorExtensions
    {
        public static Color Brightened(this Color color, float a)
        {
            return Color.FromArgb(
                (int)(color.R + a * (255 - color.R)),
                (int)(color.G + a * (255 - color.G)),
                (int)(color.B + a * (255 - color.B))
            );
        }
    }
}
