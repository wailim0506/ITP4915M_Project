using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace templatev1
{
    public partial class DID : Form
    {
        AccountController accountController;
        UIController UIController;
        didController controller;
        private string uName, UID;
        string orderID;
        string partNum;
        bool isManager;

        public DID(string orderID, string partNum)
        {
            InitializeComponent();
            controller = new didController();
            this.orderID = orderID;
            this.partNum = partNum;
        }

        public DID(string orderID, string partNum, AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.partNum = partNum;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new didController();
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void DID_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            string filePath = $"DID of {lblPartName.Text}.pdf";

            SaveDIDToPdf(pnlDID, filePath);
            PreviewDIDInBrowser(filePath);
        }

        public void SaveDIDToPdf(Panel panel, string filePath)
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

        private void PreviewDIDInBrowser(string filePath)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDIC(pnlDID);
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

        private Bitmap CaptureDIC(Panel p)
        {
            Bitmap bitmap = new Bitmap(p.Width, p.Height);
            p.DrawToBitmap(bitmap, new Rectangle(0, 0, p.Width, p.Height));
            return bitmap;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

        }

        private void load_data()
        {
            DataTable dt;
            dt = controller.getData(orderID, partNum);
            lblOrderDate.Text = dt.Rows[0][0].ToString();
            lblOrderSerialNum.Text = dt.Rows[0][1].ToString();
            lblOrderQty.Text = dt.Rows[0][2].ToString();
            lblTotalToDespatch.Text = dt.Rows[0][2].ToString();
            lblPartName.Text = dt.Rows[0][3].ToString();
            lblCategory.Text = $"{dt.Rows[0][4]} - {dt.Rows[0][5]}";
            lblCustomerID.Text = dt.Rows[0][6].ToString();
            lblDeliveryman.Text = $"{dt.Rows[0][7]} - {dt.Rows[0][8]} " +
                                  $"{dt.Rows[0][9]}";
            lblLoc.Text += $"{lblPartName.Text}"; 
        }
    }
}