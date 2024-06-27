using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Profile;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.WinForms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using static controller.Utilities.Log;

namespace LMCIS.Order_Management
{
    public partial class staffViewReport : Form
    {
        readonly AccountController _accountController;
        readonly UIController _uiController;
        private string uName, UID;
        string orderID;
        string shipDate;
        bool isManager;
        viewInvoiceController controller;
        private bool _comeFromInvoiceList;
        readonly OrderAnalysisReportController _ordereportController;
        private string period;
        private DateTime startDate;
        private DateTime endDate;

        public staffViewReport()
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            _ordereportController = new OrderAnalysisReportController();
        }


        public staffViewReport(AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            _accountController = accountController;
            _uiController = UIController;
            controller = new viewInvoiceController();
            shipDate = "";
            UID = _accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
            _ordereportController = new OrderAnalysisReportController();
            timer1.Enabled = true;
        }


        private void staffViewInvoice_Load(object sender, EventArgs e)
        {
            rvReport.Visible = false;
            btnSaveAsFile.Visible = false;
            btnPrint.Visible = false;
            lblFormat.Visible = false;
            cbxFileFormat.Visible = false;
            hideButton();
            load_data();
        }

        public void load_data()
        {
        }

        private void GenerateReportAndRefresh(string period, DateTime startDate, DateTime endDate)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            DataTable reportData = _ordereportController.GenerateReport(period, startDate, endDate);
            DataTable reportData2 = _ordereportController.GenerateItemReport(period, startDate, endDate);
            var reportDataSource = new ReportDataSource
            {
                Name = "OrderSet",
                Value = reportData
            };
            var reportDataSource2 = new ReportDataSource
            {
                Name = "OrderItemDataSet",
                Value = reportData2
            };
            rvReport.LocalReport.DataSources.Clear();
            rvReport.LocalReport.ReportPath = "OrderAnalysisReport.rdlc"; // Set the path to your RDLC file here
            rvReport.LocalReport.DataSources.Add(reportDataSource);
            // rvReport.LocalReport.DataSources.Add(reportDataSource2);
            rvReport.RefreshReport();
            rvReport.Visible = true;
            lblFormat.Visible = true;
            cbxFileFormat.Visible = true;
        }

        private void cbxperiod_SelectedIndexChanged(object sender, EventArgs e)
        {
            period = cbxperiod.Text;
            if (period == "yearly")
            {
                GenerateReportAndRefresh(period, DateTime.Now.AddYears(-1), DateTime.Now);
            }
            else if (period == "monthly")
            {
                GenerateReportAndRefresh(period, DateTime.Now.AddMonths(-1), DateTime.Now);
            }
            else if (period == "daily")
            {
                GenerateReportAndRefresh(period, DateTime.Now.AddDays(-1), DateTime.Now);
            }
            else if (period == "None")
            {
                rvReport.Visible = false;
                btnSaveAsFile.Visible = false;
                btnPrint.Visible = false;
                lblFormat.Visible = false;
                cbxFileFormat.Visible = false;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(period));
            }
        }


        // get the current directory and create a new directory report
        // format: report-yyyy-MM-dd
        // path: currentDir/report/report-yyyy-MM-dd.fileFormat
        // if the report directory does not exist, create it
        private void cbxFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSaveAsFile.Visible = true;
            btnPrint.Visible = true;
            lblFormat.Visible = true;
            cbxFileFormat.Visible = true;
        }

        private void GenerateReportDocument(DataTable reportData, string reportFormat, string outputPath)
        {
            Directory.CreateDirectory(outputPath);
            LogMessage(LogLevel.Information, "OrderAnalysisReportController",
                $"Output path created at {outputPath}");

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ReportDataSource dataSource = new ReportDataSource("OrderSet", reportData); //reportData);
            rvReport.LocalReport.DataSources.Clear();
            rvReport.LocalReport.DataSources.Add(dataSource);
            string reportPath = "OrderAnalysisReport.rdlc" ??
                                throw new ArgumentException($"Report RDLC file not Exists at {reportPath}");
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = reportPath;

            var formatDetails = _ordereportController.GetFormatDetails(reportFormat.ToLower());

            string outputFile = $"{outputPath}\\OrderReport-{currentDate}{formatDetails.Extension}";
            byte[] bytes = viewer.LocalReport.Render(formatDetails.Encoding, null, out string mimeTypeOut, out _, out _,
                out _, out _);
            File.WriteAllBytes(outputFile, bytes);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form home = new Home(_accountController, _uiController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.Size = Size;
            home.ShowDialog();
            Close();
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
            string fileFormat = cbxFileFormat.Text;
            DateTime todayDate = DateTime.Now;

            string outputPath =
                $"{Directory.GetCurrentDirectory()}/report/report-{todayDate.ToString("yyyy-MM-dd")}.{fileFormat}";
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            GenerateReportDocument(_ordereportController.GenerateReport(period, startDate, endDate), fileFormat,
                outputPath);
            // SaveInvoiceToPdf(pnlInvoice, filePath);
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
                panelBitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                XImage image = XImage.FromStream(stream);
                gfx.DrawImage(image, 0, 0, page.Width, page.Height);
            }

            document.Save(filePath);
            document.Close();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        public void hideButton()
        {
            dynamic btnFun = _uiController.showFun();
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
            Form next = new Home(_accountController, _uiController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(_accountController, _uiController);
                    }
                    else if (_accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(_accountController, _uiController);
                    }
                    else
                    {
                        next = new staffOrderList(_accountController, _uiController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(_accountController, _uiController);
                    break;
                case "Cart":
                    next = new cart(_accountController, _uiController);
                    break;
                case "Favourite":
                    next = new favourite(_accountController, _uiController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(_accountController, _uiController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(_accountController, _uiController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(_accountController, _uiController);
                    break;
                case "User Management":
                    next = new SAccManage(_accountController, _uiController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(_accountController, _uiController);
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
            proFileController proFileController = new proFileController(_accountController);

            proFileController.setType(_accountController.GetAccountType());

            Form proFile = new proFileMain(_accountController, _uiController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(_accountController, _uiController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(_accountController, _uiController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

    }
}