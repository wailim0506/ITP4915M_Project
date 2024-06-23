using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using controller;
using templatev1.Properties;

namespace templatev1
{
    public partial class favourite : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        favouriteController controller;

        public favourite()
        {
            InitializeComponent();
            controller = new favouriteController();
        }


        public favourite(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new favouriteController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void favourite_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DataTable dt = controller.GetFavourite(UID);
            load_part(dt);
            cmbSorting.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Favourite"));
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

            List<string> partName = new List<string>();
            for (int i = 0; i < rowNeed; i++)
            {
                for (int k = 0; k < columnPerRow; k++)
                {
                    if (currentGrpBox >= grpBoxNeeded) continue;
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
                        Text = $"{dt.Rows[currentGrpBox][3]} - {dt.Rows[currentGrpBox][19]}",
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
                        Text = $"{dt.Rows[currentGrpBox][14]}", AutoSize = false,
                        Font = new Font("Microsoft Sans Serif", 12), Location = new Point(62, 268),
                        Size = new Size(218, 20)
                    };
                    Label lblPrice = new Label
                    {
                        Text = $"{dt.Rows[currentGrpBox][8]}", AutoSize = false,
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
                    btnView.Click += viewPart;
                    btnRemove.Click += removeFavourite;

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
                    partName.Add(lblName.Text);
                }

                yPosition += 388;
            }

            lblResultNum.Text = grpBoxNeeded.ToString();

            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();
            autoCompleteCollection.AddRange(partName.ToArray());
            tbKW.AutoCompleteCustomSource = autoCompleteCollection;
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

            if (sender is Button clickedButton)
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
                                Form viewSparePart = new viewSparePart(control.Text, accountController,
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
            if (sender is Button clickedButton)
            {
                string buttonName = clickedButton.Name;
                int index = getRemoveIndex(buttonName);
                if (index != -1)
                {
                    if (clickedButton.Parent is GroupBox parentGroupBox)
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private Image imageString(string imageName)
        {
            PropertyInfo property =
                typeof(Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }

        private void tbKW_TextChanged(object sender, EventArgs e)
        {
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text, tbKW.Text,
                cmbSorting.Text));
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text, tbKW.Text,
                cmbSorting.Text));
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
            load_part(controller.GetFavouriteWhenTextChange(UID, cmbCategory.Text, tbKW.Text,
                cmbSorting.Text));
        }

        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Settings.Default.BWmode = value.BWmode;
            if (Settings.Default.BWmode)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Resources.LB;
                picHome.Image = Resources.home;
            }
        }

        public void hideButton()
        {
            dynamic btnFun = UIController.showFun();
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
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void setIndicator(int btnNo)
        {
            switch (btnNo)
            {
                case 1:
                    palSelect1.Visible = true;
                    break;
                case 2:
                    palSelect2.Visible = true;
                    break;
                case 3:
                    palSelect3.Visible = true;
                    break;
                case 4:
                    palSelect4.Visible = true;
                    break;
                case 5:
                    palSelect5.Visible = true;
                    break;
            }
        }
    }
}