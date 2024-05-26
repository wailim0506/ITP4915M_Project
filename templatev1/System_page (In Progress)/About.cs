using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class About : Form
    {
        public static string UID;      //The user ID.
        public static bool showbtn1, showbtn2, showbtn3, showbtn4, showbtn5;      //whether the button is visible.

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            this.Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;
            login.Size = this.Size;
            login.ShowDialog();
            this.Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home();
            this.Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = this.Location;
            home.Size = this.Size;
            home.ShowDialog();
            this.Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.BWmode == false)
            {
                btnFunction1.FlatAppearance.BorderColor = btnFunction2.FlatAppearance.BorderColor = btnFunction3.FlatAppearance.BorderColor
                    = btnFunction4.FlatAppearance.BorderColor = btnFunction5.FlatAppearance.BorderColor = Color.Green;
                btnLogOut.FlatAppearance.BorderColor = Color.Red;
                btnProFile.FlatAppearance.BorderColor = Color.DarkKhaki;
                picHome.Image = Properties.Resources.homeWhite;
                picBWMode.Image = Properties.Resources.LBWhite;
                Properties.Settings.Default.textColor = Color.White;
                Properties.Settings.Default.bgColor = Color.FromArgb(64, 64, 64);
                Properties.Settings.Default.navBarColor = Color.Green;
                Properties.Settings.Default.navColor = SystemColors.ControlDark;
                Properties.Settings.Default.timeColor = Color.DarkGray;
                Properties.Settings.Default.locTbColor = Color.Gray;
                Properties.Settings.Default.logoutColor = Color.Red;
                Properties.Settings.Default.profileColor = Color.DarkKhaki;
                Properties.Settings.Default.BWmode = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                btnFunction1.FlatAppearance.BorderColor = btnFunction2.FlatAppearance.BorderColor = btnFunction3.FlatAppearance.BorderColor 
                    = btnFunction4.FlatAppearance.BorderColor = btnFunction5.FlatAppearance.BorderColor = Color.FromArgb(59, 213, 184);
                btnLogOut.FlatAppearance.BorderColor = Color.FromArgb(255, 192, 192);
                btnProFile.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 192);
                picBWMode.Image = Properties.Resources.LB;
                picHome.Image = Properties.Resources.home;
                Properties.Settings.Default.textColor = Color.Black;
                Properties.Settings.Default.bgColor = SystemColors.Control;
                Properties.Settings.Default.navBarColor = Color.FromArgb(59, 213, 184);
                Properties.Settings.Default.navColor = SystemColors.GradientActiveCaption;
                Properties.Settings.Default.timeColor = Color.FromArgb(204, 204, 204);
                Properties.Settings.Default.locTbColor = SystemColors.ControlLight;
                Properties.Settings.Default.logoutColor = Color.FromArgb(255, 192, 192);
                Properties.Settings.Default.profileColor = Color.FromArgb(255, 255, 192);
                Properties.Settings.Default.BWmode = false;
                Properties.Settings.Default.Save();
            }
        }

        public static string funbtn1, funbtn2, funbtn3, funbtn4, funbtn5;        //Text in the button.

        public About()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            dynamic userInfo = Login.getUserInfo();
            UID = userInfo.uID;
            lblUid.Text = "UID: " + UID;

            //For determine which button needs to be shown.

            dynamic btnFun = Login.showFun();
            btnFunction1.Visible = btnFun.btn1show;
            btnFunction1.Text = btnFun.btn1value;
            btnFunction2.Visible = btnFun.btn2show;
            btnFunction2.Text = btnFun.btn2value;
            btnFunction3.Visible = btnFun.btn3show;
            btnFunction3.Text = btnFun.btn3value;
            btnFunction4.Visible = btnFun.btn4show;
            btnFunction4.Text = btnFun.btn4value;
            btnFunction5.Visible = btnFun.btn5show;
            btnFunction5.Text = btnFun.btn5value;

            lblPlatform.Text = Environment.OSVersion.ToString();
            lblInsDate.Text = Login.getInsDate();
            TimeSpan diff = DateTime.Now - DateTime.Parse(Login.getInsDate());
            var hours = diff.Hours;
            lblTotalOpTime.Text = hours.ToString() + " Hours";
           
        }


    }
}
