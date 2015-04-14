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
    public partial class ReducedBezierCurvesView : UserControl
    {
        private BindingSource bindingSource;
        public object DataSource
        {
            get { return bindingSource; }
            set
            {
                if (bindingSource != null)
                    bindingSource.ListChanged -= bindingSource_ListChanged;
                bindingSource = value as BindingSource;
                if (bindingSource == null && value != null)
                    throw new NotImplementedException("This controll only "
                        + "accepts BindingSource as DataSource.");
                if (bindingSource != null)
                    bindingSource.ListChanged += bindingSource_ListChanged;
            }
        }

        private ReducedBezierCurvesViewItem selectedItem;
        private ReducedBezierCurvesViewItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    if (selectedItem != null)
                        selectedItem.Selected = false;
                    selectedItem = value;
                    if (selectedItem != null)
                        selectedItem.Selected = true;
                    if (SelectedCurveChanged != null)
                        SelectedCurveChanged();
                }
            }
        }
        public ReducedBezierCurve SelectedCurve
        {
            get
            {
                if (SelectedItem == null)
                    return null;
                else
                    return SelectedItem.Curve;
            }
        }

        public event Action SelectedCurveChanged;

        public void Select(int i)
        {
            SelectedItem = panel.Controls[i] as ReducedBezierCurvesViewItem; 
        }

        void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    AddCurve(bindingSource.List[e.NewIndex] as ReducedBezierCurve);
                    break;

                case ListChangedType.ItemDeleted:
                    if (SelectedItem == panel.Controls[e.NewIndex])
                        SelectedItem = null;
                    panel.Controls.RemoveAt(e.NewIndex);
                    RearrangeItems(e.NewIndex);
                    break;

                case ListChangedType.Reset:
                    panel.Controls.Clear();
                    foreach (object curve in bindingSource.List)
                        AddCurve(curve as ReducedBezierCurve);
                    break;
            }
        }

        private void AddCurve(ReducedBezierCurve curve)
        {
            ReducedBezierCurvesViewItem item = new ReducedBezierCurvesViewItem();
            item.Curve = curve;
            item.Click += item_Click;
            panel.Controls.Add(item);
            RearrangeItems(panel.Controls.Count - 1);
        }

        void item_Click(object sender, EventArgs e)
        {
            ReducedBezierCurvesViewItem newSelectedItem
                = sender as ReducedBezierCurvesViewItem;

            if (SelectedItem != newSelectedItem)
                SelectedItem = newSelectedItem;
            else SelectedItem = null;
        }

        public ReducedBezierCurvesView()
        {
            InitializeComponent();
        }

        private void RearrangeItems(int start)
        {
            if (panel.Controls.Count == 0) return;

            if (start == 0)
            {
                panel.Controls[0].Top = 0;
                ++start;
            }

            for (int i = start; i < panel.Controls.Count; ++i)
                panel.Controls[i].Top = panel.Controls[i - 1].Bottom;

            AdjustItemsWidths();
        }

        private void AdjustItemsWidths()
        {
            if (panel.Controls.Count == 0) return;
            int width = panel.Controls[panel.Controls.Count - 1].Bottom
                    - panel.Controls[0].Top > Height
                    ? Width - SystemInformation.VerticalScrollBarWidth
                    : Width;
            foreach (Control control in panel.Controls)
                control.Width = width;
        }

        private void ReducedBezierCurvesView_Resize(object sender, EventArgs e)
        {
            AdjustItemsWidths();
        }
    }
}
