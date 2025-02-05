﻿using System;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.controller.Utilities;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using Microsoft.Extensions.Logging;

namespace LMCIS.User_Manag
{
    public partial class SAccManage : Form
    {
        private string uName, UID, selectedUid;
        AccountController accountController;
        UIController UIController;
        proFileController proFileController;
        UserController UserController;
        private int index;

        public SAccManage(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            UserController = new UserController();
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is loaded the form.");
            Initialization();
        }

        private void Initialization()
        {
            setIndicator(UIController.getIndicator("User Management"));
            timer1.Enabled = true;
            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            rdoStaff.Checked = true;
            cmbDept.Items.AddRange(UserController.GetDept().ToArray());
            radioButtons_CheckedChanged(this, new EventArgs());
            btnAddStaffAcc.Visible = palMgmt.Visible = UIController.UserMgmt();
            dgvUser.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            DgvIndicator();

            //For determine which button needs to be shown.
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        //Determine next page.
        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else if (accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(accountController, UIController);
                    }
                    else
                    {
                        next = new staffOrderList(accountController, UIController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
            }

            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the {Function} page.");
            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the about page.");
            Form about = new About(accountController, UIController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
        }

        //For Dark Color function
        private void picHome_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the home page.");
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.Size = Size;
            home.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the profile page.");
            proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.Size = Size;
            proFile.ShowDialog();
            Close();
        }


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is logging out.");
            Form login = new Login();
            Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = Location;
            login.Size = Size;
            login.ShowDialog();
            Close();
        }

        private void btnAddStaffAcc_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the create account page.");
            Form createAcc = new ScreateAccount(accountController, UIController, UserController);
            Hide();
            //Swap the current form to another.
            createAcc.StartPosition = FormStartPosition.Manual;
            createAcc.Location = Location;
            createAcc.Size = Size;
            createAcc.ShowDialog();
            Close();
        }

        private void cmbDept_SelectedValueChanged(object sender, EventArgs e)
        {
            dgvUser.DataSource = UserController.GetUserList("Staff", cmbDept.SelectedItem.ToString());
            DgvIndicator();
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            index = dgvUser.CurrentCell.RowIndex;
            selectedUid = dgvUser.Rows[index].Cells[0].Value.ToString();
            
            if (selectedUid == UID)
                MessageBox.Show("You cannot DISABLE your account.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (dgvUser.Rows[index].Cells[4].Value.ToString().Equals("disable"))
                MessageBox.Show("The current status is DISABLE!",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var result =
                    MessageBox.Show(
                        "Are you sure to DISABLE user account " + selectedUid + " ?\nClick Yes to continue.",
                        "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (rdoStaff.Checked == true)
                        UserController.StatusAcc("Staff", selectedUid, 1);
                    else
                        UserController.StatusAcc("Customer", selectedUid, 1);
                }

                radioButtons_CheckedChanged(this, new EventArgs());
            }
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            index = dgvUser.CurrentCell.RowIndex;
            selectedUid = dgvUser.Rows[index].Cells[0].Value.ToString();

            if (dgvUser.Rows[index].Cells[4].Value.ToString().Equals("active"))
                MessageBox.Show("The current status is ACTIVE!",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var result =
                    MessageBox.Show(
                        $"Are you sure to ACTIVE user account {selectedUid}?\nClick Yes to continue.",
                        "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (rdoStaff.Checked == true)
                        UserController.StatusAcc("Staff", selectedUid, 0);
                    else
                        UserController.StatusAcc("Customer", selectedUid, 0);
                }

                radioButtons_CheckedChanged(this, new EventArgs());
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text.StartsWith("LMC") || tbSearch.Text.StartsWith("LMS"))
            {
                if (tbSearch.Text.StartsWith("LMS"))
                    rdoStaff.Checked = true;
                else
                    rdoCustomer.Checked = true;

                dgvUser.DataSource = UserController.GetUser(tbSearch.Text);
                DgvIndicator();
            }
            else
                MessageBox.Show("Not a valid UserID",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedUid))
            {
                Form UserModify = new UserModify(accountController, UIController, selectedUid);
                Hide();
                //Swap the current form to another.
                UserModify.StartPosition = FormStartPosition.Manual;
                UserModify.Location = Location;
                UserModify.Size = Size;
                UserModify.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("User NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                picSearch_Click(this, new EventArgs());
            }
        }

        private void dgvUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dgvUser.ClearSelection();
            ClearLabel();
        }

        private void dgvUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvUser.Rows.Count > 0)
            {
                dgvUser.ClearSelection();
                index = dgvUser.CurrentCell.RowIndex;

                for (int r = 0; r < dgvUser.ColumnCount; r++)
                    dgvUser[r, index].Selected = true;

                selectedUid = dgvUser.Rows[index].Cells[0].Value.ToString();

                proFileController UserInfo =
                    new proFileController(selectedUid, rdoStaff.Checked == true ? "Staff" : "Customer");
                dynamic info = UserInfo.getUserInfo();
                lblDUname.Text = info.fName + ", " + info.lName;
                lblUGender.Text = info.sex;
                lblUEmail.Text = info.email;
                lblUPhone.Text = info.phone;
                lblUCorpName.Text = info.corp;
                lblUCorpAdd.Text = info.caddress;
                lblUAdd.Text = info.waddress;
                lblUStatus.Text = info.status;
                lblUDept.Text = info.dept;
                lblUJob.Text = info.jobTitle;
                lblULMAcc.Text = info.IsLM;
            }
            else
                MessageBox.Show("User NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvUser_Sorted(object sender, EventArgs e)
        {
            DgvIndicator();
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            ClearLabel();
            if (rdoStaff.Checked == true)
            {
                lblTitTotalNoUser.Text = "No. of staff in the system: " + UserController.GetTotalUser("Staff");
                cmbDept.Visible = lblTitDept.Visible = true;
                btnBlock.Text = "Disable staff account";
                btnAct.Text = "Active staff account";
                dgvUser.DataSource = UserController.GetUserList("Staff");
                DgvIndicator();
                palCus.Visible = false;
            }
            else
            {
                lblTitTotalNoUser.Text = "No. of customer in the system: " + UserController.GetTotalUser("Customer");
                cmbDept.Visible = lblTitDept.Visible = false;
                btnBlock.Text = "Disable Customer Account";
                btnAct.Text = "Active Customer account";
                dgvUser.DataSource = UserController.GetUserList("Customer");
                DgvIndicator();
                palCus.Visible = true;
            }
        }

        private void ClearLabel()
        {
            lblDUname.Text = lblUGender.Text = lblUStatus.Text = lblUJob.Text = lblUDept.Text = lblULMAcc.Text
                = lblUEmail.Text = lblUPhone.Text = lblUCorpName.Text = lblUCorpAdd.Text = lblUAdd.Text = "";
        }

        private void DgvIndicator()
        {
            dgvUser.ClearSelection();
            //If lower than danger level change color of the row to red. 
            for (int r = 0; r < dgvUser.RowCount; r++)
            {
                if (dgvUser.Rows[r].Cells[4].Value.ToString().Equals("active")) //Normal.
                    dgvUser.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C6FEB8");
                if (dgvUser.Rows[r].Cells[4].Value.ToString().Equals("disable")) //meets re-order level.
                    dgvUser.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB8B8");
            }
        }
    }
}