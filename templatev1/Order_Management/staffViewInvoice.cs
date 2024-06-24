using System;
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
    public partial class staffViewInvoice : Form
    {
        AccountController accountController;
        UIController UIController;
        viewInvoiceController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        bool isManager;
        private bool comeFromInvoiceList;

        public staffViewInvoice(string orderID)
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            this.orderID = orderID;
        }


        public staffViewInvoice(string orderID, AccountController accountController,
            UIController UIController, bool comeFromInvoiceList)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            this.comeFromInvoiceList = comeFromInvoiceList;
            controller = new viewInvoiceController();
            shipDate = "";
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }


        private void staffViewInvoice_Load(object sender, EventArgs e)
        {
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Invoice Management"));
            load_data();
        }

        public void load_data()
        {
            lblOrderNum.Text = orderID;
            string[] d = controller.GetOrderDate(orderID).Split(' ');
            lblOrderDate.Text = d[0];
            lblCustomerID.Text = controller.GetCustomerId(orderID);
            lblInvoiceNum.Text = controller.GetInvoiceNum(orderID);
            lblDelaerAddress.Text = controller.GetCustomerAddress(orderID);
            lblDeliveryAddress.Text = controller.GetWarehouseAddress(orderID);

            //spare part
            string[] partNum = controller.GetOrderedSparePartNumber(orderID);
            int rowPos = 1;
            foreach (var t in partNum)
            {
                Label lblPartNum = new Label
                {
                    Text = $"{t}",
                    Location = new Point(4, rowPos),
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(164, 20)
                };
                Label lblPartName = new Label
                {
                    Text = $"{controller.GetPartName(t)}",
                    Location = new Point(174, rowPos),
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(316, 20)
                };
                Label lblQtyOrdered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(496, rowPos),
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(123, 20)
                };
                Label lblQtyDelivered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(625, rowPos),
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(139, 20)
                };

                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQtyOrdered);
                pnlSP.Controls.Add(lblQtyDelivered);
                rowPos += 27;
            }

            lblDeliveryDate.Text += controller.GetDeliveryDate(orderID);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (comeFromInvoiceList)
            {
                Form o = new staffInvoiceList(accountController, UIController);
                Hide();
                o.StartPosition = FormStartPosition.Manual;
                o.Location = Location;
                o.ShowDialog();
                Close();
                return;
            }
            else
            {
                Form o = new staffViewOrder(orderID, accountController, UIController);
                Hide();
                o.StartPosition = FormStartPosition.Manual;
                o.Location = Location;
                o.ShowDialog();
                Close();
                return;
            }
        }


        private Bitmap CaptureInvoice(Panel p)
        {
            Bitmap bitmap = new Bitmap(p.Width, p.Height);
            p.DrawToBitmap(bitmap, new Rectangle(0, 0, p.Width, p.Height));
            return bitmap;
        }

        private void printInvoice(Panel panel)
        {
            Bitmap pnlDIC = CaptureInvoice(panel);

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
            string filePath = $"Invoice of {orderID}.pdf";

            SaveInvoiceToPdf(pnlInvoice, filePath);
            PreviewInvoiceInBrowser(filePath);
        }

        private void PreviewInvoiceInBrowser(string filePath)
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

        public void SaveInvoiceToPdf(Panel panel, string filePath)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printInvoice(pnlInvoice);
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o =
                new staffOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o =
                new staffInvoiceList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
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

        private void lblCorpName_Click(object sender, EventArgs e)
        {

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