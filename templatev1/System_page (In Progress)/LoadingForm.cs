using System;
using System.Windows.Forms;

namespace templatev1
{
    // PLEASE NOTE THAT DON"T USE SINCE IT IS BUGGED it will Crash the application
    public partial class LoadingForm : UserControl
    {
        // Define an event
        public event Action OnExit;
        Timer timer = new Timer();
        int i = 1;
        Label waitPoint = new Label();

        public LoadingForm()
        {
            InitializeComponent();
            timer.Interval = 500;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (i <= 3)
            {
                waitPoint.Text += ".";
                i++;
            }
            else
            {
                waitPoint.Text = ".";
                i = 1;
            }
        }
    }
}