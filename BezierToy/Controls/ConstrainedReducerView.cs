using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BezierToy
{
    public partial class ConstrainedReducerView : BezierToy.ReducerView
    {
        public ConstrainedReducerView()
        {
            InitializeComponent();
            ReducerChanged += ConstrainedReducerView_ReducerChanged;
        }

        void ConstrainedReducerView_ReducerChanged(Reducer oldReducer)
        {
            ConstrainedReducer constrainedReducer
                = Reducer as ConstrainedReducer;
            constrainedReducerBindingSource.DataSource = constrainedReducer;
        }
    }
}
