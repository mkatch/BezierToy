namespace BezierToy
{
    partial class ConstrainedReducerView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label contnuity0Label;
            System.Windows.Forms.Label continuity1Label;
            this.continuity0UpDown = new System.Windows.Forms.NumericUpDown();
            this.constrainedReducerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.continuity1UpDown = new System.Windows.Forms.NumericUpDown();
            contnuity0Label = new System.Windows.Forms.Label();
            continuity1Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.continuity0UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constrainedReducerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.continuity1UpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // contnuity0Label
            // 
            contnuity0Label.AutoSize = true;
            contnuity0Label.Location = new System.Drawing.Point(3, 3);
            contnuity0Label.Name = "contnuity0Label";
            contnuity0Label.Size = new System.Drawing.Size(77, 13);
            contnuity0Label.TabIndex = 2;
            contnuity0Label.Text = "Continuity at 0:";
            // 
            // continuity1Label
            // 
            continuity1Label.AutoSize = true;
            continuity1Label.Location = new System.Drawing.Point(3, 29);
            continuity1Label.Name = "continuity1Label";
            continuity1Label.Size = new System.Drawing.Size(77, 13);
            continuity1Label.TabIndex = 3;
            continuity1Label.Text = "Continuity at 1:";
            // 
            // continuity0UpDown
            // 
            this.continuity0UpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.constrainedReducerBindingSource, "ContinuityClassAt0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.continuity0UpDown.Location = new System.Drawing.Point(87, 1);
            this.continuity0UpDown.Name = "continuity0UpDown";
            this.continuity0UpDown.Size = new System.Drawing.Size(84, 20);
            this.continuity0UpDown.TabIndex = 0;
            // 
            // constrainedReducerBindingSource
            // 
            this.constrainedReducerBindingSource.DataSource = typeof(BezierToy.ConstrainedReducer);
            // 
            // continuity1UpDown
            // 
            this.continuity1UpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.constrainedReducerBindingSource, "ContinuityClassAt1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.continuity1UpDown.Location = new System.Drawing.Point(87, 27);
            this.continuity1UpDown.Name = "continuity1UpDown";
            this.continuity1UpDown.Size = new System.Drawing.Size(84, 20);
            this.continuity1UpDown.TabIndex = 1;
            // 
            // ConstrainedReducerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(continuity1Label);
            this.Controls.Add(contnuity0Label);
            this.Controls.Add(this.continuity1UpDown);
            this.Controls.Add(this.continuity0UpDown);
            this.Name = "ConstrainedReducerView";
            this.Size = new System.Drawing.Size(250, 48);
            ((System.ComponentModel.ISupportInitialize)(this.continuity0UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constrainedReducerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.continuity1UpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown continuity0UpDown;
        private System.Windows.Forms.NumericUpDown continuity1UpDown;
        private System.Windows.Forms.BindingSource constrainedReducerBindingSource;
    }
}
