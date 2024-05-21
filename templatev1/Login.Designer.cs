namespace templatev1
{
    partial class Login
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
            this.palTime = new System.Windows.Forms.Panel();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.palLoc = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnCreateCustomerAcc = new System.Windows.Forms.Button();
            this.btnForgetPassword = new System.Windows.Forms.Button();
            this.lblSystemTitle1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblSystemTitle2 = new System.Windows.Forms.Label();
            this.lblUsernameMsg = new System.Windows.Forms.Label();
            this.lblPasswordMsg = new System.Windows.Forms.Label();
            this.palTime.SuspendLayout();
            this.palLoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // palTime
            // 
            this.palTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.palTime.Controls.Add(this.lblTimeDate);
            this.palTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.palTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palTime.Location = new System.Drawing.Point(0, 0);
            this.palTime.Margin = new System.Windows.Forms.Padding(2);
            this.palTime.Name = "palTime";
            this.palTime.Size = new System.Drawing.Size(1170, 40);
            this.palTime.TabIndex = 1;
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
            this.palLoc.BackColor = System.Drawing.SystemColors.ControlLight;
            this.palLoc.Controls.Add(this.lblFormTitle);
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(0, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(1170, 38);
            this.palLoc.TabIndex = 2;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(6, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(55, 22);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Login";
            // 
            // btnCreateCustomerAcc
            // 
            this.btnCreateCustomerAcc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateCustomerAcc.Location = new System.Drawing.Point(461, 590);
            this.btnCreateCustomerAcc.Name = "btnCreateCustomerAcc";
            this.btnCreateCustomerAcc.Size = new System.Drawing.Size(220, 37);
            this.btnCreateCustomerAcc.TabIndex = 21;
            this.btnCreateCustomerAcc.Text = "Create Customer Account";
            this.btnCreateCustomerAcc.UseVisualStyleBackColor = true;
            this.btnCreateCustomerAcc.Click += new System.EventHandler(this.btnCreateCustomerAcc_Click);
            // 
            // btnForgetPassword
            // 
            this.btnForgetPassword.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForgetPassword.Location = new System.Drawing.Point(461, 550);
            this.btnForgetPassword.Name = "btnForgetPassword";
            this.btnForgetPassword.Size = new System.Drawing.Size(161, 37);
            this.btnForgetPassword.TabIndex = 20;
            this.btnForgetPassword.Text = "Forget Password";
            this.btnForgetPassword.UseVisualStyleBackColor = true;
            this.btnForgetPassword.Click += new System.EventHandler(this.btnForgetPassword_Click);
            // 
            // lblSystemTitle1
            // 
            this.lblSystemTitle1.Font = new System.Drawing.Font("Times New Roman", 24.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemTitle1.ForeColor = System.Drawing.Color.Red;
            this.lblSystemTitle1.Location = new System.Drawing.Point(405, 257);
            this.lblSystemTitle1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSystemTitle1.Name = "lblSystemTitle1";
            this.lblSystemTitle1.Size = new System.Drawing.Size(380, 49);
            this.lblSystemTitle1.TabIndex = 19;
            this.lblSystemTitle1.Text = "Legend Motor Company";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnLogin.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(596, 496);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(98, 33);
            this.btnLogin.TabIndex = 18;
            this.btnLogin.Text = "Log in";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(459, 448);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(235, 27);
            this.tbPassword.TabIndex = 17;
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsername.Location = new System.Drawing.Point(459, 374);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(235, 27);
            this.tbUsername.TabIndex = 16;
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.Location = new System.Drawing.Point(461, 502);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(128, 23);
            this.chkRememberMe.TabIndex = 15;
            this.chkRememberMe.Text = "Remember me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(457, 423);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(94, 22);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(457, 349);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(94, 22);
            this.lblUsername.TabIndex = 13;
            this.lblUsername.Text = "Username:";
            // 
            // lblSystemTitle2
            // 
            this.lblSystemTitle2.Font = new System.Drawing.Font("Times New Roman", 24.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemTitle2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSystemTitle2.Location = new System.Drawing.Point(405, 297);
            this.lblSystemTitle2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSystemTitle2.Name = "lblSystemTitle2";
            this.lblSystemTitle2.Size = new System.Drawing.Size(380, 49);
            this.lblSystemTitle2.TabIndex = 22;
            this.lblSystemTitle2.Text = "Integrated system";
            // 
            // lblUsernameMsg
            // 
            this.lblUsernameMsg.ForeColor = System.Drawing.Color.Red;
            this.lblUsernameMsg.Location = new System.Drawing.Point(460, 404);
            this.lblUsernameMsg.Name = "lblUsernameMsg";
            this.lblUsernameMsg.Size = new System.Drawing.Size(221, 19);
            this.lblUsernameMsg.TabIndex = 23;
            // 
            // lblPasswordMsg
            // 
            this.lblPasswordMsg.ForeColor = System.Drawing.Color.Red;
            this.lblPasswordMsg.Location = new System.Drawing.Point(460, 478);
            this.lblPasswordMsg.Name = "lblPasswordMsg";
            this.lblPasswordMsg.Size = new System.Drawing.Size(221, 15);
            this.lblPasswordMsg.TabIndex = 24;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.lblPasswordMsg);
            this.Controls.Add(this.lblUsernameMsg);
            this.Controls.Add(this.lblSystemTitle1);
            this.Controls.Add(this.lblSystemTitle2);
            this.Controls.Add(this.btnCreateCustomerAcc);
            this.Controls.Add(this.btnForgetPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palTime);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login";
            this.Text = "Legend Motor Company Integrated System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.palTime.ResumeLayout(false);
            this.palTime.PerformLayout();
            this.palLoc.ResumeLayout(false);
            this.palLoc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel palTime;
        private System.Windows.Forms.Label lblTimeDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel palLoc;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnCreateCustomerAcc;
        private System.Windows.Forms.Button btnForgetPassword;
        private System.Windows.Forms.Label lblSystemTitle1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblSystemTitle2;
        private System.Windows.Forms.Label lblUsernameMsg;
        private System.Windows.Forms.Label lblPasswordMsg;
    }
}

