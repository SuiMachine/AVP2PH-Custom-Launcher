﻿namespace AVP_CustomLauncher
{
    partial class mainform
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Exit = new System.Windows.Forms.Button();
			this.B_DisplaySettings = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.B_StartGame = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.WSGFLink = new System.Windows.Forms.LinkLabel();
			this.projectPageLink = new System.Windows.Forms.LinkLabel();
			this.pcgwLink = new System.Windows.Forms.LinkLabel();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.B_Exit, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.B_DisplaySettings, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_StartGame, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 350);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// B_Exit
			// 
			this.B_Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Exit.AutoSize = true;
			this.B_Exit.Location = new System.Drawing.Point(109, 309);
			this.B_Exit.Name = "B_Exit";
			this.B_Exit.Size = new System.Drawing.Size(137, 37);
			this.B_Exit.TabIndex = 4;
			this.B_Exit.Text = "Exit";
			this.B_Exit.UseVisualStyleBackColor = true;
			this.B_Exit.Click += new System.EventHandler(this.B_Exit_Click);
			// 
			// B_DisplaySettings
			// 
			this.B_DisplaySettings.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_DisplaySettings.AutoSize = true;
			this.B_DisplaySettings.Location = new System.Drawing.Point(109, 265);
			this.B_DisplaySettings.Name = "B_DisplaySettings";
			this.B_DisplaySettings.Size = new System.Drawing.Size(137, 37);
			this.B_DisplaySettings.TabIndex = 2;
			this.B_DisplaySettings.Text = "Game settings";
			this.B_DisplaySettings.UseVisualStyleBackColor = true;
			this.B_DisplaySettings.Click += new System.EventHandler(this.B_DisplaySettings_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = global::AVP_CustomLauncher.Properties.Resources.yes1;
			this.pictureBox1.Location = new System.Drawing.Point(4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(348, 210);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// B_StartGame
			// 
			this.B_StartGame.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_StartGame.AutoSize = true;
			this.B_StartGame.Location = new System.Drawing.Point(109, 221);
			this.B_StartGame.Name = "B_StartGame";
			this.B_StartGame.Size = new System.Drawing.Size(137, 37);
			this.B_StartGame.TabIndex = 1;
			this.B_StartGame.Text = "Start the Game";
			this.B_StartGame.UseVisualStyleBackColor = true;
			this.B_StartGame.Click += new System.EventHandler(this.B_StartGame_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.WSGFLink);
			this.panel2.Controls.Add(this.projectPageLink);
			this.panel2.Controls.Add(this.pcgwLink);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 350);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(356, 55);
			this.panel2.TabIndex = 6;
			// 
			// WSGFLink
			// 
			this.WSGFLink.AutoSize = true;
			this.WSGFLink.Location = new System.Drawing.Point(2, 35);
			this.WSGFLink.Name = "WSGFLink";
			this.WSGFLink.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.WSGFLink.Size = new System.Drawing.Size(138, 16);
			this.WSGFLink.TabIndex = 4;
			this.WSGFLink.TabStop = true;
			this.WSGFLink.Text = "Wide-screen Gaming Forum";
			this.WSGFLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WSGFLink_LinkClicked);
			// 
			// projectPageLink
			// 
			this.projectPageLink.AutoSize = true;
			this.projectPageLink.Location = new System.Drawing.Point(2, 3);
			this.projectPageLink.Name = "projectPageLink";
			this.projectPageLink.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.projectPageLink.Size = new System.Drawing.Size(68, 16);
			this.projectPageLink.TabIndex = 1;
			this.projectPageLink.TabStop = true;
			this.projectPageLink.Text = "Project Page";
			this.projectPageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ProjectPageLink_LinkClicked);
			// 
			// pcgwLink
			// 
			this.pcgwLink.AutoSize = true;
			this.pcgwLink.Location = new System.Drawing.Point(2, 19);
			this.pcgwLink.Name = "pcgwLink";
			this.pcgwLink.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.pcgwLink.Size = new System.Drawing.Size(84, 16);
			this.pcgwLink.TabIndex = 0;
			this.pcgwLink.TabStop = true;
			this.pcgwLink.Text = "PC Gaming Wiki";
			this.pcgwLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PcgwLink_LinkClicked);
			// 
			// mainform
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(356, 405);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "mainform";
			this.ShowIcon = false;
			this.Text = "Aliens vs. Predator 2: Primal Hunt  - Custom Launcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
			this.Load += new System.EventHandler(this.Mainform_Load);
			this.LocationChanged += new System.EventHandler(this.Mainform_LocationChanged);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button B_DisplaySettings;
        private System.Windows.Forms.Button B_StartGame;
        private System.Windows.Forms.Button B_Exit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel WSGFLink;
        private System.Windows.Forms.LinkLabel projectPageLink;
        private System.Windows.Forms.LinkLabel pcgwLink;
    }
}

