﻿
namespace LMCIS.Order_Management
{
    partial class staffFeedbackList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(staffFeedbackList));
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblStaff = new System.Windows.Forms.Label();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.pnlFeedback = new System.Windows.Forms.Panel();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblOrderID = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblNumberOfFeedbackShown = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.palLoc = new System.Windows.Forms.Panel();
            this.lblLoc = new System.Windows.Forms.Label();
            this.palDate = new System.Windows.Forms.Panel();
            this.lblUid = new System.Windows.Forms.Label();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.palSelect5 = new System.Windows.Forms.Panel();
            this.palSelect4 = new System.Windows.Forms.Panel();
            this.palSelect2 = new System.Windows.Forms.Panel();
            this.palSelect1 = new System.Windows.Forms.Panel();
            this.picBWMode = new System.Windows.Forms.PictureBox();
            this.btnProFile = new System.Windows.Forms.Button();
            this.palSelect3 = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.lblCorpName = new System.Windows.Forms.Label();
            this.btnFunction5 = new System.Windows.Forms.Button();
            this.btnFunction4 = new System.Windows.Forms.Button();
            this.btnFunction3 = new System.Windows.Forms.Button();
            this.btnFunction2 = new System.Windows.Forms.Button();
            this.btnFunction1 = new System.Windows.Forms.Button();
            this.grpOrder.SuspendLayout();
            this.palLoc.SuspendLayout();
            this.palDate.SuspendLayout();
            this.pnlNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(288, 85);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(158, 36);
            this.lblHeading.TabIndex = 105;
            this.lblHeading.Text = "Feedbacks";
            // 
            // lblStaff
            // 
            this.lblStaff.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(484, 123);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(140, 18);
            this.lblStaff.TabIndex = 108;
            this.lblStaff.Text = "Customer ID";
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpOrder
            // 
            this.grpOrder.Controls.Add(this.pnlFeedback);
            this.grpOrder.Location = new System.Drawing.Point(266, 138);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(803, 690);
            this.grpOrder.TabIndex = 109;
            this.grpOrder.TabStop = false;
            // 
            // pnlFeedback
            // 
            this.pnlFeedback.AutoScroll = true;
            this.pnlFeedback.Location = new System.Drawing.Point(6, 9);
            this.pnlFeedback.Name = "pnlFeedback";
            this.pnlFeedback.Size = new System.Drawing.Size(789, 678);
            this.pnlFeedback.TabIndex = 0;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDate.Location = new System.Drawing.Point(630, 123);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(175, 18);
            this.lblOrderDate.TabIndex = 107;
            this.lblOrderDate.Text = "Feedback Date";
            this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderID
            // 
            this.lblOrderID.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderID.Location = new System.Drawing.Point(811, 123);
            this.lblOrderID.Name = "lblOrderID";
            this.lblOrderID.Size = new System.Drawing.Size(142, 18);
            this.lblOrderID.TabIndex = 106;
            this.lblOrderID.Text = "Order ID";
            this.lblOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(327, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 18);
            this.label5.TabIndex = 112;
            this.label5.Text = "Feedback ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberOfFeedbackShown
            // 
            this.lblNumberOfFeedbackShown.AutoSize = true;
            this.lblNumberOfFeedbackShown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfFeedbackShown.Location = new System.Drawing.Point(443, 838);
            this.lblNumberOfFeedbackShown.Name = "lblNumberOfFeedbackShown";
            this.lblNumberOfFeedbackShown.Size = new System.Drawing.Size(0, 20);
            this.lblNumberOfFeedbackShown.TabIndex = 114;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(268, 838);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 20);
            this.label7.TabIndex = 113;
            this.label7.Text = "Feedback(s) Shown: ";
            // 
            // btnReturn
            // 
            this.btnReturn.AutoSize = true;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(1064, 900);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(94, 29);
            this.btnReturn.TabIndex = 178;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // palLoc
            // 
            this.palLoc.BackColor = System.Drawing.SystemColors.ControlLight;
            this.palLoc.Controls.Add(this.lblLoc);
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(198, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(972, 38);
            this.palLoc.TabIndex = 181;
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc.Location = new System.Drawing.Point(6, 9);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(133, 22);
            this.lblLoc.TabIndex = 0;
            this.lblLoc.Text = "View Feedback";
            // 
            // palDate
            // 
            this.palDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.palDate.Controls.Add(this.lblUid);
            this.palDate.Controls.Add(this.lblTimeDate);
            this.palDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.palDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palDate.Location = new System.Drawing.Point(198, 0);
            this.palDate.Margin = new System.Windows.Forms.Padding(2);
            this.palDate.Name = "palDate";
            this.palDate.Size = new System.Drawing.Size(972, 40);
            this.palDate.TabIndex = 180;
            // 
            // lblUid
            // 
            this.lblUid.AutoSize = true;
            this.lblUid.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUid.Location = new System.Drawing.Point(814, 9);
            this.lblUid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(134, 22);
            this.lblUid.TabIndex = 1;
            this.lblUid.Text = "Uid: LMXXXX";
            // 
            // lblTimeDate
            // 
            this.lblTimeDate.AutoSize = true;
            this.lblTimeDate.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeDate.Location = new System.Drawing.Point(6, 9);
            this.lblTimeDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeDate.Name = "lblTimeDate";
            this.lblTimeDate.Size = new System.Drawing.Size(57, 22);
            this.lblTimeDate.TabIndex = 0;
            this.lblTimeDate.Text = "TIME";
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlNav.Controls.Add(this.palSelect5);
            this.pnlNav.Controls.Add(this.palSelect4);
            this.pnlNav.Controls.Add(this.palSelect2);
            this.pnlNav.Controls.Add(this.palSelect1);
            this.pnlNav.Controls.Add(this.picBWMode);
            this.pnlNav.Controls.Add(this.btnProFile);
            this.pnlNav.Controls.Add(this.palSelect3);
            this.pnlNav.Controls.Add(this.btnLogOut);
            this.pnlNav.Controls.Add(this.picHome);
            this.pnlNav.Controls.Add(this.lblCorpName);
            this.pnlNav.Controls.Add(this.btnFunction5);
            this.pnlNav.Controls.Add(this.btnFunction4);
            this.pnlNav.Controls.Add(this.btnFunction3);
            this.pnlNav.Controls.Add(this.btnFunction2);
            this.pnlNav.Controls.Add(this.btnFunction1);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0, 0);
            this.pnlNav.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(198, 941);
            this.pnlNav.TabIndex = 179;
            // 
            // palSelect5
            // 
            this.palSelect5.BackColor = System.Drawing.Color.Red;
            this.palSelect5.Location = new System.Drawing.Point(0, 371);
            this.palSelect5.Name = "palSelect5";
            this.palSelect5.Size = new System.Drawing.Size(10, 55);
            this.palSelect5.TabIndex = 4;
            this.palSelect5.Visible = false;
            // 
            // palSelect4
            // 
            this.palSelect4.BackColor = System.Drawing.Color.Red;
            this.palSelect4.Location = new System.Drawing.Point(0, 296);
            this.palSelect4.Name = "palSelect4";
            this.palSelect4.Size = new System.Drawing.Size(10, 55);
            this.palSelect4.TabIndex = 4;
            this.palSelect4.Visible = false;
            // 
            // palSelect2
            // 
            this.palSelect2.BackColor = System.Drawing.Color.Red;
            this.palSelect2.Location = new System.Drawing.Point(0, 150);
            this.palSelect2.Name = "palSelect2";
            this.palSelect2.Size = new System.Drawing.Size(10, 55);
            this.palSelect2.TabIndex = 4;
            this.palSelect2.Visible = false;
            // 
            // palSelect1
            // 
            this.palSelect1.BackColor = System.Drawing.Color.Red;
            this.palSelect1.Location = new System.Drawing.Point(0, 77);
            this.palSelect1.Name = "palSelect1";
            this.palSelect1.Size = new System.Drawing.Size(10, 55);
            this.palSelect1.TabIndex = 4;
            this.palSelect1.Visible = false;
            // 
            // picBWMode
            // 
            this.picBWMode.Image = global::LMCIS.Properties.Resources.LB;
            this.picBWMode.Location = new System.Drawing.Point(143, 24);
            this.picBWMode.Name = "picBWMode";
            this.picBWMode.Size = new System.Drawing.Size(49, 46);
            this.picBWMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBWMode.TabIndex = 22;
            this.picBWMode.TabStop = false;
            // 
            // btnProFile
            // 
            this.btnProFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnProFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnProFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProFile.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProFile.Location = new System.Drawing.Point(0, 794);
            this.btnProFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnProFile.Name = "btnProFile";
            this.btnProFile.Size = new System.Drawing.Size(198, 34);
            this.btnProFile.TabIndex = 20;
            this.btnProFile.Text = "ProFile";
            this.btnProFile.UseVisualStyleBackColor = false;
            this.btnProFile.Click += new System.EventHandler(this.btnProFile_Click);
            // 
            // palSelect3
            // 
            this.palSelect3.BackColor = System.Drawing.Color.Red;
            this.palSelect3.Location = new System.Drawing.Point(0, 223);
            this.palSelect3.Name = "palSelect3";
            this.palSelect3.Size = new System.Drawing.Size(10, 55);
            this.palSelect3.TabIndex = 3;
            this.palSelect3.Visible = false;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(0, 836);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(198, 34);
            this.btnLogOut.TabIndex = 19;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.picHome.Image = global::LMCIS.Properties.Resources.home;
            this.picHome.Location = new System.Drawing.Point(13, 13);
            this.picHome.Margin = new System.Windows.Forms.Padding(4);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(57, 56);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHome.TabIndex = 18;
            this.picHome.TabStop = false;
            this.picHome.Click += new System.EventHandler(this.picHome_Click);
            // 
            // lblCorpName
            // 
            this.lblCorpName.Font = new System.Drawing.Font("Times New Roman", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorpName.ForeColor = System.Drawing.Color.Red;
            this.lblCorpName.Location = new System.Drawing.Point(1, 892);
            this.lblCorpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorpName.Name = "lblCorpName";
            this.lblCorpName.Size = new System.Drawing.Size(163, 49);
            this.lblCorpName.TabIndex = 10;
            this.lblCorpName.Text = "Legend Motor Company";
            this.lblCorpName.Click += new System.EventHandler(this.lblCorpName_Click);
            // 
            // btnFunction5
            // 
            this.btnFunction5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction5.Location = new System.Drawing.Point(0, 371);
            this.btnFunction5.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction5.Name = "btnFunction5";
            this.btnFunction5.Size = new System.Drawing.Size(198, 55);
            this.btnFunction5.TabIndex = 15;
            this.btnFunction5.Text = "User Management";
            this.btnFunction5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction5.UseVisualStyleBackColor = false;
            this.btnFunction5.Click += new System.EventHandler(this.btnFunction5_Click);
            // 
            // btnFunction4
            // 
            this.btnFunction4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction4.Location = new System.Drawing.Point(0, 296);
            this.btnFunction4.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction4.Name = "btnFunction4";
            this.btnFunction4.Size = new System.Drawing.Size(198, 55);
            this.btnFunction4.TabIndex = 14;
            this.btnFunction4.Text = "Stock Management";
            this.btnFunction4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction4.UseVisualStyleBackColor = false;
            this.btnFunction4.Click += new System.EventHandler(this.btnFunction4_Click);
            // 
            // btnFunction3
            // 
            this.btnFunction3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction3.Location = new System.Drawing.Point(0, 223);
            this.btnFunction3.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction3.Name = "btnFunction3";
            this.btnFunction3.Size = new System.Drawing.Size(198, 55);
            this.btnFunction3.TabIndex = 13;
            this.btnFunction3.Text = "On-Sale Product Management";
            this.btnFunction3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction3.UseVisualStyleBackColor = false;
            this.btnFunction3.Click += new System.EventHandler(this.btnFunction3_Click);
            // 
            // btnFunction2
            // 
            this.btnFunction2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction2.Location = new System.Drawing.Point(0, 150);
            this.btnFunction2.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction2.Name = "btnFunction2";
            this.btnFunction2.Size = new System.Drawing.Size(198, 55);
            this.btnFunction2.TabIndex = 12;
            this.btnFunction2.Text = "Invoice Management";
            this.btnFunction2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction2.UseVisualStyleBackColor = false;
            this.btnFunction2.Click += new System.EventHandler(this.btnFunction2_Click);
            // 
            // btnFunction1
            // 
            this.btnFunction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction1.Location = new System.Drawing.Point(0, 77);
            this.btnFunction1.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction1.Name = "btnFunction1";
            this.btnFunction1.Size = new System.Drawing.Size(198, 55);
            this.btnFunction1.TabIndex = 11;
            this.btnFunction1.Text = "Order Management";
            this.btnFunction1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction1.UseVisualStyleBackColor = false;
            this.btnFunction1.Click += new System.EventHandler(this.btnFunction1_Click);
            // 
            // staffFeedbackList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palDate);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lblNumberOfFeedbackShown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.lblOrderID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "staffFeedbackList";
            this.Text = "Legend Motor Company Integrated System";
            this.Load += new System.EventHandler(this.staffFeedbackList_Load);
            this.grpOrder.ResumeLayout(false);
            this.palLoc.ResumeLayout(false);
            this.palLoc.PerformLayout();
            this.palDate.ResumeLayout(false);
            this.palDate.PerformLayout();
            this.pnlNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.Panel pnlFeedback;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNumberOfFeedbackShown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Panel palLoc;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Panel palDate;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.Label lblTimeDate;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.Panel palSelect5;
        private System.Windows.Forms.Panel palSelect4;
        private System.Windows.Forms.Panel palSelect2;
        private System.Windows.Forms.Panel palSelect1;
        private System.Windows.Forms.PictureBox picBWMode;
        private System.Windows.Forms.Button btnProFile;
        private System.Windows.Forms.Panel palSelect3;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Label lblCorpName;
        private System.Windows.Forms.Button btnFunction5;
        private System.Windows.Forms.Button btnFunction4;
        private System.Windows.Forms.Button btnFunction3;
        private System.Windows.Forms.Button btnFunction2;
        private System.Windows.Forms.Button btnFunction1;
    }
}