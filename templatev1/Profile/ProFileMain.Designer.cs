namespace templatev1
{
    partial class proFileMain
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
            this.palNav = new System.Windows.Forms.Panel();
            this.picBWMode = new System.Windows.Forms.PictureBox();
            this.palSelect = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.lblCorpName = new System.Windows.Forms.Label();
            this.btnProFile = new System.Windows.Forms.Button();
            this.btnFunction5 = new System.Windows.Forms.Button();
            this.btnFunction4 = new System.Windows.Forms.Button();
            this.btnFunction2 = new System.Windows.Forms.Button();
            this.btnFunction1 = new System.Windows.Forms.Button();
            this.btnFunction3 = new System.Windows.Forms.Button();
            this.palTime = new System.Windows.Forms.Panel();
            this.lblUid = new System.Windows.Forms.Label();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.palLoc = new System.Windows.Forms.Panel();
            this.lblLoc = new System.Windows.Forms.Label();
            this.btnUploadIMG = new System.Windows.Forms.Button();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblTitGender = new System.Windows.Forms.Label();
            this.lblTitPhone = new System.Windows.Forms.Label();
            this.lblTitEmail = new System.Windows.Forms.Label();
            this.lblTitAccType = new System.Windows.Forms.Label();
            this.grpContact = new System.Windows.Forms.GroupBox();
            this.lblPhoneMsg = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTitCorpAdd = new System.Windows.Forms.Label();
            this.lblWareAddress = new System.Windows.Forms.Label();
            this.btnManagAddress = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAccType = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.lblTitDateOfBirth = new System.Windows.Forms.Label();
            this.chkNGDateOfBirth = new System.Windows.Forms.CheckBox();
            this.lblTitCreateDate = new System.Windows.Forms.Label();
            this.lblTitUID = new System.Windows.Forms.Label();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.lblUserUID = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblTitPayment = new System.Windows.Forms.Label();
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.btnRemoveIMG = new System.Windows.Forms.Button();
            this.grpPass = new System.Windows.Forms.GroupBox();
            this.lblPwdMsg = new System.Windows.Forms.Label();
            this.tbOldPass = new System.Windows.Forms.TextBox();
            this.lblTitOldPass = new System.Windows.Forms.Label();
            this.lblTitPass = new System.Windows.Forms.Label();
            this.tbConfirmPass = new System.Windows.Forms.TextBox();
            this.lblTitConfirmPass = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.picUserIMG = new System.Windows.Forms.PictureBox();
            this.lblTitDept = new System.Windows.Forms.Label();
            this.lblTitJobTitle = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblJobTitle = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblTitWareAdd = new System.Windows.Forms.Label();
            this.lblCorpAddress = new System.Windows.Forms.Label();
            this.lblTItCCorpName = new System.Windows.Forms.Label();
            this.tbCorp = new System.Windows.Forms.TextBox();
            this.lblfNameMsg = new System.Windows.Forms.Label();
            this.lbllNameMsg = new System.Windows.Forms.Label();
            this.lblSexMsg = new System.Windows.Forms.Label();
            this.lblDateMsg = new System.Windows.Forms.Label();
            this.lblPayMsg = new System.Windows.Forms.Label();
            this.lblContactMsg = new System.Windows.Forms.Label();
            this.btnChangePwd = new System.Windows.Forms.Button();
            this.palNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.palTime.SuspendLayout();
            this.palLoc.SuspendLayout();
            this.grpContact.SuspendLayout();
            this.grpPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // palNav
            // 
            this.palNav.BackColor = global::templatev1.Properties.Settings.Default.navColor;
            this.palNav.Controls.Add(this.picBWMode);
            this.palNav.Controls.Add(this.palSelect);
            this.palNav.Controls.Add(this.btnLogOut);
            this.palNav.Controls.Add(this.picHome);
            this.palNav.Controls.Add(this.lblCorpName);
            this.palNav.Controls.Add(this.btnProFile);
            this.palNav.Controls.Add(this.btnFunction5);
            this.palNav.Controls.Add(this.btnFunction4);
            this.palNav.Controls.Add(this.btnFunction2);
            this.palNav.Controls.Add(this.btnFunction1);
            this.palNav.Controls.Add(this.btnFunction3);
            this.palNav.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.palNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.palNav.Location = new System.Drawing.Point(0, 0);
            this.palNav.Margin = new System.Windows.Forms.Padding(2);
            this.palNav.Name = "palNav";
            this.palNav.Size = new System.Drawing.Size(198, 941);
            this.palNav.TabIndex = 0;
            // 
            // picBWMode
            // 
            this.picBWMode.Image = global::templatev1.Properties.Resources.LB;
            this.picBWMode.Location = new System.Drawing.Point(143, 24);
            this.picBWMode.Name = "picBWMode";
            this.picBWMode.Size = new System.Drawing.Size(49, 46);
            this.picBWMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBWMode.TabIndex = 21;
            this.picBWMode.TabStop = false;
            this.picBWMode.Click += new System.EventHandler(this.picBWMode_Click);
            // 
            // palSelect
            // 
            this.palSelect.BackColor = System.Drawing.Color.Red;
            this.palSelect.Location = new System.Drawing.Point(0, 794);
            this.palSelect.Name = "palSelect";
            this.palSelect.Size = new System.Drawing.Size(10, 34);
            this.palSelect.TabIndex = 3;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = global::templatev1.Properties.Settings.Default.logoutColor;
            this.btnLogOut.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "logoutColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnLogOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogOut.FlatAppearance.BorderSize = 0;
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
            this.picHome.Image = global::templatev1.Properties.Resources.home;
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
            // btnProFile
            // 
            this.btnProFile.BackColor = global::templatev1.Properties.Settings.Default.profileColor;
            this.btnProFile.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "profileColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnProFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnProFile.FlatAppearance.BorderSize = 0;
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
            // btnFunction5
            // 
            this.btnFunction5.BackColor = global::templatev1.Properties.Settings.Default.navBarColor;
            this.btnFunction5.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navBarColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFunction5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction5.FlatAppearance.BorderSize = 0;
            this.btnFunction5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction5.Location = new System.Drawing.Point(0, 371);
            this.btnFunction5.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction5.Name = "btnFunction5";
            this.btnFunction5.Size = new System.Drawing.Size(198, 55);
            this.btnFunction5.TabIndex = 87;
            this.btnFunction5.Text = "User Management";
            this.btnFunction5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction5.UseVisualStyleBackColor = false;
            // 
            // btnFunction4
            // 
            this.btnFunction4.BackColor = global::templatev1.Properties.Settings.Default.navBarColor;
            this.btnFunction4.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navBarColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFunction4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction4.FlatAppearance.BorderSize = 0;
            this.btnFunction4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction4.Location = new System.Drawing.Point(0, 296);
            this.btnFunction4.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction4.Name = "btnFunction4";
            this.btnFunction4.Size = new System.Drawing.Size(198, 55);
            this.btnFunction4.TabIndex = 86;
            this.btnFunction4.Text = "Stock Management";
            this.btnFunction4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction4.UseVisualStyleBackColor = false;
            // 
            // btnFunction2
            // 
            this.btnFunction2.BackColor = global::templatev1.Properties.Settings.Default.navBarColor;
            this.btnFunction2.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navBarColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFunction2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction2.FlatAppearance.BorderSize = 0;
            this.btnFunction2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction2.Location = new System.Drawing.Point(0, 150);
            this.btnFunction2.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction2.Name = "btnFunction2";
            this.btnFunction2.Size = new System.Drawing.Size(198, 55);
            this.btnFunction2.TabIndex = 84;
            this.btnFunction2.Text = "Invoice Management";
            this.btnFunction2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction2.UseVisualStyleBackColor = false;
            // 
            // btnFunction1
            // 
            this.btnFunction1.BackColor = global::templatev1.Properties.Settings.Default.navBarColor;
            this.btnFunction1.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navBarColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFunction1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction1.FlatAppearance.BorderSize = 0;
            this.btnFunction1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction1.Location = new System.Drawing.Point(0, 77);
            this.btnFunction1.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction1.Name = "btnFunction1";
            this.btnFunction1.Size = new System.Drawing.Size(198, 55);
            this.btnFunction1.TabIndex = 83;
            this.btnFunction1.Text = "Order Management";
            this.btnFunction1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction1.UseVisualStyleBackColor = false;
            // 
            // btnFunction3
            // 
            this.btnFunction3.BackColor = global::templatev1.Properties.Settings.Default.navBarColor;
            this.btnFunction3.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "navBarColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFunction3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction3.FlatAppearance.BorderSize = 0;
            this.btnFunction3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction3.Location = new System.Drawing.Point(0, 223);
            this.btnFunction3.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction3.Name = "btnFunction3";
            this.btnFunction3.Size = new System.Drawing.Size(198, 55);
            this.btnFunction3.TabIndex = 13;
            this.btnFunction3.Text = "On-Sale Product Management";
            this.btnFunction3.UseVisualStyleBackColor = false;
            // 
            // palTime
            // 
            this.palTime.BackColor = global::templatev1.Properties.Settings.Default.timeColor;
            this.palTime.Controls.Add(this.lblUid);
            this.palTime.Controls.Add(this.lblTimeDate);
            this.palTime.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "timeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.palTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.palTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palTime.Location = new System.Drawing.Point(198, 0);
            this.palTime.Margin = new System.Windows.Forms.Padding(2);
            this.palTime.Name = "palTime";
            this.palTime.Size = new System.Drawing.Size(972, 40);
            this.palTime.TabIndex = 1;
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // palLoc
            // 
            this.palLoc.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.palLoc.Controls.Add(this.lblLoc);
            this.palLoc.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(198, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(972, 38);
            this.palLoc.TabIndex = 2;
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc.Location = new System.Drawing.Point(6, 9);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(70, 22);
            this.lblLoc.TabIndex = 0;
            this.lblLoc.Text = "ProFile";
            // 
            // btnUploadIMG
            // 
            this.btnUploadIMG.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnUploadIMG.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnUploadIMG.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnUploadIMG.Location = new System.Drawing.Point(1016, 174);
            this.btnUploadIMG.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadIMG.Name = "btnUploadIMG";
            this.btnUploadIMG.Size = new System.Drawing.Size(90, 73);
            this.btnUploadIMG.TabIndex = 20;
            this.btnUploadIMG.Text = "Upload Image";
            this.btnUploadIMG.UseVisualStyleBackColor = false;
            this.btnUploadIMG.Click += new System.EventHandler(this.btnUploadIMG_Click);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(244, 231);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(86, 19);
            this.lblFirstName.TabIndex = 20;
            this.lblFirstName.Text = "FirstName:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(244, 272);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(83, 19);
            this.lblLastName.TabIndex = 21;
            this.lblLastName.Text = "LastName:";
            // 
            // lblTitGender
            // 
            this.lblTitGender.AutoSize = true;
            this.lblTitGender.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitGender.Location = new System.Drawing.Point(244, 313);
            this.lblTitGender.Name = "lblTitGender";
            this.lblTitGender.Size = new System.Drawing.Size(64, 19);
            this.lblTitGender.TabIndex = 22;
            this.lblTitGender.Text = "Gender:";
            // 
            // lblTitPhone
            // 
            this.lblTitPhone.AutoSize = true;
            this.lblTitPhone.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitPhone.Location = new System.Drawing.Point(20, 38);
            this.lblTitPhone.Name = "lblTitPhone";
            this.lblTitPhone.Size = new System.Drawing.Size(57, 19);
            this.lblTitPhone.TabIndex = 23;
            this.lblTitPhone.Text = "Phone:";
            // 
            // lblTitEmail
            // 
            this.lblTitEmail.AutoSize = true;
            this.lblTitEmail.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitEmail.Location = new System.Drawing.Point(20, 79);
            this.lblTitEmail.Name = "lblTitEmail";
            this.lblTitEmail.Size = new System.Drawing.Size(112, 19);
            this.lblTitEmail.TabIndex = 24;
            this.lblTitEmail.Text = "Email Address:";
            // 
            // lblTitAccType
            // 
            this.lblTitAccType.AutoSize = true;
            this.lblTitAccType.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitAccType.Location = new System.Drawing.Point(242, 100);
            this.lblTitAccType.Name = "lblTitAccType";
            this.lblTitAccType.Size = new System.Drawing.Size(107, 19);
            this.lblTitAccType.TabIndex = 25;
            this.lblTitAccType.Text = "Account Type:";
            // 
            // grpContact
            // 
            this.grpContact.Controls.Add(this.lblPhoneMsg);
            this.grpContact.Controls.Add(this.tbPhone);
            this.grpContact.Controls.Add(this.lblEmail);
            this.grpContact.Controls.Add(this.lblTitPhone);
            this.grpContact.Controls.Add(this.lblTitEmail);
            this.grpContact.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.grpContact.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContact.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.grpContact.Location = new System.Drawing.Point(245, 571);
            this.grpContact.Name = "grpContact";
            this.grpContact.Size = new System.Drawing.Size(479, 139);
            this.grpContact.TabIndex = 26;
            this.grpContact.TabStop = false;
            this.grpContact.Text = "Contact";
            // 
            // lblPhoneMsg
            // 
            this.lblPhoneMsg.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblPhoneMsg.ForeColor = System.Drawing.Color.Red;
            this.lblPhoneMsg.Location = new System.Drawing.Point(21, 110);
            this.lblPhoneMsg.Name = "lblPhoneMsg";
            this.lblPhoneMsg.Size = new System.Drawing.Size(434, 19);
            this.lblPhoneMsg.TabIndex = 108;
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbPhone.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPhone.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPhone.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbPhone.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbPhone.Location = new System.Drawing.Point(86, 35);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(177, 27);
            this.tbPhone.TabIndex = 34;
            this.tbPhone.Enter += new System.EventHandler(this.tbPhone_Enter);
            this.tbPhone.Leave += new System.EventHandler(this.tbPhone_Leave);
            // 
            // lblEmail
            // 
            this.lblEmail.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblEmail.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(143, 75);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(306, 26);
            this.lblEmail.TabIndex = 33;
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitCorpAdd
            // 
            this.lblTitCorpAdd.AutoSize = true;
            this.lblTitCorpAdd.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitCorpAdd.Location = new System.Drawing.Point(244, 496);
            this.lblTitCorpAdd.Name = "lblTitCorpAdd";
            this.lblTitCorpAdd.Size = new System.Drawing.Size(136, 19);
            this.lblTitCorpAdd.TabIndex = 36;
            this.lblTitCorpAdd.Text = "Company Address:";
            // 
            // lblWareAddress
            // 
            this.lblWareAddress.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblWareAddress.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblWareAddress.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWareAddress.Location = new System.Drawing.Point(393, 492);
            this.lblWareAddress.Name = "lblWareAddress";
            this.lblWareAddress.Size = new System.Drawing.Size(450, 26);
            this.lblWareAddress.TabIndex = 35;
            this.lblWareAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnManagAddress
            // 
            this.btnManagAddress.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnManagAddress.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnManagAddress.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagAddress.Location = new System.Drawing.Point(859, 522);
            this.btnManagAddress.Name = "btnManagAddress";
            this.btnManagAddress.Size = new System.Drawing.Size(127, 33);
            this.btnManagAddress.TabIndex = 26;
            this.btnManagAddress.Text = "Manage Address";
            this.btnManagAddress.UseVisualStyleBackColor = false;
            this.btnManagAddress.Click += new System.EventHandler(this.btnManagAddress_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnModify.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnModify.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnModify.Location = new System.Drawing.Point(508, 881);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(96, 34);
            this.btnModify.TabIndex = 27;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnCancel.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnCancel.Location = new System.Drawing.Point(668, 881);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 34);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblAccType
            // 
            this.lblAccType.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblAccType.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblAccType.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccType.Location = new System.Drawing.Point(355, 96);
            this.lblAccType.Name = "lblAccType";
            this.lblAccType.Size = new System.Drawing.Size(153, 26);
            this.lblAccType.TabIndex = 29;
            this.lblAccType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbGender
            // 
            this.cmbGender.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.cmbGender.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbGender.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbGender.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.cmbGender.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(333, 310);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(121, 27);
            this.cmbGender.TabIndex = 30;
            // 
            // tbFirstName
            // 
            this.tbFirstName.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbFirstName.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbFirstName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbFirstName.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbFirstName.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbFirstName.Location = new System.Drawing.Point(333, 228);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(177, 27);
            this.tbFirstName.TabIndex = 31;
            this.tbFirstName.Enter += new System.EventHandler(this.tbFirstName_Enter);
            this.tbFirstName.Leave += new System.EventHandler(this.tbFirstName_Leave);
            // 
            // tbLastName
            // 
            this.tbLastName.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbLastName.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbLastName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbLastName.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbLastName.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbLastName.Location = new System.Drawing.Point(333, 268);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(177, 27);
            this.tbLastName.TabIndex = 32;
            this.tbLastName.Enter += new System.EventHandler(this.tbLastName_Enter);
            this.tbLastName.Leave += new System.EventHandler(this.tbLastName_Leave);
            // 
            // lblTitDateOfBirth
            // 
            this.lblTitDateOfBirth.AutoSize = true;
            this.lblTitDateOfBirth.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitDateOfBirth.Location = new System.Drawing.Point(244, 356);
            this.lblTitDateOfBirth.Name = "lblTitDateOfBirth";
            this.lblTitDateOfBirth.Size = new System.Drawing.Size(104, 19);
            this.lblTitDateOfBirth.TabIndex = 33;
            this.lblTitDateOfBirth.Text = "Date of Birth:";
            // 
            // chkNGDateOfBirth
            // 
            this.chkNGDateOfBirth.AutoSize = true;
            this.chkNGDateOfBirth.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.chkNGDateOfBirth.Location = new System.Drawing.Point(359, 379);
            this.chkNGDateOfBirth.Name = "chkNGDateOfBirth";
            this.chkNGDateOfBirth.Size = new System.Drawing.Size(117, 23);
            this.chkNGDateOfBirth.TabIndex = 35;
            this.chkNGDateOfBirth.Text = "Not provided";
            this.chkNGDateOfBirth.UseVisualStyleBackColor = true;
            // 
            // lblTitCreateDate
            // 
            this.lblTitCreateDate.AutoSize = true;
            this.lblTitCreateDate.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitCreateDate.Location = new System.Drawing.Point(242, 162);
            this.lblTitCreateDate.Name = "lblTitCreateDate";
            this.lblTitCreateDate.Size = new System.Drawing.Size(95, 19);
            this.lblTitCreateDate.TabIndex = 36;
            this.lblTitCreateDate.Text = "Create Date:";
            // 
            // lblTitUID
            // 
            this.lblTitUID.AutoSize = true;
            this.lblTitUID.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitUID.Location = new System.Drawing.Point(242, 131);
            this.lblTitUID.Name = "lblTitUID";
            this.lblTitUID.Size = new System.Drawing.Size(43, 19);
            this.lblTitUID.TabIndex = 37;
            this.lblTitUID.Text = "UID:";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCreateDate.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateDate.Location = new System.Drawing.Point(355, 158);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(153, 26);
            this.lblCreateDate.TabIndex = 38;
            this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserUID
            // 
            this.lblUserUID.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblUserUID.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblUserUID.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserUID.Location = new System.Drawing.Point(355, 127);
            this.lblUserUID.Name = "lblUserUID";
            this.lblUserUID.Size = new System.Drawing.Size(153, 26);
            this.lblUserUID.TabIndex = 39;
            this.lblUserUID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = global::templatev1.Properties.Settings.Default.logoutColor;
            this.btnDelete.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "logoutColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnDelete.Location = new System.Drawing.Point(1034, 868);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(124, 61);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete Account";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTitPayment
            // 
            this.lblTitPayment.AutoSize = true;
            this.lblTitPayment.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitPayment.Location = new System.Drawing.Point(244, 417);
            this.lblTitPayment.Name = "lblTitPayment";
            this.lblTitPayment.Size = new System.Drawing.Size(185, 19);
            this.lblTitPayment.TabIndex = 41;
            this.lblTitPayment.Text = "Default Payment Method:";
            // 
            // cmbPayment
            // 
            this.cmbPayment.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.cmbPayment.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbPayment.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbPayment.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.cmbPayment.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Items.AddRange(new object[] {
            "AmericanExpress",
            "MasterCard",
            "UnionPay",
            "Visa"});
            this.cmbPayment.Location = new System.Drawing.Point(435, 414);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(129, 27);
            this.cmbPayment.TabIndex = 42;
            // 
            // btnRemoveIMG
            // 
            this.btnRemoveIMG.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnRemoveIMG.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnRemoveIMG.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnRemoveIMG.Location = new System.Drawing.Point(1016, 347);
            this.btnRemoveIMG.Name = "btnRemoveIMG";
            this.btnRemoveIMG.Size = new System.Drawing.Size(142, 29);
            this.btnRemoveIMG.TabIndex = 50;
            this.btnRemoveIMG.Text = "Remove Image";
            this.btnRemoveIMG.UseVisualStyleBackColor = false;
            this.btnRemoveIMG.Click += new System.EventHandler(this.btnRemoveIMG_Click);
            // 
            // grpPass
            // 
            this.grpPass.Controls.Add(this.btnChangePwd);
            this.grpPass.Controls.Add(this.lblPwdMsg);
            this.grpPass.Controls.Add(this.tbOldPass);
            this.grpPass.Controls.Add(this.lblTitOldPass);
            this.grpPass.Controls.Add(this.lblTitPass);
            this.grpPass.Controls.Add(this.tbConfirmPass);
            this.grpPass.Controls.Add(this.lblTitConfirmPass);
            this.grpPass.Controls.Add(this.tbPass);
            this.grpPass.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.grpPass.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.grpPass.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.grpPass.Location = new System.Drawing.Point(755, 571);
            this.grpPass.Name = "grpPass";
            this.grpPass.Size = new System.Drawing.Size(362, 212);
            this.grpPass.TabIndex = 76;
            this.grpPass.TabStop = false;
            this.grpPass.Text = "Change Password:";
            // 
            // lblPwdMsg
            // 
            this.lblPwdMsg.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblPwdMsg.ForeColor = System.Drawing.Color.Red;
            this.lblPwdMsg.Location = new System.Drawing.Point(6, 149);
            this.lblPwdMsg.Name = "lblPwdMsg";
            this.lblPwdMsg.Size = new System.Drawing.Size(350, 19);
            this.lblPwdMsg.TabIndex = 108;
            // 
            // tbOldPass
            // 
            this.tbOldPass.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbOldPass.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbOldPass.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbOldPass.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbOldPass.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbOldPass.Location = new System.Drawing.Point(168, 35);
            this.tbOldPass.Name = "tbOldPass";
            this.tbOldPass.Size = new System.Drawing.Size(177, 27);
            this.tbOldPass.TabIndex = 105;
            // 
            // lblTitOldPass
            // 
            this.lblTitOldPass.AutoSize = true;
            this.lblTitOldPass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitOldPass.Location = new System.Drawing.Point(16, 38);
            this.lblTitOldPass.Name = "lblTitOldPass";
            this.lblTitOldPass.Size = new System.Drawing.Size(108, 19);
            this.lblTitOldPass.TabIndex = 104;
            this.lblTitOldPass.Text = "Old Password:";
            // 
            // lblTitPass
            // 
            this.lblTitPass.AutoSize = true;
            this.lblTitPass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitPass.Location = new System.Drawing.Point(16, 79);
            this.lblTitPass.Name = "lblTitPass";
            this.lblTitPass.Size = new System.Drawing.Size(79, 19);
            this.lblTitPass.TabIndex = 101;
            this.lblTitPass.Text = "Password:";
            // 
            // tbConfirmPass
            // 
            this.tbConfirmPass.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbConfirmPass.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbConfirmPass.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbConfirmPass.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbConfirmPass.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbConfirmPass.Location = new System.Drawing.Point(168, 117);
            this.tbConfirmPass.Name = "tbConfirmPass";
            this.tbConfirmPass.Size = new System.Drawing.Size(177, 27);
            this.tbConfirmPass.TabIndex = 103;
            // 
            // lblTitConfirmPass
            // 
            this.lblTitConfirmPass.AutoSize = true;
            this.lblTitConfirmPass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitConfirmPass.Location = new System.Drawing.Point(16, 120);
            this.lblTitConfirmPass.Name = "lblTitConfirmPass";
            this.lblTitConfirmPass.Size = new System.Drawing.Size(139, 19);
            this.lblTitConfirmPass.TabIndex = 100;
            this.lblTitConfirmPass.Text = "Confirm password:";
            // 
            // tbPass
            // 
            this.tbPass.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbPass.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPass.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPass.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbPass.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbPass.Location = new System.Drawing.Point(168, 76);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(177, 27);
            this.tbPass.TabIndex = 102;
            // 
            // picUserIMG
            // 
            this.picUserIMG.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picUserIMG.Location = new System.Drawing.Point(964, 96);
            this.picUserIMG.Name = "picUserIMG";
            this.picUserIMG.Size = new System.Drawing.Size(192, 243);
            this.picUserIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUserIMG.TabIndex = 77;
            this.picUserIMG.TabStop = false;
            // 
            // lblTitDept
            // 
            this.lblTitDept.AutoSize = true;
            this.lblTitDept.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitDept.Location = new System.Drawing.Point(569, 131);
            this.lblTitDept.Name = "lblTitDept";
            this.lblTitDept.Size = new System.Drawing.Size(94, 19);
            this.lblTitDept.TabIndex = 79;
            this.lblTitDept.Text = "Department:";
            // 
            // lblTitJobTitle
            // 
            this.lblTitJobTitle.AutoSize = true;
            this.lblTitJobTitle.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitJobTitle.Location = new System.Drawing.Point(569, 100);
            this.lblTitJobTitle.Name = "lblTitJobTitle";
            this.lblTitJobTitle.Size = new System.Drawing.Size(73, 19);
            this.lblTitJobTitle.TabIndex = 78;
            this.lblTitJobTitle.Text = "Job Title:";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.dtpDateOfBirth.CalendarFont = new System.Drawing.Font("Times New Roman", 12.75F);
            this.dtpDateOfBirth.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpDateOfBirth.Location = new System.Drawing.Point(359, 354);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 21);
            this.dtpDateOfBirth.TabIndex = 2;
            // 
            // lblJobTitle
            // 
            this.lblJobTitle.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblJobTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblJobTitle.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobTitle.Location = new System.Drawing.Point(670, 96);
            this.lblJobTitle.Name = "lblJobTitle";
            this.lblJobTitle.Size = new System.Drawing.Size(153, 26);
            this.lblJobTitle.TabIndex = 82;
            this.lblJobTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDept
            // 
            this.lblDept.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblDept.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblDept.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDept.Location = new System.Drawing.Point(670, 127);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(153, 26);
            this.lblDept.TabIndex = 83;
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitWareAdd
            // 
            this.lblTitWareAdd.AutoSize = true;
            this.lblTitWareAdd.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitWareAdd.Location = new System.Drawing.Point(244, 529);
            this.lblTitWareAdd.Name = "lblTitWareAdd";
            this.lblTitWareAdd.Size = new System.Drawing.Size(148, 19);
            this.lblTitWareAdd.TabIndex = 84;
            this.lblTitWareAdd.Text = "Warehouse Address:";
            // 
            // lblCorpAddress
            // 
            this.lblCorpAddress.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblCorpAddress.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCorpAddress.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorpAddress.Location = new System.Drawing.Point(393, 525);
            this.lblCorpAddress.Name = "lblCorpAddress";
            this.lblCorpAddress.Size = new System.Drawing.Size(450, 26);
            this.lblCorpAddress.TabIndex = 85;
            this.lblCorpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTItCCorpName
            // 
            this.lblTItCCorpName.AutoSize = true;
            this.lblTItCCorpName.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTItCCorpName.Location = new System.Drawing.Point(244, 463);
            this.lblTItCCorpName.Name = "lblTItCCorpName";
            this.lblTItCCorpName.Size = new System.Drawing.Size(122, 19);
            this.lblTItCCorpName.TabIndex = 86;
            this.lblTItCCorpName.Text = "Company Name:";
            // 
            // tbCorp
            // 
            this.tbCorp.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbCorp.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbCorp.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbCorp.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.tbCorp.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.tbCorp.Location = new System.Drawing.Point(392, 460);
            this.tbCorp.Name = "tbCorp";
            this.tbCorp.Size = new System.Drawing.Size(302, 27);
            this.tbCorp.TabIndex = 88;
            this.tbCorp.Enter += new System.EventHandler(this.tbCorp_Enter);
            this.tbCorp.Leave += new System.EventHandler(this.tbCorp_Leave);
            // 
            // lblfNameMsg
            // 
            this.lblfNameMsg.ForeColor = System.Drawing.Color.Red;
            this.lblfNameMsg.Location = new System.Drawing.Point(517, 236);
            this.lblfNameMsg.Name = "lblfNameMsg";
            this.lblfNameMsg.Size = new System.Drawing.Size(386, 19);
            this.lblfNameMsg.TabIndex = 102;
            // 
            // lbllNameMsg
            // 
            this.lbllNameMsg.ForeColor = System.Drawing.Color.Red;
            this.lbllNameMsg.Location = new System.Drawing.Point(517, 276);
            this.lbllNameMsg.Name = "lbllNameMsg";
            this.lbllNameMsg.Size = new System.Drawing.Size(406, 19);
            this.lbllNameMsg.TabIndex = 103;
            // 
            // lblSexMsg
            // 
            this.lblSexMsg.ForeColor = System.Drawing.Color.Red;
            this.lblSexMsg.Location = new System.Drawing.Point(460, 318);
            this.lblSexMsg.Name = "lblSexMsg";
            this.lblSexMsg.Size = new System.Drawing.Size(434, 19);
            this.lblSexMsg.TabIndex = 104;
            // 
            // lblDateMsg
            // 
            this.lblDateMsg.ForeColor = System.Drawing.Color.Red;
            this.lblDateMsg.Location = new System.Drawing.Point(565, 359);
            this.lblDateMsg.Name = "lblDateMsg";
            this.lblDateMsg.Size = new System.Drawing.Size(434, 19);
            this.lblDateMsg.TabIndex = 105;
            // 
            // lblPayMsg
            // 
            this.lblPayMsg.ForeColor = System.Drawing.Color.Red;
            this.lblPayMsg.Location = new System.Drawing.Point(570, 422);
            this.lblPayMsg.Name = "lblPayMsg";
            this.lblPayMsg.Size = new System.Drawing.Size(434, 19);
            this.lblPayMsg.TabIndex = 106;
            // 
            // lblContactMsg
            // 
            this.lblContactMsg.ForeColor = System.Drawing.Color.Red;
            this.lblContactMsg.Location = new System.Drawing.Point(700, 466);
            this.lblContactMsg.Name = "lblContactMsg";
            this.lblContactMsg.Size = new System.Drawing.Size(434, 19);
            this.lblContactMsg.TabIndex = 107;
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnChangePwd.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnChangePwd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePwd.Location = new System.Drawing.Point(197, 171);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(148, 33);
            this.btnChangePwd.TabIndex = 108;
            this.btnChangePwd.Text = "Change password";
            this.btnChangePwd.UseVisualStyleBackColor = false;
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click);
            // 
            // proFileMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::templatev1.Properties.Settings.Default.bgColor;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.lblContactMsg);
            this.Controls.Add(this.lblPayMsg);
            this.Controls.Add(this.lblDateMsg);
            this.Controls.Add(this.lblSexMsg);
            this.Controls.Add(this.lbllNameMsg);
            this.Controls.Add(this.lblfNameMsg);
            this.Controls.Add(this.tbCorp);
            this.Controls.Add(this.lblTItCCorpName);
            this.Controls.Add(this.lblCorpAddress);
            this.Controls.Add(this.lblTitWareAdd);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.lblTitCorpAdd);
            this.Controls.Add(this.lblWareAddress);
            this.Controls.Add(this.lblTitDept);
            this.Controls.Add(this.btnManagAddress);
            this.Controls.Add(this.lblTitJobTitle);
            this.Controls.Add(this.btnUploadIMG);
            this.Controls.Add(this.grpPass);
            this.Controls.Add(this.btnRemoveIMG);
            this.Controls.Add(this.cmbPayment);
            this.Controls.Add(this.lblTitPayment);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblUserUID);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.lblTitUID);
            this.Controls.Add(this.lblTitCreateDate);
            this.Controls.Add(this.chkNGDateOfBirth);
            this.Controls.Add(this.lblTitDateOfBirth);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.tbFirstName);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.lblAccType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.grpContact);
            this.Controls.Add(this.lblTitAccType);
            this.Controls.Add(this.lblTitGender);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palTime);
            this.Controls.Add(this.palNav);
            this.Controls.Add(this.picUserIMG);
            this.Controls.Add(this.lblJobTitle);
            this.Controls.Add(this.lblDept);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "bgColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "proFileMain";
            this.Text = "Legend Motor Company Integrated System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.palNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.palTime.ResumeLayout(false);
            this.palTime.PerformLayout();
            this.palLoc.ResumeLayout(false);
            this.palLoc.PerformLayout();
            this.grpContact.ResumeLayout(false);
            this.grpContact.PerformLayout();
            this.grpPass.ResumeLayout(false);
            this.grpPass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserIMG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel palNav;
        private System.Windows.Forms.Panel palTime;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Label lblCorpName;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.Label lblTimeDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel palLoc;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Button btnProFile;
        private System.Windows.Forms.Panel palSelect;
        private System.Windows.Forms.PictureBox picBWMode;
        private System.Windows.Forms.Button btnUploadIMG;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblTitGender;
        private System.Windows.Forms.Label lblTitPhone;
        private System.Windows.Forms.Label lblTitEmail;
        private System.Windows.Forms.Label lblTitAccType;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnManagAddress;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAccType;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label lblWareAddress;
        private System.Windows.Forms.Label lblTitDateOfBirth;
        private System.Windows.Forms.CheckBox chkNGDateOfBirth;
        private System.Windows.Forms.Label lblTitCreateDate;
        private System.Windows.Forms.Label lblTitUID;
        private System.Windows.Forms.Label lblCreateDate;
        private System.Windows.Forms.Label lblUserUID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTitPayment;
        private System.Windows.Forms.ComboBox cmbPayment;
        private System.Windows.Forms.Button btnRemoveIMG;
        private System.Windows.Forms.GroupBox grpPass;
        private System.Windows.Forms.Label lblTitPass;
        private System.Windows.Forms.TextBox tbConfirmPass;
        private System.Windows.Forms.Label lblTitConfirmPass;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label lblTitCorpAdd;
        private System.Windows.Forms.PictureBox picUserIMG;
        private System.Windows.Forms.Label lblTitDept;
        private System.Windows.Forms.Label lblTitJobTitle;
        private System.Windows.Forms.Button btnFunction3;
        private System.Windows.Forms.Button btnFunction5;
        private System.Windows.Forms.Button btnFunction4;
        private System.Windows.Forms.Button btnFunction2;
        private System.Windows.Forms.Button btnFunction1;
        private System.Windows.Forms.TextBox tbOldPass;
        private System.Windows.Forms.Label lblTitOldPass;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblJobTitle;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label lblTitWareAdd;
        private System.Windows.Forms.Label lblCorpAddress;
        private System.Windows.Forms.Label lblTItCCorpName;
        private System.Windows.Forms.TextBox tbCorp;
        private System.Windows.Forms.Label lblPhoneMsg;
        private System.Windows.Forms.Label lblPwdMsg;
        private System.Windows.Forms.Label lblfNameMsg;
        private System.Windows.Forms.Label lbllNameMsg;
        private System.Windows.Forms.Label lblSexMsg;
        private System.Windows.Forms.Label lblDateMsg;
        private System.Windows.Forms.Label lblPayMsg;
        private System.Windows.Forms.Label lblContactMsg;
        private System.Windows.Forms.Button btnChangePwd;
    }
}

