
namespace templatev1
{
    partial class test_Database_and_Controller
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
            this.tbHashPassword = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblCheckHashpassword = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbHashPassword
            // 
            this.tbHashPassword.Location = new System.Drawing.Point(189, 39);
            this.tbHashPassword.Name = "tbHashPassword";
            this.tbHashPassword.Size = new System.Drawing.Size(100, 20);
            this.tbHashPassword.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(189, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 8;
            // 
            // lblCheckHashpassword
            // 
            this.lblCheckHashpassword.Location = new System.Drawing.Point(36, 74);
            this.lblCheckHashpassword.Name = "lblCheckHashpassword";
            this.lblCheckHashpassword.Size = new System.Drawing.Size(147, 23);
            this.lblCheckHashpassword.TabIndex = 9;
            this.lblCheckHashpassword.Text = "Check Hashpassword";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(36, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hashpassword";
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(312, 39);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(75, 23);
            this.btnTest1.TabIndex = 11;
            this.btnTest1.Text = "Hash";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "HashOldData";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(427, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 23);
            this.label6.TabIndex = 16;
            this.label6.Text = "username";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(580, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(580, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(427, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 23);
            this.label7.TabIndex = 17;
            this.label7.Text = "password";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(708, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(427, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(253, 23);
            this.label8.TabIndex = 19;
            this.label8.Text = "Change password";
            // 
            // DevTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1259, 1046);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCheckHashpassword);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tbHashPassword);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "DevTool";
            this.Text = "Developer Tool";
            this.Load += new System.EventHandler(this.test_Database_and_Controller_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Button btnTest1;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.TextBox tbHashPassword;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblCheckHashpassword;

        #endregion
    }
}