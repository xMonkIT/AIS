using System.Windows.Forms;
using Practicum_1.Domain;
using Practicum_1.Report;

namespace Practicum_1
{
    internal partial class ReportForm : Form
    {
        private readonly ReportBuilder _reportBuilder;

        public ReportForm(OrderRepository rep)
        {
            InitializeComponent();
            _reportBuilder = new ReportBuilder(rep);
        }

        private void ReportForm_Load(object sender, System.EventArgs e)
        {
            UpdateData();
        }

        private void dtp_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var report = _reportBuilder.CreateReportModel(dtpBeginDate.Value, dtpEndDate.Value);
            reportByComingBindingSource.DataSource = report;
            productsGroupBindingSource.DataSource = report.ProductsGroups;
        }
    }
}
