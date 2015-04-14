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
    public partial class ReducerView : UserControl
    {
        Reducer reducer;
        public Reducer Reducer
        {
            get { return reducer; }
            set
            {
                if (reducer != value)
                {
                    Reducer oldReducer = reducer;
                    reducer = value;
                    if (ReducerChanged != null)
                        ReducerChanged(oldReducer);
                }
            }
        }

        protected delegate void ReducerChangedHandler(Reducer oldReducer);

        protected event ReducerChangedHandler ReducerChanged;

        public ReducerView()
        {
            InitializeComponent();
        }
    }
}
