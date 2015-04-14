namespace BezierToy
{
    partial class ReducedBezierCurveDetailView
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
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.methodLabel = new System.Windows.Forms.Label();
            this.degreeLabel = new System.Windows.Forms.Label();
            this.degreeUpDown = new System.Windows.Forms.NumericUpDown();
            this.reducedBezierCurveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorLabel = new System.Windows.Forms.Label();
            this.reducerViewPanel = new System.Windows.Forms.Panel();
            this.colorView = new BezierToy.ColorView();
            ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reducedBezierCurveBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // methodComboBox
            // 
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Location = new System.Drawing.Point(87, 0);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(163, 21);
            this.methodComboBox.TabIndex = 0;
            this.methodComboBox.SelectedIndexChanged += new System.EventHandler(this.methodComboBox_SelectedIndexChanged);
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(3, 3);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(46, 13);
            this.methodLabel.TabIndex = 1;
            this.methodLabel.Text = "Method:";
            // 
            // degreeLabel
            // 
            this.degreeLabel.AutoSize = true;
            this.degreeLabel.Location = new System.Drawing.Point(3, 30);
            this.degreeLabel.Name = "degreeLabel";
            this.degreeLabel.Size = new System.Drawing.Size(45, 13);
            this.degreeLabel.TabIndex = 2;
            this.degreeLabel.Text = "Degree:";
            // 
            // degreeUpDown
            // 
            this.degreeUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.reducedBezierCurveBindingSource, "Degree", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.degreeUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this.reducedBezierCurveBindingSource, "MaxDegree", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.degreeUpDown.Location = new System.Drawing.Point(87, 28);
            this.degreeUpDown.Name = "degreeUpDown";
            this.degreeUpDown.Size = new System.Drawing.Size(84, 20);
            this.degreeUpDown.TabIndex = 3;
            // 
            // reducedBezierCurveBindingSource
            // 
            this.reducedBezierCurveBindingSource.DataSource = typeof(BezierToy.ReducedBezierCurve);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(3, 59);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(34, 13);
            this.colorLabel.TabIndex = 5;
            this.colorLabel.Text = "Color:";
            // 
            // reducerViewPanel
            // 
            this.reducerViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reducerViewPanel.Location = new System.Drawing.Point(0, 81);
            this.reducerViewPanel.Name = "reducerViewPanel";
            this.reducerViewPanel.Size = new System.Drawing.Size(250, 48);
            this.reducerViewPanel.TabIndex = 6;
            // 
            // colorView
            // 
            this.colorView.BackColor = System.Drawing.Color.Red;
            this.colorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorView.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.reducedBezierCurveBindingSource, "Color", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colorView.Location = new System.Drawing.Point(87, 55);
            this.colorView.Name = "colorView";
            this.colorView.Size = new System.Drawing.Size(84, 20);
            this.colorView.TabIndex = 4;
            // 
            // ReducedBezierCurveDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reducerViewPanel);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.colorView);
            this.Controls.Add(this.degreeUpDown);
            this.Controls.Add(this.degreeLabel);
            this.Controls.Add(this.methodLabel);
            this.Controls.Add(this.methodComboBox);
            this.Name = "ReducedBezierCurveDetailView";
            this.Size = new System.Drawing.Size(250, 129);
            ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reducedBezierCurveBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label methodLabel;
        private System.Windows.Forms.Label degreeLabel;
        private System.Windows.Forms.NumericUpDown degreeUpDown;
        private ColorView colorView;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Panel reducerViewPanel;
        private System.Windows.Forms.BindingSource reducedBezierCurveBindingSource;
    }
}
