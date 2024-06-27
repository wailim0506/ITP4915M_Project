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
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void DID_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
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
            Form o =
                new DID_List(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
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
            lblPartNum.Text = partNum;
            lblCategory.Text = $"{dt.Rows[0][4]} - {dt.Rows[0][5]}";
            lblCustomerID.Text = dt.Rows[0][6].ToString();
            lblDeliveryman.Text = $"{dt.Rows[0][7]} - {dt.Rows[0][8]} " +
                                  $"{dt.Rows[0][9]}";
            lblLoc.Text += $"{lblPartName.Text}";
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

        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else if (accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(accountController, UIController);
                    }
                    else
                    {
                        next = new staffOrderList(accountController, UIController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
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

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(accountController, UIController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
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