﻿
namespace LMCIS.Order_Management
{
    partial class staffInvoiceList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(staffInvoiceList));
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSorting = new System.Windows.Forms.ComboBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStaffContact = new System.Windows.Forms.Label();
            this.lblStaff = new System.Windows.Forms.Label();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.pnl_Invoice = new System.Windows.Forms.Panel();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblOrderID = new System.Windows.Forms.Label();
            this.lblNumberOfInvoiceShown = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(898, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 108;
            this.label2.Text = "Sort By:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSorting
            // 
            this.cmbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSorting.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSorting.FormattingEnabled = true;
            this.cmbSorting.Items.AddRange(new object[] {
            "All",
            "Invoice Number (Ascending)",
            "Invoice Number (Descending)",
            "Order Date (Nearest)",
            "Order Date (Furtherest)",
            "Order ID (Ascending)",
            "Order ID (Descending)",
            "Delivery Date (Nearest)",
            "Delivery Date (Furtherest)"});
            this.cmbSorting.Location = new System.Drawing.Point(962, 98);
            this.cmbSorting.Name = "cmbSorting";
            this.cmbSorting.Size = new System.Drawing.Size(196, 25);
            this.cmbSorting.TabIndex = 107;
            this.cmbSorting.SelectedIndexChanged += new System.EventHandler(this.cmbSorting_SelectedIndexChanged);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(207, 88);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(375, 36);
            this.lblHeading.TabIndex = 106;
            this.lblHeading.Text = "Invoices of Shipped Order";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(676, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 105;
            this.label4.Text = "Status:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All",
            "Confirmed",
            "Not Confirmed"});
            this.cmbStatus.Location = new System.Drawing.Point(734, 98);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(148, 25);
            this.cmbStatus.TabIndex = 104;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(901, 138);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 18);
            this.lblStatus.TabIndex = 103;
            this.lblStatus.Text = "Invoice Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStaffContact
            // 
            this.lblStaffContact.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffContact.Location = new System.Drawing.Point(743, 138);
            this.lblStaffContact.Name = "lblStaffContact";
            this.lblStaffContact.Size = new System.Drawing.Size(152, 18);
            this.lblStaffContact.TabIndex = 102;
            this.lblStaffContact.Text = "Delivery Date";
            this.lblStaffContact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStaff
            // 
            this.lblStaff.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(268, 138);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(141, 18);
            this.lblStaff.TabIndex = 100;
            this.lblStaff.Text = "Invoice Number";
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpOrder
            // 
            this.grpOrder.Controls.Add(this.pnl_Invoice);
            this.grpOrder.Location = new System.Drawing.Point(208, 153);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(950, 687);
            this.grpOrder.TabIndex = 101;
            this.grpOrder.TabStop = false;
            // 
            // pnl_Invoice
            // 
            this.pnl_Invoice.AutoScroll = true;
            this.pnl_Invoice.Location = new System.Drawing.Point(6, 9);
            this.pnl_Invoice.Name = "pnl_Invoice";
            this.pnl_Invoice.Size = new System.Drawing.Size(938, 672);
            this.pnl_Invoice.TabIndex = 0;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDate.Location = new System.Drawing.Point(584, 138);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(153, 18);
            this.lblOrderDate.TabIndex = 99;
            this.lblOrderDate.Text = "Order Date";
            this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderID
            // 
            this.lblOrderID.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderID.Location = new System.Drawing.Point(415, 138);
            this.lblOrderID.Name = "lblOrderID";
            this.lblOrderID.Size = new System.Drawing.Size(163, 18);
            this.lblOrderID.TabIndex = 98;
            this.lblOrderID.Text = "Order ID";
            this.lblOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberOfInvoiceShown
            // 
            this.lblNumberOfInvoiceShown.AutoSize = true;
            this.lblNumberOfInvoiceShown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfInvoiceShown.Location = new System.Drawing.Point(350, 843);
            this.lblNumberOfInvoiceShown.Name = "lblNumberOfInvoiceShown";
            this.lblNumberOfInvoiceShown.Size = new System.Drawing.Size(0, 20);
            this.lblNumberOfInvoiceShown.TabIndex = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 842);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 109;
            this.label1.Text = "Invoice(s) Shown: ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // palLoc
            // 
            this.palLoc.BackColor = System.Drawing.SystemColors.ControlLight;
            this.palLoc.Controls.Add(this.lblLoc);
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(198, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(972, 38);
            this.palLoc.TabIndex = 208;
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc.Location = new System.Drawing.Point(6, 9);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(173, 22);
            this.lblLoc.TabIndex = 0;
            this.lblLoc.Text = "Invoice Management";
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
            this.palDate.TabIndex = 207;
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
            this.pnlNav.TabIndex = 206;
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
            // staffInvoiceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palDate);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.lblNumberOfInvoiceShown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSorting);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStaffContact);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.lblOrderID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "staffInvoiceList";
            this.Text = "Legend Motor Company Integrated System";
            this.Load += new System.EventHandler(this.staffInvoiceList_Load);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSorting;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStaffContact;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.Panel pnl_Invoice;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.Label lblNumberOfInvoiceShown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
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