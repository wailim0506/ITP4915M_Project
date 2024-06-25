using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using controller;
using controller.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.WinForms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using static controller.Utilities.Log;

namespace templatev1
{
    public partial class staffViewReport : Form
    {
        AccountController accountController;
        UIController UIController;
        viewInvoiceController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        bool isManager;
        private bool comeFromInvoiceList;
        OrderAnalysisReportController OrdereportController;
        private string period;
        private DateTime startDate;
        private DateTime endDate;

        public staffViewReport()
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            OrdereportController = new OrderAnalysisReportController();
        }


        public staffViewReport(AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
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
            dtpEndData.Visible = false;
            dtpStartData.Visible = false;
            btnReportApply.Visible = false;
            lblEndData.Visible = false;
            lblStartData.Visible = false;
            btnReportApply.Visible = false;
            rvReport.Visible = false;
            btnSaveAsFile.Visible = false;
            dtpStartData.Visible = false;
            dtpEndData.Visible = false;
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
            DataTable reportData = OrdereportController.GenerateReport(period, startDate, endDate);
            DataTable reportData2 = OrdereportController.GenerateItemReport(period, startDate, endDate);
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
            else if (period == "specific period")
            {
                dtpEndData.Visible = true;
                dtpStartData.Visible = true;
                btnReportApply.Visible = true;
                dtpStartData.Visible = true;
                dtpEndData.Visible = true;
            }
        }

        private void btnReportApply_Click(object sender, EventArgs e)
        {
            startDate = dtpStartData.Value;
            endDate = dtpEndData.Value;
            DataTable reportData = OrdereportController.GenerateReport(period, startDate, endDate);
            DataTable reportData2 = OrdereportController.GenerateItemReport(period, startDate, endDate);
            rvReport.LocalReport.DataSources.Clear();
            rvReport.LocalReport.ReportPath = "OrderAnalysisReport.rdlc"; // Set the path to your RDLC file here
            rvReport.LocalReport.DataSources.Add(new ReportDataSource("OrderSet", reportData));
            // rvReport.LocalReport.DataSources.Add(new ReportDataSource("OrderItemDataSet", reportData2));
            rvReport.RefreshReport();
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
            Log.LogMessage(LogLevel.Information, "OrderAnalysisReportController",
                $"Output path created at {outputPath}");

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ReportDataSource dataSource = new ReportDataSource("OrderSet", reportData); //reportData);
            rvReport.LocalReport.DataSources.Clear();
            rvReport.LocalReport.DataSources.Add(dataSource);
            string reportPath = "OrderAnalysisReport.rdlc" ??
                                throw new ArgumentException($"Report RDLC file not Exists at {reportPath}");
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = reportPath;

            var formatDetails = OrdereportController.GetFormatDetails(reportFormat.ToLower());

            string outputFile = $"{outputPath}\\OrderReport-{currentDate}{formatDetails.Extension}";
            byte[] bytes = viewer.LocalReport.Render(formatDetails.Encoding, null, out string mimeTypeOut, out _, out _,
                out _, out _);
            File.WriteAllBytes(outputFile, bytes);
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
            string fileFormat = cbxFileFormat.Text;
            DateTime todayDate = DateTime.Now;

            string outputPath =
                $"{Directory.GetCurrentDirectory()}/report/report-{todayDate.ToString("yyyy-MM-dd")}.{fileFormat}";
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            GenerateReportDocument(OrdereportController.GenerateReport(period, startDate, endDate), fileFormat,
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

        private void dtpStartData_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpEndData_ValueChanged(object sender, EventArgs e)
        {
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
    }
}