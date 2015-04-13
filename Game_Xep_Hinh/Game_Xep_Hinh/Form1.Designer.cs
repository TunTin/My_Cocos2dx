namespace Game_Xep_Hinh
{
    partial class frmChinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChinh));
            this.panel_Chinh = new System.Windows.Forms.Panel();
            this.panel_Phu = new System.Windows.Forms.Panel();
            this.lblAm_Thanh = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.panel_Tiep = new System.Windows.Forms.Panel();
            this.lblDiem = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblCap_Do = new System.Windows.Forms.Label();
            this.timer_Time = new System.Windows.Forms.Timer(this.components);
            this.timer_Gach = new System.Windows.Forms.Timer(this.components);
            this.panel_Phu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Chinh
            // 
            this.panel_Chinh.BackColor = System.Drawing.Color.Transparent;
            this.panel_Chinh.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Chinh.Location = new System.Drawing.Point(0, 0);
            this.panel_Chinh.Name = "panel_Chinh";
            this.panel_Chinh.Size = new System.Drawing.Size(260, 460);
            this.panel_Chinh.TabIndex = 0;
            this.panel_Chinh.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Chinh_Paint);
            // 
            // panel_Phu
            // 
            this.panel_Phu.BackColor = System.Drawing.Color.Transparent;
            this.panel_Phu.Controls.Add(this.lblAm_Thanh);
            this.panel_Phu.Controls.Add(this.lblHelp);
            this.panel_Phu.Controls.Add(this.panel_Tiep);
            this.panel_Phu.Controls.Add(this.lblDiem);
            this.panel_Phu.Controls.Add(this.lblTime);
            this.panel_Phu.Controls.Add(this.lblCap_Do);
            this.panel_Phu.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Phu.Location = new System.Drawing.Point(260, 0);
            this.panel_Phu.Name = "panel_Phu";
            this.panel_Phu.Size = new System.Drawing.Size(140, 460);
            this.panel_Phu.TabIndex = 1;
            // 
            // lblAm_Thanh
            // 
            this.lblAm_Thanh.BackColor = System.Drawing.Color.Transparent;
            this.lblAm_Thanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAm_Thanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblAm_Thanh.ForeColor = System.Drawing.Color.Yellow;
            this.lblAm_Thanh.Location = new System.Drawing.Point(80, 280);
            this.lblAm_Thanh.Name = "lblAm_Thanh";
            this.lblAm_Thanh.Size = new System.Drawing.Size(40, 40);
            this.lblAm_Thanh.TabIndex = 5;
            this.lblAm_Thanh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAm_Thanh.Click += new System.EventHandler(this.lblAm_Thanh_Click);
            // 
            // lblHelp
            // 
            this.lblHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblHelp.ForeColor = System.Drawing.Color.Yellow;
            this.lblHelp.Location = new System.Drawing.Point(20, 280);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(40, 40);
            this.lblHelp.TabIndex = 4;
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            // 
            // panel_Tiep
            // 
            this.panel_Tiep.BackColor = System.Drawing.Color.Transparent;
            this.panel_Tiep.Location = new System.Drawing.Point(20, 160);
            this.panel_Tiep.Name = "panel_Tiep";
            this.panel_Tiep.Size = new System.Drawing.Size(100, 100);
            this.panel_Tiep.TabIndex = 3;
            // 
            // lblDiem
            // 
            this.lblDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.lblDiem.Location = new System.Drawing.Point(20, 120);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(100, 20);
            this.lblDiem.TabIndex = 2;
            this.lblDiem.Text = "00000";
            this.lblDiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.lblTime.Location = new System.Drawing.Point(20, 80);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 20);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "00 : 00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCap_Do
            // 
            this.lblCap_Do.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCap_Do.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.lblCap_Do.Location = new System.Drawing.Point(20, 40);
            this.lblCap_Do.Name = "lblCap_Do";
            this.lblCap_Do.Size = new System.Drawing.Size(100, 20);
            this.lblCap_Do.TabIndex = 0;
            this.lblCap_Do.Text = "LEVEL : 99";
            this.lblCap_Do.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer_Time
            // 
            this.timer_Time.Interval = 1000;
            this.timer_Time.Tick += new System.EventHandler(this.timer_Time_Tick);
            // 
            // timer_Gach
            // 
            this.timer_Gach.Tick += new System.EventHandler(this.timer_Gach_Tick);
            // 
            // frmChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(400, 460);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Phu);
            this.Controls.Add(this.panel_Chinh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xếp Hình";
            this.Load += new System.EventHandler(this.frmChinh_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmChinh_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmChinh_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChinh_KeyDown);
            this.panel_Phu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Chinh;
        private System.Windows.Forms.Panel panel_Phu;
        private System.Windows.Forms.Label lblCap_Do;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.Panel panel_Tiep;
        private System.Windows.Forms.Timer timer_Time;
        private System.Windows.Forms.Timer timer_Gach;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblAm_Thanh;
    }
}

