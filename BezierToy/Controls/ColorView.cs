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
    public partial class ColorView : UserControl
    {
        private static ColorDialog colorDialog = new ColorDialog();

        public ColorView()
        {
            InitializeComponent();
        }

        private void ColorView_Click(object sender, EventArgs e)
        {
            colorDialog.Color = BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog.Color;
            }
        }
    }
}
