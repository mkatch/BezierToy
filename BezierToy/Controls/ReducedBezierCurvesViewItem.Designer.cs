namespace BezierToy
{
    partial class ReducedBezierCurvesViewItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label = new System.Windows.Forms.Label();
            this.reducedBezierCurveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorView = new BezierToy.ColorView();
            this.degreeUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.reducedBezierCurveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.reducedBezierCurveBindingSource, "Label", true));
            this.label.Location = new System.Drawing.Point(30, 13);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(43, 13);
            this.label.TabIndex = 2;
            this.label.Text = "Method";
            // 
            // reducedBezierCurveBindingSource
            // 
            this.reducedBezierCurveBindingSource.DataSource = typeof(BezierToy.ReducedBezierCurve);
            // 
            // colorView
            // 
            this.colorView.BackColor = System.Drawing.Color.Red;
            this.colorView.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.reducedBezierCurveBindingSource, "Color", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colorView.Location = new System.Drawing.Point(11, 13);
            this.colorView.Name = "colorView";
            this.colorView.Size = new System.Drawing.Size(13, 13);
            this.colorView.TabIndex = 1;
            // 
            // degreeUpDown
            // 
            this.degreeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.degreeUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.reducedBezierCurveBindingSource, "Degree", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.degreeUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this.reducedBezierCurveBindingSource, "MaxDegree", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.degreeUpDown.Location = new System.Drawing.Point(201, 11);
            this.degreeUpDown.Name = "degreeUpDown";
            this.degreeUpDown.Size = new System.Drawing.Size(36, 20);
            this.degreeUpDown.TabIndex = 3;
            this.degreeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ReducedBezierCurvesViewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(20, 0);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.degreeUpDown);
            this.Controls.Add(this.label);
            this.Controls.Add(this.colorView);
            this.DoubleBuffered = true;
            this.Name = "ReducedBezierCurvesViewItem";
            this.Size = new System.Drawing.Size(248, 40);
            ((System.ComponentModel.ISupportInitialize)(this.reducedBezierCurveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorView colorView;
        private System.Windows.Forms.NumericUpDown degreeUpDown;
        private System.Windows.Forms.BindingSource reducedBezierCurveBindingSource;
        private System.Windows.Forms.Label label;
    }
}
