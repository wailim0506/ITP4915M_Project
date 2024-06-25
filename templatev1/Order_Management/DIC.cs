using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using controller;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace templatev1
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
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
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
            btnFunction5.Location = new Point(0, 223);
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
            string filePath = $"DIC of {orderID}.pdf";

            SaveDICToPdf(pnlDIC, filePath);
            PreviewDICInBrowser(filePath);
        }

        public void SaveDICToPdf(Panel panel, string filePath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            Bitmap panelBitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panel.Width, panel.Height));
            using (MemoryStream stream = new MemoryStream())
            {
                panelBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                XImage image = XImage.FromStream(stream);
                gfx.DrawImage(image, 0, 0, page.Width, page.Height);
            }

            document.Save(filePath);
            document.Close();
        }

        private void PreviewDICInBrowser(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open PDF: {ex.Message}");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
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
            proFile.Size = Size;
            proFile.ShowDialog();
            Close();
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            Form proFile = new SAccManage(accountController, UIController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form home = new OnSaleMain(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form home = new StockMgmt(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
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