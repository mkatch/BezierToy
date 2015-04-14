using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierToy
{
    public partial class MainWindow : Form
    {
        private static MainWindow instance;
        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainWindow();
                return instance;
            }
        }

        private const float SCALING_FACTOR = 1.1F;

        private enum WorkModes
        {
            Modify,
            Drag
        }

        public PictureBox Canvas { get { return canvas; } }

        private Model Model;
        private View View;

        private WorkModes mode = WorkModes.Modify;
        private Vector2D lastMousePos;
        private bool heldPointDeselectionAllowed = false;
        private bool holdingCanvas = false;
        private bool holdingPoints = false;
        private bool allowZoom = true;

        private MainWindow()
        {
            InitializeComponent();

            Model = Model.Instance;
            View = View.Instance;


            canvas.Paint += View.canvas_Paint;

            reducedBezierCurveDetailView.Reducers = Model.Reducers;

            this.MouseWheel += canvas_MouseWheel;
            modifyButton.Enabled = false;

            modelBindingSource.DataSource = Model;
            baseCurveBindingSource.DataSource = Model.BaseCurve;
            reducedCurvesBindingSource.DataSource = Model.ReducedCurves;
            reducedBezierCurvesView.DataSource = reducedCurvesBindingSource;
            reducedCurvesBindingSource.ListChanged += (s, e) => Canvas.Invalidate();
            Model.PropertyChanged += Model_PropertyChanged;

            View.Origin = Canvas.Center();
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ShowPoints" || e.PropertyName == "ConnectPoints")
                Canvas.Invalidate();
        }

        private void SetWorkMode(WorkModes mode)
        {
            dragButton.Enabled = (mode != WorkModes.Drag);
            modifyButton.Enabled = (mode != WorkModes.Modify);
            this.mode = mode;

            if (mode == WorkModes.Drag)
            {
                Canvas.Cursor = Cursors.SizeAll;
                Model.SelectedPoints.Clear();
            }
            else
                Canvas.Cursor = Cursors.Cross;
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (allowZoom)
                View.Scale *= (float) Math.Pow(SCALING_FACTOR,
                    e.Delta / SystemInformation.MouseWheelScrollDelta);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                SetWorkMode(WorkModes.Drag);

            if (mode == WorkModes.Modify && e.Button == MouseButtons.Left)
            {
                Vector2D point = View.ToModel(new Vector2D(e.X, e.Y));

                if (
                        !Model.SelectedPoints.Contains(Model.HoveredOverPoint)
                    &&  (Control.ModifierKeys & Keys.Control) == 0
                )
                    Model.SelectedPoints.Clear();

                if (Model.HoveredOverPoint < 0)
                {
                    Model.BaseCurve.Points.Add(point);
                    Model.HoveredOverPoint = Model.BaseCurve.Points.Count - 1;
                }

                if (Model.SelectedPoints.Contains(Model.HoveredOverPoint))
                    heldPointDeselectionAllowed = true;
                else
                    Model.SelectedPoints.Add(Model.HoveredOverPoint);

                holdingPoints = true;
            }
            else if (
                    (mode == WorkModes.Drag && e.Button == MouseButtons.Left)
                || e.Button == MouseButtons.Right
            )
                holdingCanvas = true;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            holdingPoints = false;
            holdingCanvas = false;
            if (e.Button == MouseButtons.Right)
                SetWorkMode(WorkModes.Modify);
        }
        
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Vector2D mousePos = new Vector2D(e.X, e.Y);
            Vector2D deltaMouse = mousePos - lastMousePos;

            int oldHoveredOverPoint = Model.HoveredOverPoint;
            if (!holdingPoints)
                Model.HoveredOverPoint = Model.BaseCurve.Points.Hit(
                                            View.ToModel(mousePos),
                                            View.PointSize / View.Scale
                                       );

            if (holdingPoints)
                Model.BaseCurve.Points.MoveBy(Model.SelectedPoints, deltaMouse / View.Scale);
            else if (holdingCanvas)
                View.Origin += deltaMouse;

            lastMousePos = mousePos;
            heldPointDeselectionAllowed = false;
        }

        private void canvas_Resize(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            SetWorkMode(WorkModes.Modify);
        }

        private void dragButton_Click(object sender, EventArgs e)
        {
            SetWorkMode(WorkModes.Drag);
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            if (heldPointDeselectionAllowed)
                Model.SelectedPoints.Remove(Model.HoveredOverPoint);
        }

        void reducedCurve_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Color")
                Canvas.Invalidate();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ReducedBezierCurve reducedCurve = new ReducedBezierCurve(
                Model.BaseCurve,
                new IteratedInverseElevationReducer()
            );
            Random random = new Random();
            reducedCurve.Color = Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256)
            );
            reducedCurve.ShapeChanged += () => Canvas.Invalidate();
            reducedCurve.PropertyChanged += reducedCurve_PropertyChanged;
            int index = reducedCurvesBindingSource.Add(reducedCurve);
            reducedBezierCurvesView.Select(index);
        }

        private void reducedBezierCurvesView_SelectedCurveChanged()
        {
            // Could be done better, but works.
            reducedBezierCurveDetailView.Curve
                = reducedBezierCurvesView.SelectedCurve;
            Model.SelectedCurve = reducedBezierCurvesView.SelectedCurve;
            Canvas.Invalidate();
        }

        private void canvas_MouseEnter(object sender, EventArgs e)
        {
            canvas.Focus();
            allowZoom = true;
        }

        private void canvas_MouseLeave(object sender, EventArgs e)
        {
            allowZoom = false;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Model.SelectedCurve != null)
            {
                Model.SelectedCurve.DisconnectFromEvents();
                reducedCurvesBindingSource.Remove(Model.SelectedCurve);
                Model.SelectedCurve = null;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    SetWorkMode(WorkModes.Drag);
                    break;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    SetWorkMode(WorkModes.Modify);
                    break;

                case Keys.N:
                    if (ModifierKeys == Keys.Control)
                        newToolStripMenuItem_Click(null, null);
                    break;

                case Keys.O:
                    if (ModifierKeys == Keys.Control)
                        openToolStripMenuItem_Click(null, null);
                    break;

                case Keys.S:
                    if (ModifierKeys == Keys.Control)
                        saveToolStripMenuItem_Click(null, null);
                    else if (ModifierKeys == (Keys.Control | Keys.Shift))
                        saveAsToolStripMenuItem_Click(null, null);
                    break;

                case Keys.A:
                    if (ModifierKeys == Keys.Control)
                        selectAllToolStripMenuItem_Click(null, null);
                    break;

                case Keys.Q:
                    if (ModifierKeys == Keys.Control)
                        closeToolStripMenuItem_Click(null, null);
                    break;

                case Keys.Delete:
                    Model.BaseCurve.Points.Remove(Model.SelectedPoints);
                    Model.SelectedPoints.Clear();
                    break;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveModelDialog.ShowDialog() == DialogResult.OK)
                Model.Save(saveModelDialog.FileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openModelDialog.ShowDialog() == DialogResult.OK)
            {
                Model.Load(openModelDialog.FileName);
                reducedCurvesBindingSource.ResetBindings(false);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.BaseCurve.Points.Clear();
            reducedCurvesBindingSource.Clear();
        }

        private void centerCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.Origin = Canvas.Center();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Model.FileName == null)
                saveAsToolStripMenuItem_Click(sender, e);
            else
                Model.Save(Model.FileName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Model.SelectedPoints.Count == Model.BaseCurve.Points.Count)
                Model.SelectedPoints.Clear();
            else
                for (int i = 0; i < Model.BaseCurve.Points.Count; ++i)
                    Model.SelectedPoints.Add(i);
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkMode(WorkModes.Modify);
        }

        private void dragToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkMode(WorkModes.Drag);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.ShowPoints = showToolStripMenuItem.Checked;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.ConnectPoints = connectToolStripMenuItem.Checked;
        }

        private void asJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportDialog.DefaultExt = "jpg";
            exportDialog.Filter = "JPEG files|*.jpg;*.jpeg*|All files|*.*";
            if (exportDialog.ShowDialog() == DialogResult.OK)
                Model.Export(exportDialog.FileName, ImageFormat.Jpeg);
        }

        private void asPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportDialog.DefaultExt = "png";
            exportDialog.Filter = "PNG files|*.png|All files|*.*";
            if (exportDialog.ShowDialog() == DialogResult.OK)
                Model.Export(exportDialog.FileName, ImageFormat.Png);
        }

        private void asBMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportDialog.DefaultExt = "bmp";
            exportDialog.Filter = "Bitmap files|*.bmp|All files|*.*";
            if (exportDialog.ShowDialog() == DialogResult.OK)
                Model.Export(exportDialog.FileName, ImageFormat.Bmp);
        }
    }
}
