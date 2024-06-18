using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;

namespace templatev1.Order_Management
{
    public partial class DIC : Form
    {
        AccountController accountController;
        UIController UIController;
        dicController controller;
        private string uName, UID;
        string orderID;
        bool isManager;

        public DIC(string orderID)
        {
            controller = new dicController();
            this.orderID = orderID;
        }

        public DIC(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new dicController();
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void DIC_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }

            timer1.Enabled = true;
            lblLoc.Text += $" {orderID}";
            lblHeading.Text += $" of Order {orderID}";
            load_data();
        }

        public void load_data()
        {
            DataTable dt = controller.getData(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];

            string orderDate = dt.Rows[0][0].ToString();
            string[]
                e = orderDate
                    .Split(' ');
            orderDate = e[0];

            lblOrderDate.Text = orderDate;
            lblDeliveryDate.Text = shippingDate;
            lblDeliveryAdd.Text = $"{dt.Rows[0][3]}";
            lblInvoiceAdd.Text = $"{dt.Rows[0][4]}, {dt.Rows[0][5]}, {dt.Rows[0][6]}";
            lblOrderSerialNum.Text = $"{dt.Rows[0][1]}";
            lblOrderID.Text = orderID;
        }

        public void hideButton()
        {
            palSelect3.Visible = false;
            btnFunction3.Visible = false;
            palSelect4.Visible = false;
            btnFunction4.Visible = false;
            btnFunction5.Location = new Point(0, 233);
            btnFunction5.Controls.Add(palSelect5);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffViewOrder(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDIC(pnlDIC);
        }

        private Bitmap CaptureDIC(Panel p)
        {
            Bitmap bitmap = new Bitmap(p.Width, p.Height);
            p.DrawToBitmap(bitmap, new Rectangle(0, 0, p.Width, p.Height));
            return bitmap;
        }

        private void printDIC(Panel panel)
        {
            Bitmap pnlDIC = CaptureDIC(panel);

            PrintDocument p = new PrintDocument();
            p.PrintPage += (sender, e) => { e.Graphics.DrawImage(pnlDIC, new Point(0, 0)); };

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = p
            };
            preview.ShowDialog();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            WebBrowser webBrowser = new WebBrowser
            {
                Location = new Point(0, 0),
                Size = new Size(0, 0)
            };
            Controls.Add(webBrowser);
            string imagePath = Path.Combine(Path.GetTempPath(), "DIC.png");
            toImg(imagePath, pnlDIC);
            string pdfPath = Path.Combine(Path.GetTempPath(), $"DIC of {orderID}.pdf");
            toPDF(imagePath, pdfPath);
            webBrowser.Navigate(pdfPath);
        }

        private void toImg(string filePath, Panel p)
        {
            Bitmap panelBitmap = new Bitmap(p.Width, p.Height);
            p.DrawToBitmap(panelBitmap, new Rectangle(0, 0, p.Width, p.Height));
            panelBitmap.Save(filePath, ImageFormat.Png);
        }

        private void toPDF(string imagePath, string pdfPath)
        {
            PrintDocument p = new PrintDocument();
            p.PrintPage += (sender, e) =>
            {
                Image i = Image.FromFile(imagePath);
                e.Graphics.DrawImage(i, 0, 0, e.PageBounds.Width, e.PageBounds.Height);
            };

            p.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            p.PrinterSettings.PrintToFile = true;
            p.PrinterSettings.PrintFileName = pdfPath;
            p.Print();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}