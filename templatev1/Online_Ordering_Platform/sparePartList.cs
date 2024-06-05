using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Online_Ordering_Platform
{
    public partial class sparePartList : Form
    {
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.spareListController controller;
        public sparePartList()
        {
            InitializeComponent();
            controller = new controller.spareListController();
            UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        public sparePartList(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.spareListController();
            UID = accountController.getUID();
            //UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }



        private void sparePartList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DataTable dt = controller.getAllPart();
            load_part(dt);
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
                            grpSpareBox = new GroupBox {Name = $"grpBox{currentGrpBox}", Size = new System.Drawing.Size(281, 383), Location = new System.Drawing.Point(firstColumnXPosition, yPosition) };
                        }
                        else if (k == 1)
                        {
                            grpSpareBox = new GroupBox { Size = new System.Drawing.Size(281, 383), Location = new System.Drawing.Point(secondColumnXPosition, yPosition) };
                        }
                        else
                        {
                            grpSpareBox = new GroupBox { Size = new System.Drawing.Size(281, 383), Location = new System.Drawing.Point(thirdColumnXPosition, yPosition) };
                        }
                        PictureBox picPartImage = new PictureBox { Size = new System.Drawing.Size(275, 186), Location = new System.Drawing.Point(3, 16), SizeMode = PictureBoxSizeMode.Zoom, Image = imageString($"{dt.Rows[currentGrpBox][0]}") };
                        Label lblCategoryLabel = new Label { Text = "Category :", AutoSize = true, Location = new System.Drawing.Point(6, 207), Font = new Font("Microsoft Sans Serif", 12) };
                        Label lblPartNumLabel = new Label { Text = "Part Number :", AutoSize = true, Location = new System.Drawing.Point(6, 237), Font = new Font("Microsoft Sans Serif", 12) };
                        Label lblNameLabel = new Label { Text = "Name :", AutoSize = true, Location = new System.Drawing.Point(6, 267), Font = new Font("Microsoft Sans Serif", 12) };
                        Label lblPriceLabel = new Label { Text = "Price :¥", AutoSize = true, Location = new System.Drawing.Point(6, 297), Font = new Font("Microsoft Sans Serif", 12) };

                        Label lblCategory = new Label { Text = $"{dt.Rows[currentGrpBox][2]} - {controller.getCategoryName(dt.Rows[currentGrpBox][2].ToString())}", AutoSize = false, Font = new Font("Microsoft Sans Serif", 12), Location = new System.Drawing.Point(83, 208), Size = new System.Drawing.Size(174, 20) };
                        Label lblPartNum = new Label { Name = $"lblPartNum{currentGrpBox}", Text = $"{dt.Rows[currentGrpBox][0]}", AutoSize = false, Font = new Font("Microsoft Sans Serif", 12), Location = new System.Drawing.Point(108, 238), Size = new System.Drawing.Size(171, 20) };
                        Label lblName = new Label { Text = $"{dt.Rows[currentGrpBox][3]}", AutoSize = false, Font = new Font("Microsoft Sans Serif", 12), Location = new System.Drawing.Point(62, 268), Size = new System.Drawing.Size(218, 20) };
                        Label lblPrice = new Label { Text = $"{controller.getPrice(dt.Rows[currentGrpBox][0].ToString())}", AutoSize = false, Font = new Font("Microsoft Sans Serif", 12), Location = new System.Drawing.Point(64, 297), Size = new System.Drawing.Size(213, 20) };
                        Button btnView = new Button { Name = $"btnView{currentGrpBox}", Text = "View", Font = new Font("Times New Roman", 12), Cursor = Cursors.Hand, Location = new System.Drawing.Point(3, 319), Size = new System.Drawing.Size(272, 30) };
                        Button btnAddCart = new Button { Name = $"btnAddCart{currentGrpBox}", Text = "Add Cart", Font = new Font("Times New Roman", 12), Cursor = Cursors.Hand, Location = new System.Drawing.Point(3, 350), Size = new System.Drawing.Size(205, 30) };
                        TextBox tbQty = new TextBox { Text = "1",Name = $"tbQty{currentGrpBox}", BorderStyle = BorderStyle.FixedSingle, Font = new Font("Microsoft Sans Serif", 12), Location = new System.Drawing.Point(214, 352), MaxLength = 4, Size = new System.Drawing.Size(61, 26), TextAlign = HorizontalAlignment.Center };
                        btnView.Click += new EventHandler(viewPart);
                        btnAddCart.Click += new EventHandler(addCart);
                        tbQty.KeyPress += qtyBox_KeyPress;

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
                        grpSpareBox.Controls.Add(btnAddCart);
                        grpSpareBox.Controls.Add(tbQty);

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
                                Form viewSparePart = new viewSparePart(control.Text.ToString(), accountController, UIController);
                                this.Hide();
                                viewSparePart.StartPosition = FormStartPosition.Manual;
                                viewSparePart.Location = this.Location;
                                viewSparePart.ShowDialog();
                                this.Close();
                            }
                        }
                    }
                }
            }
        }

        private void addCart(object sender, EventArgs e)
        {
            string partNum = "";
            int qty = 0;
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getAddCartIndex(buttonName);
                if (index != -1)
                {
                    GroupBox parentGroupBox = clickedButton.Parent as GroupBox;
                    if (parentGroupBox != null)
                    {
                        foreach (Control control in parentGroupBox.Controls)
                        {
                            if (control.Name == $"lblPartNum{index}")  
                            {
                                partNum = control.Text;
                            }
                            if (control.Name == $"tbQty{index}")  
                            {
                                qty += int.Parse(control.Text);
                                control.Text = "1";
                            }
                        }
                    }
                }
            }
            if (qty <= 0)
            {
                MessageBox.Show($"The quantity input is not valid.\nPlease adjust the quantity input", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else if (qty <= controller.getOnSaleQty(partNum))
            {
               if(controller.addCart(UID,partNum, qty))
                {
                    MessageBox.Show($"Added to cart", "Add Cart", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Please try again", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else {
                MessageBox.Show($"The quantity input exceed the on sale quantity({controller.getOnSaleQty(partNum)}).\nPlease adjust the quantity input", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void qtyBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private int getAddCartIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnAddCart{i}")
                {
                    return i;
                }
                i++;
            }
            int x = -1;
            return x;
        }

        private void tbKW_TextChanged(object sender, EventArgs e)
        {
            load_part(controller.getSpareWhenTextChange(cmbCategory.Text.ToString(), tbKW.Text.ToString(), cmbSorting.Text.ToString()));
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part(controller.getSpareWhenTextChange(cmbCategory.Text.ToString(), tbKW.Text.ToString(), cmbSorting.Text.ToString()));
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part(controller.getSpareWhenTextChange(cmbCategory.Text.ToString(), tbKW.Text.ToString(), cmbSorting.Text.ToString()));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form cart = new cart(accountController, UIController);
            this.Hide();
            cart.StartPosition = FormStartPosition.Manual;
            cart.Location = this.Location;
            cart.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbKW.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.favourite(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.sparePartList(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
        }

        private Image imageString(string imageName)
        {
            PropertyInfo property = typeof(Properties.Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
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
