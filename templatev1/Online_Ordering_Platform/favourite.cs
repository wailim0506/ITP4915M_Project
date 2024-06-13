using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace templatev1.Online_Ordering_Platform
{
    public partial class favourite : Form
    {
        private string uName, UID;
        controller.AccountController accountController;
        controller.UIController UIController;
        controller.favouriteController controller;

        public favourite()
        {
            InitializeComponent();
            controller = new controller.favouriteController();
            UID = "LMC00001"; //hard code for testing
        }


        public favourite(controller.AccountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.favouriteController();
            UID = accountController.GetUid();
            //UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        private void favourite_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DataTable dt = controller.GetFavourite(UID);
            load_part(dt);
            cmbSorting.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
        }


        public void load_part(DataTable dt)
        {
            pnlSP.Controls.Clear();
            int grpBoxNeeded = countSparePart(dt);
            int columnPerRow = 3;
            int rowNeed = divideRoundUp(grpBoxNeeded, columnPerRow);
            int firstColumnXPosition = 23;
            int secondColumnXPosition = 328;
            int thirdColumnXPosition = 636;
            int yPosition = 11; //first row y position  
            int currentGrpBox = 0;

            for (int i = 0; i < rowNeed; i++)
            {
                for (int k = 0; k < columnPerRow; k++)
                {
                    if (currentGrpBox < grpBoxNeeded)
                    {
                        GroupBox grpSpareBox;
                        if (k == 0)
                        {
                            grpSpareBox = new GroupBox
                            {
                                Name = $"grpBox{currentGrpBox}", Size = new Size(281, 383),
                                Location = new Point(firstColumnXPosition, yPosition)
                            };
                        }
                        else if (k == 1)
                        {
                            grpSpareBox = new GroupBox
                            {
                                Size = new Size(281, 383),
                                Location = new Point(secondColumnXPosition, yPosition)
                            };
                        }
                        else
                        {
                            grpSpareBox = new GroupBox
                            {
                                Size = new Size(281, 383),
                                Location = new Point(thirdColumnXPosition, yPosition)
                            };
                        }

                        PictureBox picPartImage = new PictureBox
                        {
                            Size = new Size(275, 186), Location = new Point(3, 16),
                            SizeMode = PictureBoxSizeMode.Zoom, Image = imageString($"{dt.Rows[currentGrpBox][4]}")
                        };
                        Label lblCategoryLabel = new Label
                        {
                            Text = "Category :", AutoSize = true, Location = new Point(6, 207),
                            Font = new Font("Microsoft Sans Serif", 12)
                        };
                        Label lblPartNumLabel = new Label
                        {
                            Text = "Part Number :", AutoSize = true, Location = new Point(6, 237),
                            Font = new Font("Microsoft Sans Serif", 12)
                        };
                        Label lblNameLabel = new Label
                        {
                            Text = "Name :", AutoSize = true, Location = new Point(6, 267),
                            Font = new Font("Microsoft Sans Serif", 12)
                        };
                        Label lblPriceLabel = new Label
                        {
                            Text = "Price :¥", AutoSize = true, Location = new Point(6, 297),
                            Font = new Font("Microsoft Sans Serif", 12)
                        };

                        Label lblCategory = new Label
                        {
                            Text = $"{dt.Rows[currentGrpBox][3]} - {dt.Rows[currentGrpBox][18].ToString()}",
                            AutoSize = false, Font = new Font("Microsoft Sans Serif", 12),
                            Location = new Point(83, 208), Size = new Size(174, 20)
                        };
                        Label lblPartNum = new Label
                        {
                            Name = $"lblPartNum{currentGrpBox}", Text = $"{dt.Rows[currentGrpBox][4]}",
                            AutoSize = false, Font = new Font("Microsoft Sans Serif", 12),
                            Location = new Point(108, 238), Size = new Size(171, 20)
                        };
                        Label lblName = new Label
                        {
                            Text = $"{dt.Rows[currentGrpBox][13]}", AutoSize = false,
                            Font = new Font("Microsoft Sans Serif", 12), Location = new Point(62, 268),
                            Size = new Size(218, 20)
                        };
                        Label lblPrice = new Label
                        {
                            Text = $"{dt.Rows[currentGrpBox][7].ToString()}", AutoSize = false,
                            Font = new Font("Microsoft Sans Serif", 12), Location = new Point(64, 297),
                            Size = new Size(213, 20)
                        };
                        Button btnView = new Button
                        {
                            Name = $"btnView{currentGrpBox}", Text = "View", Font = new Font("Times New Roman", 12),
                            Cursor = Cursors.Hand, Location = new Point(3, 319),
                            Size = new Size(272, 30)
                        };
                        Button btnRemove = new Button
                        {
                            Name = $"btnRemove{currentGrpBox}", Text = "Remove from favourite",
                            Font = new Font("Times New Roman", 12), Cursor = Cursors.Hand,
                            Location = new Point(3, 350), Size = new Size(272, 30)
                        };
                        btnView.Click += new EventHandler(viewPart);
                        btnRemove.Click += new EventHandler(removeFavourite);

                        grpSpareBox.Controls.Add(picPartImage);
                        grpSpareBox.Controls.Add(lblCategoryLabel);
                        grpSpareBox.Controls.Add(lblPartNumLabel);
                        grpSpareBox.Controls.Add(lblNameLabel);
                        grpSpareBox.Controls.Add(lblPriceLabel);
                        grpSpareBox.Controls.Add(lblCategory);
                        grpSpareBox.Controls.Add(lblPartNum);
                        grpSpareBox.Controls.Add(lblName);
                        grpSpareBox.Controls.Add(lblPrice);
                        grpSpareBox.Controls.Add(btnView);
                        grpSpareBox.Controls.Add(btnRemove);


                        pnlSP.Controls.Add(grpSpareBox);
                        ++currentGrpBox;
                    }
                }

                yPosition += 388;
            }

            lblResultNum.Text = grpBoxNeeded.ToString();
        }

        public int countSparePart(DataTable dt)
        {
            return dt.Rows.Count;
        }

        public int divideRoundUp(int upperNum, int lowerNum)
        {
            return (upperNum + lowerNum - 1) / lowerNum;
        }

        private void viewPart(object sender, EventArgs e)
        {
            string partNum = "";
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getViewIndex(buttonName);
                if (index != -1)
                {
                    GroupBox parentGroupBox = clickedButton.Parent as GroupBox;
                    if (parentGroupBox != null)
                    {
                        foreach (Control control in parentGroupBox.Controls)
                        {
                            if (control.Name == $"lblPartNum{index}")
                            {
                                Form viewSparePart = new viewSparePart(control.Text.ToString(), accountController,
                                    UIController);
                                Hide();
                                viewSparePart.StartPosition = FormStartPosition.Manual;
                                viewSparePart.Location = Location;
                                viewSparePart.ShowDialog();
                                Close();
                            }
                        }
                    }
                }
            }
        }

        private void removeFavourite(object sender, EventArgs e)
        {
            string partNum = "";
            string qty = "";
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getRemoveIndex(buttonName);
                if (index != -1)
                {
                    GroupBox parentGroupBox = clickedButton.Parent as GroupBox;
                    if (parentGroupBox != null)
                    {
                        foreach (Control control in parentGroupBox.Controls)
                        {
                            if (control.Name == $"lblPartNum{index}") //get part Num
                            {
                                if (controller.RemoveFromFavourite(control.Text, UID))
                                {
                                    MessageBox.Show($"{control.Text} is removed from favourite", "Remove Favourite",
                                        MessageBoxButtons.OK);
                                    Form fav = new favourite(accountController, UIController);
                                    Hide();
                                    fav.StartPosition = FormStartPosition.Manual;
                                    fav.Location = Location;
                                    fav.ShowDialog();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show($"Cannot remove {control.Text} from favourite.\nPlease try again",
                                        "Remove Favourite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        private int getViewIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnView{i}")
                {
                    return i;
                }

                i++;
            }

            int x = -1;
            return x;
        }

        private int getRemoveIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnRemove{i}")
                {
                    return i;
                }

                i++;
            }

            int x = -1;
            return x;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private Image imageString(string imageName)
        {
            PropertyInfo property =
                typeof(Properties.Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }

        private void tbKW_TextChanged(object sender, EventArgs e)
        {
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text.ToString(), tbKW.Text.ToString(),
                cmbSorting.Text.ToString()));
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text.ToString(), tbKW.Text.ToString(),
                cmbSorting.Text.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form o = new cart(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new customerOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text.ToString(), tbKW.Text.ToString(),
                cmbSorting.Text.ToString()));
        }

        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Properties.Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Properties.Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Properties.Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Properties.Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Properties.Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Properties.Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Properties.Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Properties.Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Properties.Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Properties.Settings.Default.BWmode = value.BWmode;
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Properties.Resources.LB;
                picHome.Image = Properties.Resources.home;
            }
        }
    }
}