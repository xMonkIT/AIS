using System;
using System.Windows.Forms;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class AccountingDialog : Form
    {
        internal readonly Accounting Accounting = new Accounting();

        public AccountingDialog()
        {
            InitializeComponent();
        }

        private void AccountingDialog_Load(object sender, EventArgs e)
        {
            accountingBindingSource.DataSource = Accounting;
        }
    }
}
