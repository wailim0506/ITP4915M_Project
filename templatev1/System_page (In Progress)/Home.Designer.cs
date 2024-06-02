namespace templatev1
{
    partial class Home
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
            this.btnProFile = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.lblCorpName = new System.Windows.Forms.Label();
            this.btnFunction5 = new System.Windows.Forms.Button();
            this.btnFunction4 = new System.Windows.Forms.Button();
            this.btnFunction3 = new System.Windows.Forms.Button();
            this.btnFunction2 = new System.Windows.Forms.Button();
            this.btnFunction1 = new System.Windows.Forms.Button();
            this.palTime = new System.Windows.Forms.Panel();
            this.lblUid = new System.Windows.Forms.Label();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.palLoc = new System.Windows.Forms.Panel();
            this.lblWelUser = new System.Windows.Forms.Label();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.lblTitLastLogin = new System.Windows.Forms.Label();
            this.lblTitLastPassChange = new System.Windows.Forms.Label();
            this.lblLastPassChange = new System.Windows.Forms.Label();
            this.lblLastLogin = new System.Windows.Forms.Label();
            this.lblTitMsg = new System.Windows.Forms.Label();
            this.lstDataBase = new System.Windows.Forms.ListBox();
            this.lblTitClickToSelect = new System.Windows.Forms.Label();
            this.lblTitIndex = new System.Windows.Forms.Label();
            this.lblTitSender = new System.Windows.Forms.Label();
            this.lblTitDateTime = new System.Windows.Forms.Label();
            this.btnViewFullRec = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.palNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.palTime.SuspendLayout();
            this.palLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // palNav
            // 
            this.palNav.BackColor = global::templatev1.Properties.Settings.Default.navColor;
            this.palNav.Controls.Add(this.picBWMode);
            this.palNav.Controls.Add(this.btnProFile);
            this.palNav.Controls.Add(this.btnLogOut);
            this.palNav.Controls.Add(this.picHome);
            this.palNav.Controls.Add(this.lblCorpName);
            this.palNav.Controls.Add(this.btnFunction5);
            this.palNav.Controls.Add(this.btnFunction4);
            this.palNav.Controls.Add(this.btnFunction3);
            this.palNav.Controls.Add(this.btnFunction2);
            this.palNav.Controls.Add(this.btnFunction1);
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
            this.picBWMode.TabIndex = 22;
            this.picBWMode.TabStop = false;
            this.picBWMode.Click += new System.EventHandler(this.pictureBox1_Click);
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
            this.btnFunction5.TabIndex = 15;
            this.btnFunction5.Text = "User Management";
            this.btnFunction5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction5.UseVisualStyleBackColor = false;
            this.btnFunction5.Click += new System.EventHandler(this.btnFunction5_Click);
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
            this.btnFunction4.TabIndex = 14;
            this.btnFunction4.Text = "Stock Management";
            this.btnFunction4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction4.UseVisualStyleBackColor = false;
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
            this.btnFunction3.Click += new System.EventHandler(this.btnFunction3_Click);
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
            this.btnFunction2.TabIndex = 12;
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
            this.btnFunction1.TabIndex = 11;
            this.btnFunction1.Text = "Order Management";
            this.btnFunction1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction1.UseVisualStyleBackColor = false;
            this.btnFunction1.Click += new System.EventHandler(this.btnFunction1_Click);
            // 
            // palTime
            // 
            this.palTime.BackColor = global::templatev1.Properties.Settings.Default.timeColor;
            this.palTime.Controls.Add(this.lblUid);
            this.palTime.Controls.Add(this.lblTimeDate);
            this.palTime.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "timeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.palTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.palTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palTime.ForeColor = System.Drawing.Color.Black;
            this.palTime.Location = new System.Drawing.Point(198, 0);
            this.palTime.Margin = new System.Windows.Forms.Padding(2);
            this.palTime.Name = "palTime";
            this.palTime.Size = new System.Drawing.Size(972, 40);
            this.palTime.TabIndex = 1;
            // 
            // lblUid
            // 
            this.lblUid.AutoSize = true;
            this.lblUid.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblUid.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUid.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblUid.Location = new System.Drawing.Point(814, 9);
            this.lblUid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(0, 22);
            this.lblUid.TabIndex = 1;
            // 
            // lblTimeDate
            // 
            this.lblTimeDate.AutoSize = true;
            this.lblTimeDate.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTimeDate.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeDate.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
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
            this.palLoc.Controls.Add(this.lblWelUser);
            this.palLoc.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(198, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(972, 38);
            this.palLoc.TabIndex = 2;
            // 
            // lblWelUser
            // 
            this.lblWelUser.AutoSize = true;
            this.lblWelUser.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblWelUser.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelUser.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblWelUser.Location = new System.Drawing.Point(6, 9);
            this.lblWelUser.Name = "lblWelUser";
            this.lblWelUser.Size = new System.Drawing.Size(197, 22);
            this.lblWelUser.TabIndex = 0;
            this.lblWelUser.Text = "Welcome, XXXXXXX!";
            // 
            // btnChangePass
            // 
            this.btnChangePass.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnChangePass.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnChangePass.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnChangePass.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnChangePass.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.btnChangePass.Location = new System.Drawing.Point(977, 877);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(170, 37);
            this.btnChangePass.TabIndex = 3;
            this.btnChangePass.Text = "Change Password";
            this.btnChangePass.UseVisualStyleBackColor = false;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // lblTitLastLogin
            // 
            this.lblTitLastLogin.AutoSize = true;
            this.lblTitLastLogin.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitLastLogin.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblTitLastLogin.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitLastLogin.Location = new System.Drawing.Point(590, 844);
            this.lblTitLastLogin.Name = "lblTitLastLogin";
            this.lblTitLastLogin.Size = new System.Drawing.Size(180, 20);
            this.lblTitLastLogin.TabIndex = 4;
            this.lblTitLastLogin.Text = "Last Login DateTime：";
            // 
            // lblTitLastPassChange
            // 
            this.lblTitLastPassChange.AutoSize = true;
            this.lblTitLastPassChange.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitLastPassChange.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblTitLastPassChange.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitLastPassChange.Location = new System.Drawing.Point(590, 885);
            this.lblTitLastPassChange.Name = "lblTitLastPassChange";
            this.lblTitLastPassChange.Size = new System.Drawing.Size(221, 20);
            this.lblTitLastPassChange.TabIndex = 5;
            this.lblTitLastPassChange.Text = "Last password change date：";
            // 
            // lblLastPassChange
            // 
            this.lblLastPassChange.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblLastPassChange.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblLastPassChange.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblLastPassChange.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastPassChange.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblLastPassChange.Location = new System.Drawing.Point(803, 884);
            this.lblLastPassChange.Name = "lblLastPassChange";
            this.lblLastPassChange.Size = new System.Drawing.Size(153, 26);
            this.lblLastPassChange.TabIndex = 30;
            this.lblLastPassChange.Text = "2023/10/11";
            this.lblLastPassChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastLogin
            // 
            this.lblLastLogin.BackColor = global::templatev1.Properties.Settings.Default.locTbColor;
            this.lblLastLogin.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblLastLogin.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "locTbColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblLastLogin.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastLogin.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblLastLogin.Location = new System.Drawing.Point(803, 844);
            this.lblLastLogin.Name = "lblLastLogin";
            this.lblLastLogin.Size = new System.Drawing.Size(153, 26);
            this.lblLastLogin.TabIndex = 31;
            this.lblLastLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitMsg
            // 
            this.lblTitMsg.AutoSize = true;
            this.lblTitMsg.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitMsg.Font = new System.Drawing.Font("Times New Roman", 17F);
            this.lblTitMsg.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitMsg.Location = new System.Drawing.Point(231, 118);
            this.lblTitMsg.Name = "lblTitMsg";
            this.lblTitMsg.Size = new System.Drawing.Size(96, 26);
            this.lblTitMsg.TabIndex = 32;
            this.lblTitMsg.Text = "Message:";
            // 
            // lstDataBase
            // 
            this.lstDataBase.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.lstDataBase.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lstDataBase.FormattingEnabled = true;
            this.lstDataBase.ItemHeight = 15;
            this.lstDataBase.Location = new System.Drawing.Point(236, 210);
            this.lstDataBase.Name = "lstDataBase";
            this.lstDataBase.Size = new System.Drawing.Size(898, 259);
            this.lstDataBase.TabIndex = 33;
            // 
            // lblTitClickToSelect
            // 
            this.lblTitClickToSelect.AutoSize = true;
            this.lblTitClickToSelect.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitClickToSelect.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblTitClickToSelect.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitClickToSelect.Location = new System.Drawing.Point(333, 125);
            this.lblTitClickToSelect.Name = "lblTitClickToSelect";
            this.lblTitClickToSelect.Size = new System.Drawing.Size(204, 17);
            this.lblTitClickToSelect.TabIndex = 34;
            this.lblTitClickToSelect.Text = "[Click to select AND view detial]";
            // 
            // lblTitIndex
            // 
            this.lblTitIndex.AutoSize = true;
            this.lblTitIndex.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitIndex.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitIndex.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitIndex.Location = new System.Drawing.Point(232, 187);
            this.lblTitIndex.Name = "lblTitIndex";
            this.lblTitIndex.Size = new System.Drawing.Size(49, 19);
            this.lblTitIndex.TabIndex = 35;
            this.lblTitIndex.Text = "Index";
            // 
            // lblTitSender
            // 
            this.lblTitSender.AutoSize = true;
            this.lblTitSender.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitSender.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitSender.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitSender.Location = new System.Drawing.Point(423, 187);
            this.lblTitSender.Name = "lblTitSender";
            this.lblTitSender.Size = new System.Drawing.Size(60, 19);
            this.lblTitSender.TabIndex = 36;
            this.lblTitSender.Text = "Sender";
            // 
            // lblTitDateTime
            // 
            this.lblTitDateTime.AutoSize = true;
            this.lblTitDateTime.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblTitDateTime.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitDateTime.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.lblTitDateTime.Location = new System.Drawing.Point(606, 187);
            this.lblTitDateTime.Name = "lblTitDateTime";
            this.lblTitDateTime.Size = new System.Drawing.Size(81, 19);
            this.lblTitDateTime.TabIndex = 37;
            this.lblTitDateTime.Text = "DateTime";
            // 
            // btnViewFullRec
            // 
            this.btnViewFullRec.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.btnViewFullRec.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnViewFullRec.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnViewFullRec.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnViewFullRec.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.btnViewFullRec.Location = new System.Drawing.Point(977, 837);
            this.btnViewFullRec.Name = "btnViewFullRec";
            this.btnViewFullRec.Size = new System.Drawing.Size(170, 37);
            this.btnViewFullRec.TabIndex = 40;
            this.btnViewFullRec.Text = "View Full Record";
            this.btnViewFullRec.UseVisualStyleBackColor = false;
            this.btnViewFullRec.Click += new System.EventHandler(this.btnViewFullRec_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.BackColor = global::templatev1.Properties.Settings.Default.btnColor;
            this.tbSearch.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "btnColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbSearch.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.tbSearch.Location = new System.Drawing.Point(236, 154);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(331, 26);
            this.tbSearch.TabIndex = 42;
            // 
            // picSearch
            // 
            this.picSearch.Image = global::templatev1.Properties.Resources.Daco_367903;
            this.picSearch.InitialImage = global::templatev1.Properties.Resources.Daco_367903;
            this.picSearch.Location = new System.Drawing.Point(573, 154);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(31, 25);
            this.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearch.TabIndex = 43;
            this.picSearch.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::templatev1.Properties.Settings.Default.bgColor;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.picSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.btnViewFullRec);
            this.Controls.Add(this.lblTitDateTime);
            this.Controls.Add(this.lblTitSender);
            this.Controls.Add(this.lblTitIndex);
            this.Controls.Add(this.lblTitClickToSelect);
            this.Controls.Add(this.lstDataBase);
            this.Controls.Add(this.lblTitMsg);
            this.Controls.Add(this.lblLastLogin);
            this.Controls.Add(this.lblLastPassChange);
            this.Controls.Add(this.lblTitLastPassChange);
            this.Controls.Add(this.lblTitLastLogin);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palTime);
            this.Controls.Add(this.palNav);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::templatev1.Properties.Settings.Default, "bgColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::templatev1.Properties.Settings.Default, "textColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = global::templatev1.Properties.Settings.Default.textColor;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Home";
            this.Text = "Legend Motor Company Integrated System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.palNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.palTime.ResumeLayout(false);
            this.palTime.PerformLayout();
            this.palLoc.ResumeLayout(false);
            this.palLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel palNav;
        private System.Windows.Forms.Panel palTime;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Button btnFunction5;
        private System.Windows.Forms.Button btnFunction4;
        private System.Windows.Forms.Button btnFunction3;
        private System.Windows.Forms.Button btnFunction2;
        private System.Windows.Forms.Button btnFunction1;
        private System.Windows.Forms.Label lblCorpName;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.Label lblTimeDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel palLoc;
        private System.Windows.Forms.Label lblWelUser;
        private System.Windows.Forms.Button btnProFile;
        private System.Windows.Forms.PictureBox picBWMode;
        private System.Windows.Forms.Button btnChangePass;
        private System.Windows.Forms.Label lblTitLastLogin;
        private System.Windows.Forms.Label lblTitLastPassChange;
        private System.Windows.Forms.Label lblLastPassChange;
        private System.Windows.Forms.Label lblLastLogin;
        private System.Windows.Forms.Label lblTitMsg;
        private System.Windows.Forms.ListBox lstDataBase;
        private System.Windows.Forms.Label lblTitClickToSelect;
        private System.Windows.Forms.Label lblTitIndex;
        private System.Windows.Forms.Label lblTitSender;
        private System.Windows.Forms.Label lblTitDateTime;
        private System.Windows.Forms.Button btnViewFullRec;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.PictureBox picSearch;
    }
}

