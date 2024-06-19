using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using controller;
using templatev1.Properties;

namespace templatev1
{
    public partial class giveFeedback : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;

        public giveFeedback()
        {
            InitializeComponent();
        }

        public giveFeedback(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string feedback = tbFB.Text;
            string orderID = cmbOrder.Text;
            int wordCount = CountWords(feedback);
            if (wordCount > 100)
            {
                MessageBox.Show("Word Amount Exceed.", "Too Many Words", MessageBoxButtons.OK,
                    MessageBoxIcon.Error); //alert the user not exceed word count 
            }
            else if (wordCount <= 0)
            {
                MessageBox.Show("Please enter something", "Empty Feedback", MessageBoxButtons.OK,
                    MessageBoxIcon.Error); //alert the user the textbox is empty
            }
            else
            {
                feedbackController
                    controller = new feedbackController(); //create controller object
                Boolean addFeedback = controller.AddFeedback("LMC00001", feedback, orderID);
                if (addFeedback)
                {
                    tbFB.Text = "";
                    lblWordCount.Text = "Word Count: 0";
                    cmbOrder.Text = "N/A";
                    MessageBox.Show("Feedback Sent.\nThank you for your feedback.");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbFB.Text = "";
            lblWordCount.Text = "Word Count: 0";
        }


        private void tbFB_TextChanged(object sender, EventArgs e) //show word count instantly after typeing a new word
        {
            string feedback = tbFB.Text;
            int wordCount = CountWords(feedback);
            lblWordCount.Text = $"Word Count: {wordCount}";
        }

        public static int CountWords(string text)
        {
            // Check if the input text is null or empty
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            // Split the text into words based on delimiters
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' },
                StringSplitOptions.RemoveEmptyEntries);

            // Return the number of words
            return words.Length;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss"); //timer
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form o = new cart(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void giveFeedback_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true; //timer
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet
            LoadComboBox();
            cmbOrder.SelectedIndex = 0;
        }

        private void LoadComboBox()
        {
            List<string> order = new List<string> { "N/A" };

            feedbackController controller = new feedbackController(); //create controller object
            List<string> d = controller.getOrderID(UID);

            foreach (string x in d)
            {
                order.Add(x);
            }

            cmbOrder.DataSource = order;
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
    }
}