using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierToy
{
    public partial class ReducedBezierCurveDetailView : UserControl
    {
        private ReducedBezierCurve curve;
        public ReducedBezierCurve Curve
        {
            get { return curve; }
            set
            {
                curve = value;
                Visible = (value != null);
                if (curve != null)
                {
                    reducedBezierCurveBindingSource.DataSource = curve;
                    if (reducers != null)
                    {
                        methodComboBox.SelectedIndex = reducers.FindIndex(
                            r => r.Factory.CanProduce(curve.Reducer));
                        methodComboBox_SelectedIndexChanged(methodComboBox, new EventArgs());
                    }
                }
            }
        }

        private List<ReducerRecord> reducers;
        public List<ReducerRecord> Reducers
        {
            get { return reducers; }
            set
            {
                reducers = value;
                methodComboBox.DataSource = reducers;
            }
        }

        public string MethodsDisplayMember
        {
            get { return methodComboBox.DisplayMember; }
            set { methodComboBox.DisplayMember = value; }
        }

        public ReducedBezierCurveDetailView()
        {
            InitializeComponent();
            methodComboBox.DisplayMember = "Name";
            Visible = false;
        }

        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = methodComboBox.SelectedIndex;

            if (index < 0 || curve == null) return;

            if (
                    curve != null
                &&  !reducers[index].Factory.CanProduce(curve.Reducer)
            )
                curve.Reducer = reducers[index].Factory.Produce();

            if (reducerViewPanel.Controls.Count > 0)
                reducerViewPanel.Controls.Clear();

            ReducerView control = reducers[index].Control;
            if (control != null)
            {
                control.Reducer = curve.Reducer;
                control.Dock = DockStyle.Fill;
                reducerViewPanel.Controls.Add(control);
            }
        }
    }
}
