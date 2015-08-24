namespace RealMotion_Create_Installer
{
    partial class Form1
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SerDir = new System.Windows.Forms.Button();
            this.tb_files = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.vb_version = new System.Windows.Forms.TextBox();
            this.bt_create = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_version = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_MainDir = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_server = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SerDir
            // 
            this.SerDir.Location = new System.Drawing.Point(499, 39);
            this.SerDir.Name = "SerDir";
            this.SerDir.Size = new System.Drawing.Size(82, 23);
            this.SerDir.TabIndex = 0;
            this.SerDir.Text = "Set Directory";
            this.SerDir.UseVisualStyleBackColor = true;
            this.SerDir.Click += new System.EventHandler(this.SerDir_Click);
            // 
            // tb_files
            // 
            this.tb_files.Location = new System.Drawing.Point(93, 41);
            this.tb_files.Name = "tb_files";
            this.tb_files.Size = new System.Drawing.Size(400, 20);
            this.tb_files.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Files Directory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Version No:";
            // 
            // vb_version
            // 
            this.vb_version.Location = new System.Drawing.Point(92, 123);
            this.vb_version.Name = "vb_version";
            this.vb_version.Size = new System.Drawing.Size(114, 20);
            this.vb_version.TabIndex = 6;
            // 
            // bt_create
            // 
            this.bt_create.Location = new System.Drawing.Point(474, 149);
            this.bt_create.Name = "bt_create";
            this.bt_create.Size = new System.Drawing.Size(106, 23);
            this.bt_create.TabIndex = 8;
            this.bt_create.Text = "Create and Upload";
            this.bt_create.UseVisualStyleBackColor = true;
            this.bt_create.Click += new System.EventHandler(this.bt_create_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current Version:";
            // 
            // lb_version
            // 
            this.lb_version.AutoSize = true;
            this.lb_version.Location = new System.Drawing.Point(99, 175);
            this.lb_version.Name = "lb_version";
            this.lb_version.Size = new System.Drawing.Size(31, 13);
            this.lb_version.TabIndex = 10;
            this.lb_version.Text = "0.0.0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 149);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(453, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Main Game";
            // 
            // tb_MainDir
            // 
            this.tb_MainDir.Location = new System.Drawing.Point(93, 5);
            this.tb_MainDir.Name = "tb_MainDir";
            this.tb_MainDir.Size = new System.Drawing.Size(400, 20);
            this.tb_MainDir.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(499, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Set Directory";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Server Directory";
            // 
            // tb_server
            // 
            this.tb_server.Location = new System.Drawing.Point(102, 80);
            this.tb_server.Name = "tb_server";
            this.tb_server.Size = new System.Drawing.Size(391, 20);
            this.tb_server.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Set Directory";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 201);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_server);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_MainDir);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lb_version);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bt_create);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vb_version);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_files);
            this.Controls.Add(this.SerDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "RealMotion Create Installer App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button SerDir;
        private System.Windows.Forms.TextBox tb_files;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox vb_version;
        private System.Windows.Forms.Button bt_create;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_version;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_MainDir;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_server;
        private System.Windows.Forms.Button button1;
    }
}

