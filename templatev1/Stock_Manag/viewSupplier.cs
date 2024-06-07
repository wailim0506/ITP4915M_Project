using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Stock_Manag
{
    public partial class viewSupplier : Form
    {
        DataTable dt;
        controller.supplierController controller;
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;

        public viewSupplier()
        {
            InitializeComponent();
            controller = new controller.supplierController();
        }

        public viewSupplier(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            controller = new controller.supplierController();
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void viewSupplier_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet
            int numOfSupplier = controller.countSupplier();
            dt = controller.getSupplierList();

            //create label 
            int yPosition = 15;
            for (int i = 1; i <= numOfSupplier; i++)
            {
                Label lblID = new Label()
                {
                    Name = $"lblID{i}", Text = $"{dt.Rows[i - 1][0]}",
                    Location = new System.Drawing.Point(15, yPosition), Font = new Font("Microsoft Sans Serif", 11)
                };
                Label lblName = new Label()
                {
                    Name = $"lblName{i}", Text = $"{dt.Rows[i - 1][1]}",
                    Location = new System.Drawing.Point(152, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new System.Drawing.Size(180, 50)
                };
                Label lblPhone = new Label()
                {
                    Name = $"lblPhone{i}", Text = $"{dt.Rows[i - 1][2]}",
                    Location = new System.Drawing.Point(341, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new System.Drawing.Size(150, 50)
                };
                Label lblAddress = new Label()
                {
                    Name = $"lblAddress{i}", Text = $"{dt.Rows[i - 1][3]}",
                    Location = new System.Drawing.Point(510, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new System.Drawing.Size(180, 50)
                };
                Label lblCountry = new Label()
                {
                    Name = $"lblCountry{i}", Text = $"{dt.Rows[i - 1][4]}",
                    Location = new System.Drawing.Point(716, yPosition - 7),
                    Font = new Font("Microsoft Sans Serif", 11), Size = new System.Drawing.Size(150, 35),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                RadioButton radioButton = new RadioButton
                {
                    Name = $"radioButton{i}", Text = "", Location = new System.Drawing.Point(873, yPosition - 1),
                    BackColor = Color.Transparent, Size = new System.Drawing.Size(14, 17)
                };

                grpSupplier.Controls.Add(lblID);
                grpSupplier.Controls.Add(lblName);
                grpSupplier.Controls.Add(lblPhone);
                grpSupplier.Controls.Add(lblAddress);
                grpSupplier.Controls.Add(lblCountry);
                grpSupplier.Controls.Add(radioButton);
                yPosition += 50;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Label label = new Label();
            int index = getIndex(); //get which radio is checked first
            if (index != -1)
            {
                foreach (Control control in grpSupplier.Controls)
                {
                    if (control.Name == $"lblID{index}")
                    {
                        label = (Label)control;
                        Form editSupplier = new editSupplier(control.Text.ToString(), accountController, UIController);
                        this.Hide();
                        editSupplier.StartPosition = FormStartPosition.Manual;
                        editSupplier.Location = this.Location;
                        editSupplier.ShowDialog();
                        this.Close();
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select one supplier.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Label label = new Label();
            int index = getIndex(); //get which radio is checked first
            foreach (Control control in grpSupplier.Controls)
            {
                if (control.Name == $"lblID{index}")
                {
                    label = (Label)control;
                    Boolean del = controller.deleteSupplier(label.Text.ToString());
                    if (del == true)
                    {
                        Form viewSupplier = new viewSupplier();
                        this.Hide();
                        viewSupplier.StartPosition = FormStartPosition.Manual;
                        viewSupplier.Location = this.Location;
                        viewSupplier.Size = this.Size;
                        viewSupplier.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "It seem like there are still have spare part using this supplier.\nPLease check again.");
                    }
                }
            }
        }

        private int getIndex()
        {
            RadioButton radioButton;
            int i = 1;
            foreach (Control control in
                     grpSupplier
                         .Controls) //find which radio button is checked                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            {
                if (control.Name == $"radioButton{i}")
                {
                    radioButton = (RadioButton)control;
                    if (radioButton.Checked)
                    {
                        return i;
                    }

                    i++;
                }
            }

            int x = -1;
            return x;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addSupplier = new addSupplier(accountController, UIController);
            this.Hide();
            addSupplier.StartPosition = FormStartPosition.Manual;
            addSupplier.Location = this.Location;
            addSupplier.ShowDialog();
            this.Close();
        }
    }
}