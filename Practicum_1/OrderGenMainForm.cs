using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class OrderGenMainForm : Form
    {
        readonly Order _order = new Order();

        public OrderGenMainForm()
        {
            InitializeComponent();
        }

        private void OrderGenMainForm_Load(object sender, EventArgs e)
        {
            Contract.Ensures(orderBindingSource.DataSource == _order);
            Contract.Ensures(orderItemBindingSource.DataSource == _order.OrderItems);
            orderBindingSource.DataSource = _order;
            orderItemBindingSource.DataSource = _order.OrderItems;
        }

        private void orderItemBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Contract.Requires(tbOrderTotal.DataBindings.Count > 0);
            tbOrderTotal.DataBindings[0].ReadValue();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripSearch_TextChanged(object sender, EventArgs e)
        {
            var text = sender.ToString();
            var dataTable = dgvOrderItems.DataSource as DataTable;
            if (dataTable != null)
                dataTable.DefaultView.RowFilter = String.IsNullOrEmpty(text)
                    ? ""
                    : String.Format("Specification = '{0}'", text);
        }
    }
}
