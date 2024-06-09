using System;
using System.Drawing;
using System.Windows.Forms;

namespace templatev1
{
    public partial class LoadingForm : UserControl
    {
        // Define an event
        public event Action LoadingCompleted;

        public LoadingForm()
        {
            InitializeComponent();
            // Initialize the Label
            Label loadingLabel = new Label();
            loadingLabel.Text = "Loading...";
            loadingLabel.AutoSize = true;
            loadingLabel.Location = new Point(10, 10);
            this.Controls.Add(loadingLabel);

            // Initialize the ProgressBar
            ProgressBar progressBar = new ProgressBar();
            progressBar.Location = new Point(10, 40);
            progressBar.Width = 200;
            this.Controls.Add(progressBar);

            // Start the timer
            Timer timer = new Timer();
            timer.Interval = 100; // 100 ms
            timer.Tick += (s, e) =>
            {
                // Increase the progress bar value
                progressBar.Value = Math.Min(progressBar.Value + 1, 100);

                // Raise the event when the progress bar is full
                if (progressBar.Value == 100)
                {
                    timer.Stop();
                    LoadingCompleted?.Invoke();
                }
            };
            timer.Start();
        }
    }
}