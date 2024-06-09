using System;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1.DevTool
{
    public partial class DevToolControl : UserControl
    {
        public event Action<string, string> TestClicked;
        public event Action TestDatabaseAndControllerClicked;
        public event Action FormCloseRequested;

        public DevToolControl()
        {
            InitializeComponent();

            // Initialize the buttons
            CreateTestButton("Test 1", "LMC00001", "password123", new Point(10, 10));
            CreateTestButton("Test 2", "LMS00001", "password123", new Point(10, 40));
            CreateTestButton("Test 3", "LMS00002", "abc123456", new Point(10, 70));
            CreateTestButton("Test 4", "LMS00003", "xyz789!@#", new Point(10, 100));
            CreateTestButton("Test 5", "LMS00004", "qwer5678", new Point(10, 130));
            CreateTestButton("Test 6", "LMS00005", "asdf1234!", new Point(10, 160));

            Button btnRedirectToStaffForm = new Button();
            btnRedirectToStaffForm.Text = "Redirect to Staff Form";
            btnRedirectToStaffForm.Location = new Point(10, 190);
            btnRedirectToStaffForm.Click += (s, e) =>
            {
                RedirectToStaffForm();
            };
            Controls.Add(btnRedirectToStaffForm);

            Button btnTestDatabaseAndController = new Button();
            btnTestDatabaseAndController.Text = "Test Database and Controller";
            btnTestDatabaseAndController.Location = new Point(10, 220);
            btnTestDatabaseAndController.Click += (s, e) =>
            {
                TestDatabaseAndControllerClicked?.Invoke();
            };
            Controls.Add(btnTestDatabaseAndController);
        }

        private void RedirectToStaffForm()
        {
            // AccountController accountController = new AccountController(/* parameters here */);
            // UIController uiController = new UIController(accountController);
            // StaffForm staffForm = new StaffForm(accountController, uiController);
            // Hide();
            // //Swap the current form to another.
            // staffForm.StartPosition = FormStartPosition.Manual;
            // staffForm.Location = Location;
            // staffForm.Size = Size;
            // staffForm.ShowDialog();
            // FormCloseRequested?.Invoke();
        }

        private void CreateTestButton(string text, string username, string password, Point location)
        {
            Button testButton = new Button();
            testButton.Text = text;
            testButton.Location = location;
            testButton.Click += (s, e) =>
            {
                TestClicked?.Invoke(username, password);
            };
            Controls.Add(testButton);
        }
    }
}