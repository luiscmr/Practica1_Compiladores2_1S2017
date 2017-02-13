namespace Practica1_Compiladores2_1S2017.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.bAdd = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bSaveAs = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.bExec = new System.Windows.Forms.Button();
            this.bReport = new System.Windows.Forms.Button();
            this.bAlbum = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.Image = ((System.Drawing.Image)(resources.GetObject("bAdd.Image")));
            this.bAdd.Location = new System.Drawing.Point(13, 12);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(55, 55);
            this.bAdd.TabIndex = 0;
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bOpen
            // 
            this.bOpen.Image = ((System.Drawing.Image)(resources.GetObject("bOpen.Image")));
            this.bOpen.Location = new System.Drawing.Point(74, 12);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(55, 55);
            this.bOpen.TabIndex = 1;
            this.bOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bSave
            // 
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.Location = new System.Drawing.Point(135, 12);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(55, 55);
            this.bSave.TabIndex = 2;
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bSaveAs
            // 
            this.bSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("bSaveAs.Image")));
            this.bSaveAs.Location = new System.Drawing.Point(196, 12);
            this.bSaveAs.Name = "bSaveAs";
            this.bSaveAs.Size = new System.Drawing.Size(55, 55);
            this.bSaveAs.TabIndex = 3;
            this.bSaveAs.UseVisualStyleBackColor = true;
            this.bSaveAs.Click += new System.EventHandler(this.bSaveAs_Click);
            // 
            // bClose
            // 
            this.bClose.Image = ((System.Drawing.Image)(resources.GetObject("bClose.Image")));
            this.bClose.Location = new System.Drawing.Point(257, 12);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(55, 55);
            this.bClose.TabIndex = 4;
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bExec
            // 
            this.bExec.Image = ((System.Drawing.Image)(resources.GetObject("bExec.Image")));
            this.bExec.Location = new System.Drawing.Point(318, 12);
            this.bExec.Name = "bExec";
            this.bExec.Size = new System.Drawing.Size(55, 55);
            this.bExec.TabIndex = 5;
            this.bExec.UseVisualStyleBackColor = true;
            this.bExec.Click += new System.EventHandler(this.bExec_Click);
            // 
            // bReport
            // 
            this.bReport.Image = ((System.Drawing.Image)(resources.GetObject("bReport.Image")));
            this.bReport.Location = new System.Drawing.Point(379, 12);
            this.bReport.Name = "bReport";
            this.bReport.Size = new System.Drawing.Size(55, 55);
            this.bReport.TabIndex = 6;
            this.bReport.UseVisualStyleBackColor = true;
            // 
            // bAlbum
            // 
            this.bAlbum.Image = ((System.Drawing.Image)(resources.GetObject("bAlbum.Image")));
            this.bAlbum.Location = new System.Drawing.Point(440, 12);
            this.bAlbum.Name = "bAlbum";
            this.bAlbum.Size = new System.Drawing.Size(55, 55);
            this.bAlbum.TabIndex = 7;
            this.bAlbum.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Location = new System.Drawing.Point(13, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 500);
            this.panel1.TabIndex = 8;
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(791, 491);
            this.tabControl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(820, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 562);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(12, 580);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1338, 149);
            this.panel3.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(521, 553);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bAlbum);
            this.Controls.Add(this.bReport);
            this.Controls.Add(this.bExec);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bSaveAs);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.bAdd);
            this.Name = "MainWindow";
            this.Text = "Practica 1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bSaveAs;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bExec;
        private System.Windows.Forms.Button bReport;
        private System.Windows.Forms.Button bAlbum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}