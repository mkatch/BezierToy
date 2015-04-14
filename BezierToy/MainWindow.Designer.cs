namespace BezierToy
{
    partial class MainWindow
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
            System.Windows.Forms.Label baseCurveNameLabel;
            System.Windows.Forms.Label baseCurveDegreeLabel;
            System.Windows.Forms.Label selectedCurveLabel;
            this.modifyButton = new System.Windows.Forms.Button();
            this.dragButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asJPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asBMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dragToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reducedCurvesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.noneLabel = new System.Windows.Forms.Label();
            this.saveModelDialog = new System.Windows.Forms.SaveFileDialog();
            this.openModelDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.reducedBezierCurveDetailView = new BezierToy.ReducedBezierCurveDetailView();
            this.reducedBezierCurvesView = new BezierToy.ReducedBezierCurvesView();
            this.baseCurveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.baseCurveColorView = new BezierToy.ColorView();
            this.modelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            baseCurveNameLabel = new System.Windows.Forms.Label();
            baseCurveDegreeLabel = new System.Windows.Forms.Label();
            selectedCurveLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reducedCurvesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseCurveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // baseCurveNameLabel
            // 
            baseCurveNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            baseCurveNameLabel.AutoSize = true;
            baseCurveNameLabel.Location = new System.Drawing.Point(721, 92);
            baseCurveNameLabel.Name = "baseCurveNameLabel";
            baseCurveNameLabel.Size = new System.Drawing.Size(62, 13);
            baseCurveNameLabel.TabIndex = 12;
            baseCurveNameLabel.Text = "Base Curve";
            // 
            // baseCurveDegreeLabel
            // 
            baseCurveDegreeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            baseCurveDegreeLabel.AutoSize = true;
            baseCurveDegreeLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.baseCurveBindingSource, "Degree", true));
            baseCurveDegreeLabel.Location = new System.Drawing.Point(890, 92);
            baseCurveDegreeLabel.MinimumSize = new System.Drawing.Size(26, 0);
            baseCurveDegreeLabel.Name = "baseCurveDegreeLabel";
            baseCurveDegreeLabel.Size = new System.Drawing.Size(26, 13);
            baseCurveDegreeLabel.TabIndex = 5;
            baseCurveDegreeLabel.Text = "0";
            baseCurveDegreeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // selectedCurveLabel
            // 
            selectedCurveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            selectedCurveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            selectedCurveLabel.Location = new System.Drawing.Point(691, 457);
            selectedCurveLabel.Name = "selectedCurveLabel";
            selectedCurveLabel.Size = new System.Drawing.Size(250, 24);
            selectedCurveLabel.TabIndex = 18;
            selectedCurveLabel.Text = "Selected Curve";
            selectedCurveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modifyButton
            // 
            this.modifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modifyButton.Location = new System.Drawing.Point(691, 27);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(250, 23);
            this.modifyButton.TabIndex = 1;
            this.modifyButton.Text = "Modify";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // dragButton
            // 
            this.dragButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dragButton.Location = new System.Drawing.Point(691, 56);
            this.dragButton.Name = "dragButton";
            this.dragButton.Size = new System.Drawing.Size(250, 23);
            this.dragButton.TabIndex = 2;
            this.dragButton.Text = "Drag";
            this.dragButton.UseVisualStyleBackColor = true;
            this.dragButton.Click += new System.EventHandler(this.dragButton_Click);
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.canvas.Location = new System.Drawing.Point(12, 27);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(673, 586);
            this.canvas.TabIndex = 3;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseEnter += new System.EventHandler(this.canvas_MouseEnter);
            this.canvas.MouseLeave += new System.EventHandler(this.canvas_MouseLeave);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            this.canvas.Resize += new System.EventHandler(this.canvas_Resize);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(953, 24);
            this.menu.TabIndex = 4;
            this.menu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asJPGToolStripMenuItem,
            this.asPNGToolStripMenuItem,
            this.asBMPToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // asJPGToolStripMenuItem
            // 
            this.asJPGToolStripMenuItem.Name = "asJPGToolStripMenuItem";
            this.asJPGToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.asJPGToolStripMenuItem.Text = "As JPEG";
            this.asJPGToolStripMenuItem.Click += new System.EventHandler(this.asJPGToolStripMenuItem_Click);
            // 
            // asPNGToolStripMenuItem
            // 
            this.asPNGToolStripMenuItem.Name = "asPNGToolStripMenuItem";
            this.asPNGToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.asPNGToolStripMenuItem.Text = "As PNG";
            this.asPNGToolStripMenuItem.Click += new System.EventHandler(this.asPNGToolStripMenuItem_Click);
            // 
            // asBMPToolStripMenuItem
            // 
            this.asBMPToolStripMenuItem.Name = "asBMPToolStripMenuItem";
            this.asBMPToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.asBMPToolStripMenuItem.Text = "As BMP";
            this.asBMPToolStripMenuItem.Click += new System.EventHandler(this.asBMPToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeToolStripMenuItem.Text = "Exit";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator4,
            this.modifyToolStripMenuItem,
            this.dragToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.modifyToolStripMenuItem.Text = "Modify";
            this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // dragToolStripMenuItem
            // 
            this.dragToolStripMenuItem.Name = "dragToolStripMenuItem";
            this.dragToolStripMenuItem.ShortcutKeyDisplayString = "Space";
            this.dragToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.dragToolStripMenuItem.Text = "Drag";
            this.dragToolStripMenuItem.Click += new System.EventHandler(this.dragToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerCurveToolStripMenuItem,
            this.controlPointsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // centerCurveToolStripMenuItem
            // 
            this.centerCurveToolStripMenuItem.Name = "centerCurveToolStripMenuItem";
            this.centerCurveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.centerCurveToolStripMenuItem.Text = "Center";
            this.centerCurveToolStripMenuItem.Click += new System.EventHandler(this.centerCurveToolStripMenuItem_Click);
            // 
            // controlPointsToolStripMenuItem
            // 
            this.controlPointsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.connectToolStripMenuItem});
            this.controlPointsToolStripMenuItem.Name = "controlPointsToolStripMenuItem";
            this.controlPointsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.controlPointsToolStripMenuItem.Text = "Control Points";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Checked = true;
            this.showToolStripMenuItem.CheckOnClick = true;
            this.showToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Checked = true;
            this.connectToolStripMenuItem.CheckOnClick = true;
            this.connectToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // reducedCurvesBindingSource
            // 
            this.reducedCurvesBindingSource.DataMember = "ReducedCurves";
            this.reducedCurvesBindingSource.DataSource = this.modelBindingSource;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(691, 431);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(123, 23);
            this.removeButton.TabIndex = 14;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(820, 431);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(121, 23);
            this.addButton.TabIndex = 15;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // noneLabel
            // 
            this.noneLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.noneLabel.Location = new System.Drawing.Point(691, 484);
            this.noneLabel.Name = "noneLabel";
            this.noneLabel.Size = new System.Drawing.Size(250, 129);
            this.noneLabel.TabIndex = 0;
            this.noneLabel.Text = "None";
            this.noneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveModelDialog
            // 
            this.saveModelDialog.DefaultExt = "bzt";
            this.saveModelDialog.FileName = "untitled";
            this.saveModelDialog.Filter = "BezierToy files|*.bzt|All files|*.*";
            // 
            // openModelDialog
            // 
            this.openModelDialog.Filter = "BezierToy files|*.bzt|All files|*.*";
            // 
            // reducedBezierCurveDetailView
            // 
            this.reducedBezierCurveDetailView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reducedBezierCurveDetailView.Curve = null;
            this.reducedBezierCurveDetailView.Location = new System.Drawing.Point(691, 484);
            this.reducedBezierCurveDetailView.MethodsDisplayMember = "Name";
            this.reducedBezierCurveDetailView.Name = "reducedBezierCurveDetailView";
            this.reducedBezierCurveDetailView.Reducers = null;
            this.reducedBezierCurveDetailView.Size = new System.Drawing.Size(250, 129);
            this.reducedBezierCurveDetailView.TabIndex = 1;
            // 
            // reducedBezierCurvesView
            // 
            this.reducedBezierCurvesView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reducedBezierCurvesView.DataSource = null;
            this.reducedBezierCurvesView.Location = new System.Drawing.Point(691, 119);
            this.reducedBezierCurvesView.Name = "reducedBezierCurvesView";
            this.reducedBezierCurvesView.Size = new System.Drawing.Size(250, 306);
            this.reducedBezierCurvesView.TabIndex = 13;
            this.reducedBezierCurvesView.SelectedCurveChanged += new System.Action(this.reducedBezierCurvesView_SelectedCurveChanged);
            // 
            // baseCurveBindingSource
            // 
            this.baseCurveBindingSource.DataSource = typeof(BezierToy.BezierCurve);
            // 
            // baseCurveColorView
            // 
            this.baseCurveColorView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.baseCurveColorView.BackColor = System.Drawing.Color.Red;
            this.baseCurveColorView.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.baseCurveBindingSource, "Color", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.baseCurveColorView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.baseCurveColorView.Location = new System.Drawing.Point(703, 92);
            this.baseCurveColorView.Name = "baseCurveColorView";
            this.baseCurveColorView.Size = new System.Drawing.Size(13, 13);
            this.baseCurveColorView.TabIndex = 11;
            // 
            // modelBindingSource
            // 
            this.modelBindingSource.DataSource = typeof(BezierToy.Model);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 625);
            this.Controls.Add(this.reducedBezierCurveDetailView);
            this.Controls.Add(selectedCurveLabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.reducedBezierCurvesView);
            this.Controls.Add(baseCurveDegreeLabel);
            this.Controls.Add(baseCurveNameLabel);
            this.Controls.Add(this.baseCurveColorView);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.dragButton);
            this.Controls.Add(this.modifyButton);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.noneLabel);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "BezierToy";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reducedCurvesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseCurveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.Button dragButton;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asJPGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asBMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dragToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerCurveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.PictureBox canvas;
        private ColorView baseCurveColorView;
        private System.Windows.Forms.BindingSource baseCurveBindingSource;
        private System.Windows.Forms.BindingSource modelBindingSource;
        private ReducedBezierCurvesView reducedBezierCurvesView;
        private System.Windows.Forms.BindingSource reducedCurvesBindingSource;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label noneLabel;
        private ReducedBezierCurveDetailView reducedBezierCurveDetailView;
        private System.Windows.Forms.SaveFileDialog saveModelDialog;
        private System.Windows.Forms.OpenFileDialog openModelDialog;
        private System.Windows.Forms.SaveFileDialog exportDialog;
    }
}

