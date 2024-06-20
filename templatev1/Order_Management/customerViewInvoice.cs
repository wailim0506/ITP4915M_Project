using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using controller;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using templatev1.Properties;

namespace templatev1
{
    public partial class customerViewInvoice : Form
    {
        string orderID, UID;
        AccountController accountController;
        UIController UIController;
        viewInvoiceController controller;

        public customerViewInvoice()
        {
            InitializeComponent();
            controller = new viewInvoiceController();
        }

        public customerViewInvoice(string orderID, AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            //this.UID = this.accountController.getUID();
            lblUid.Text = $"Uid: {UID}";
        }

        private void customerViewInvoice_Load(object sender, EventArgs e)
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
                    Text = $"{t}", Location = new Point(4, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(164, 20)
                };
                Label lblPartName = new Label
                {
                    Text = $"{controller.GetPartName(t)}", Location = new Point(174, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(316, 20)
                };
                Label lblQtyOrdered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(496, rowPos), Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new Size(123, 20)
                };
                Label lblQtyDelivered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(625, rowPos), Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new Size(139, 20)
                };

                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQtyOrdered);
                pnlSP.Controls.Add(lblQtyDelivered);
                rowPos += 27;
            }

            lblDeliveryDate.Text += controller.GetDeliveryDate(orderID);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (controller.ConfirmInvoice(lblInvoiceNum.Text))
            {
                MessageBox.Show("Invoice confirmed", "Confirm Invoice");
            }
            else
            {
                MessageBox.Show("Error occur\nPlease try again", "Confirm Invoice", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form o = new cart(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerViewOrder = new customerViewOrder(orderID, accountController, UIController);
            Hide();
            customerViewOrder.StartPosition = FormStartPosition.Manual;
            customerViewOrder.Location = Location;
            customerViewOrder.ShowDialog();
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printInvoice(pnlInvoice);
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


        private void PreviewPdfInBrowser(string filePath)
        {
            try
            {
                // Use the default system browser to open the PDF file
                //Process.Start(new ProcessStartInfo
                //{
                //    FileName = filePath,
                //    UseShellExecute = true
                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open PDF: {ex.Message}");
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