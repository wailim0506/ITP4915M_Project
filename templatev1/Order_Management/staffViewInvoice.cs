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

        public staffViewInvoice(string orderID)
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            this.orderID = orderID;
        }


        public staffViewInvoice(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new viewInvoiceController();
            shipDate = "";
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }


        private void staffViewInvoice_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }

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
                    Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(164, 20)
                };
                Label lblPartName = new Label
                {
                    Text = $"{controller.GetPartName(t)}",
                    Location = new Point(174, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(316, 20)
                };
                Label lblQtyOrdered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(496, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(123, 20)
                };
                Label lblQtyDelivered = new Label
                {
                    Text = $"{controller.GetQty(orderID, t)}",
                    Location = new Point(625, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11),
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
            Form o = new staffOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
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
            WebBrowser webBrowser = new WebBrowser
            {
                Location = new Point(0, 0),
                Size = new Size(0, 0)
            };
            Controls.Add(webBrowser);
            string imagePath = Path.Combine(Path.GetTempPath(), "Invoice.png");
            toImg(imagePath, pnlInvoice);
            string pdfPath = Path.Combine(Path.GetTempPath(), $"Invoice of {orderID}.pdf");
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printInvoice(pnlInvoice);
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
    }
}