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
    public partial class ReducedBezierCurvesViewItem : UserControl
    {
        private bool selected = false;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                DetermineBackColor();
            }
        }

        private void DetermineBackColor()
        {
            if (selected)
                BackColor = Curve.Valid
                    ? SystemColors.Highlight
                    : Color.IndianRed;
            else
                BackColor = Curve.Valid
                    ? SystemColors.Control
                    : Color.Pink;
        }

        public ReducedBezierCurve Curve
        {
            get
            {
                return reducedBezierCurveBindingSource.DataSource
                    as ReducedBezierCurve;
            }
            set
            {
                reducedBezierCurveBindingSource.DataSource = value;
                Curve.PropertyChanged += Curve_PropertyChanged;
                DetermineBackColor();
            }
        }

        void Curve_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Valid")
                DetermineBackColor();
        }

        public ReducedBezierCurvesViewItem()
        {
            InitializeComponent();
            label.Click += (s, e) => OnClick(e);
        }
    }
}
