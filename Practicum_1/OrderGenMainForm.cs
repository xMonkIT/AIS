using System;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using System.Globalization;
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
            Contract.Ensures(Equals(orderBindingSource.DataSource, _order));
            Contract.Ensures(Equals(orderItemBindingSource.DataSource, _order.OrderItems));
            orderBindingSource.DataSource = _order;
            orderItemBindingSource.DataSource = _order.OrderItems;
            orderItemBindingSource.AddingNew += (obj, args) => args.NewObject = _order.New();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
