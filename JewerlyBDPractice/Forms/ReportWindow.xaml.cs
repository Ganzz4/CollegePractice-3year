using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows;
using Lab11.Reports.DataSets;
using Lab11;
using Microsoft.Reporting.WinForms;

namespace Lab11
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private int index;
        public ReportWindow(int n)
        {
            InitializeComponent();
            index = n;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] reportNames = new string[] {
            "Lab11.Reports.ReportProductProvider.rdlc",
            "Lab11.Reports.ReportTaxInvoice.rdlc",
            "Lab11.Reports.ReportCheque.rdlc"
        };

            string[] tableNames = new string[] {
            "DataSetProductProvider",
            "DataSetTaxInvoice",
            "DataSetCheque"
        };

            string[] queries = new string[] {
            "ProductProviderViewReport",
            "TaxInvoiceReportView",
            "ChequeReportView"
        };

            ProductProviderDataSet[] dataSets = new ProductProviderDataSet[3];

            using (var db = new Database())
            {
                db.OpenConnection();

                SqlCommand command = null;
                SqlDataAdapter adapter = null;

                for (int i = 0; i < 3; i++)
                {
                    if (index == i)
                    {
                        dataSets[i] = new ProductProviderDataSet();
                        command = new SqlCommand("SELECT * FROM " + queries[i], db.GetConnection());
                        adapter = new SqlDataAdapter("SELECT * FROM " + queries[i], db.GetConnection());
                        adapter.Fill(dataSets[i]);
                    }
                }
            }

            if (dataSets[index] != null)
            {
                _reportViewer.ProcessingMode = ProcessingMode.Local;

                // Load the embedded report
                string reportName = reportNames[index];
                using (Stream reportStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(reportName))
                {
                    if (reportStream != null)
                    {
                        using (StreamReader reader = new StreamReader(reportStream))
                        {
                            _reportViewer.LocalReport.LoadReportDefinition(reader);
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException("Report file not found: " + reportName);
                    }
                }

                _reportViewer.LocalReport.DataSources.Add(new ReportDataSource(tableNames[index], dataSets[index].Tables[1]));
                _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

                // Page settings
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = true;
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                _reportViewer.SetPageSettings(ps);
                // Page settings

                _reportViewer.RefreshReport();
            }
        }
    }
}