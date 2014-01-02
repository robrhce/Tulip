namespace Tulip
{
    partial class Main
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
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder2 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer2 = new XPTable.Renderers.DragDropRenderer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tblChannelStatus = new XPTable.Models.Table();
            this.tblChannelStatus_Columns = new XPTable.Models.ColumnModel();
            this.imageColumn1 = new XPTable.Models.ImageColumn();
            this.textColumn1 = new XPTable.Models.TextColumn();
            this.textColumn2 = new XPTable.Models.TextColumn();
            this.textColumn3 = new XPTable.Models.TextColumn();
            this.tblChannelStatus_Model = new XPTable.Models.TableModel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tblOutstationStatus = new XPTable.Models.Table();
            this.tblOutstationStatus_Columns = new XPTable.Models.ColumnModel();
            this.imageColumn2 = new XPTable.Models.ImageColumn();
            this.textColumn4 = new XPTable.Models.TextColumn();
            this.textColumn5 = new XPTable.Models.TextColumn();
            this.textColumn6 = new XPTable.Models.TextColumn();
            this.tblOutstationStatus_Model = new XPTable.Models.TableModel();
            this.button4 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outstationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblChannelStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblOutstationStatus)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.radioButton2);
            this.splitContainer1.Panel2.Controls.Add(this.radioButton1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(829, 449);
            this.splitContainer1.SplitterDistance = 616;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(6, 6);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(604, 437);
            this.splitContainer2.SplitterDistance = 201;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tblChannelStatus);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(604, 201);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channels";
            // 
            // tblChannelStatus
            // 
            this.tblChannelStatus.BorderColor = System.Drawing.Color.Black;
            this.tblChannelStatus.ColumnModel = this.tblChannelStatus_Columns;
            this.tblChannelStatus.DataMember = null;
            this.tblChannelStatus.DataSourceColumnBinder = dataSourceColumnBinder1;
            this.tblChannelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
            this.tblChannelStatus.DragDropRenderer = dragDropRenderer1;
            this.tblChannelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblChannelStatus.FullRowSelect = true;
            this.tblChannelStatus.GridLinesContrainedToData = false;
            this.tblChannelStatus.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblChannelStatus.Location = new System.Drawing.Point(10, 23);
            this.tblChannelStatus.Name = "tblChannelStatus";
            this.tblChannelStatus.ShowSelectionRectangle = false;
            this.tblChannelStatus.Size = new System.Drawing.Size(584, 168);
            this.tblChannelStatus.TabIndex = 0;
            this.tblChannelStatus.TableModel = this.tblChannelStatus_Model;
            this.tblChannelStatus.Text = "table1";
            this.tblChannelStatus.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // tblChannelStatus_Columns
            // 
            this.tblChannelStatus_Columns.Columns.AddRange(new XPTable.Models.Column[] {
            this.imageColumn1,
            this.textColumn1,
            this.textColumn2,
            this.textColumn3});
            this.tblChannelStatus_Columns.HeaderHeight = 30;
            // 
            // imageColumn1
            // 
            this.imageColumn1.IsTextTrimmed = false;
            // 
            // textColumn1
            // 
            this.textColumn1.Editable = false;
            this.textColumn1.IsTextTrimmed = false;
            this.textColumn1.Text = "ID";
            this.textColumn1.Width = 100;
            // 
            // textColumn2
            // 
            this.textColumn2.Editable = false;
            this.textColumn2.IsTextTrimmed = false;
            this.textColumn2.Text = "Name";
            this.textColumn2.Width = 150;
            // 
            // textColumn3
            // 
            this.textColumn3.AutoResizeMode = XPTable.Models.ColumnAutoResizeMode.Grow;
            this.textColumn3.Editable = false;
            this.textColumn3.IsTextTrimmed = false;
            this.textColumn3.Text = "Status";
            this.textColumn3.Width = 150;
            // 
            // tblChannelStatus_Model
            // 
            this.tblChannelStatus_Model.RowHeight = 30;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tblOutstationStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(604, 232);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Outstations";
            // 
            // tblOutstationStatus
            // 
            this.tblOutstationStatus.BorderColor = System.Drawing.Color.Black;
            this.tblOutstationStatus.ColumnModel = this.tblOutstationStatus_Columns;
            this.tblOutstationStatus.DataMember = null;
            this.tblOutstationStatus.DataSourceColumnBinder = dataSourceColumnBinder2;
            this.tblOutstationStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer2.ForeColor = System.Drawing.Color.Red;
            this.tblOutstationStatus.DragDropRenderer = dragDropRenderer2;
            this.tblOutstationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblOutstationStatus.GridLinesContrainedToData = false;
            this.tblOutstationStatus.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblOutstationStatus.Location = new System.Drawing.Point(10, 23);
            this.tblOutstationStatus.Name = "tblOutstationStatus";
            this.tblOutstationStatus.Size = new System.Drawing.Size(584, 199);
            this.tblOutstationStatus.TabIndex = 0;
            this.tblOutstationStatus.TableModel = this.tblOutstationStatus_Model;
            this.tblOutstationStatus.Text = "table1";
            this.tblOutstationStatus.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // tblOutstationStatus_Columns
            // 
            this.tblOutstationStatus_Columns.Columns.AddRange(new XPTable.Models.Column[] {
            this.imageColumn2,
            this.textColumn4,
            this.textColumn5,
            this.textColumn6});
            this.tblOutstationStatus_Columns.HeaderHeight = 30;
            // 
            // imageColumn2
            // 
            this.imageColumn2.IsTextTrimmed = false;
            // 
            // textColumn4
            // 
            this.textColumn4.IsTextTrimmed = false;
            this.textColumn4.Text = "ID";
            this.textColumn4.Width = 100;
            // 
            // textColumn5
            // 
            this.textColumn5.IsTextTrimmed = false;
            this.textColumn5.Text = "Name";
            this.textColumn5.Width = 150;
            // 
            // textColumn6
            // 
            this.textColumn6.IsTextTrimmed = false;
            this.textColumn6.Text = "Status";
            this.textColumn6.Width = 150;
            // 
            // tblOutstationStatus_Model
            // 
            this.tblOutstationStatus_Model.RowHeight = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(50, 345);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 32);
            this.button4.TabIndex = 6;
            this.button4.Text = "Write Bool";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(112, 311);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(69, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Latch Off";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(20, 311);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Latch On";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 272);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(50, 199);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Write Int";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(81, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "COS Poll";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Integ Poll";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(829, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton1.Text = "Start";
            this.toolStripButton1.ToolTipText = "Start";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton2.Text = "Stop";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelsToolStripMenuItem,
            this.outstationsToolStripMenuItem});
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(117, 22);
            this.toolStripButton3.Text = "Edit Configuration";
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.channelsToolStripMenuItem.Text = "Channels";
            this.channelsToolStripMenuItem.Click += new System.EventHandler(this.channelsToolStripMenuItem_Click);
            // 
            // outstationsToolStripMenuItem
            // 
            this.outstationsToolStripMenuItem.Name = "outstationsToolStripMenuItem";
            this.outstationsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.outstationsToolStripMenuItem.Text = "Outstations";
            this.outstationsToolStripMenuItem.Click += new System.EventHandler(this.outstationsToolStripMenuItem_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(63, 22);
            this.toolStripButton4.Text = "Show Log";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(93, 22);
            this.toolStripButton5.Text = "Point Summary";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 474);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Main";
            this.Text = "Main";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblChannelStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblOutstationStatus)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private XPTable.Models.Table tblChannelStatus;
        private XPTable.Models.ColumnModel tblChannelStatus_Columns;
        private XPTable.Models.ImageColumn imageColumn1;
        private XPTable.Models.TextColumn textColumn1;
        private XPTable.Models.TextColumn textColumn2;
        private XPTable.Models.TextColumn textColumn3;
        private XPTable.Models.TableModel tblChannelStatus_Model;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.GroupBox groupBox2;
        private XPTable.Models.Table tblOutstationStatus;
        private XPTable.Models.ColumnModel tblOutstationStatus_Columns;
        private XPTable.Models.ImageColumn imageColumn2;
        private XPTable.Models.TextColumn textColumn4;
        private XPTable.Models.TextColumn textColumn5;
        private XPTable.Models.TextColumn textColumn6;
        private XPTable.Models.TableModel tblOutstationStatus_Model;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outstationsToolStripMenuItem;
    }
}

