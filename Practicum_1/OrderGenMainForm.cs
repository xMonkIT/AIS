using System;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class OrderGenMainForm : Form
    {
        readonly OrderRepository _orderRepository = new OrderRepository();

        public OrderGenMainForm()
        {
            InitializeComponent();
        }

        private void OrderGenMainForm_Load(object sender, EventArgs e)
        {
            Contract.Ensures(Equals(orderRepositoryBindingSource.DataSource, _orderRepository));
            Contract.Ensures(Equals(orderBindingSource.DataSource, _orderRepository.Orders));
            orderRepositoryBindingSource.DataSource = _orderRepository;
            orderBindingSource.DataSource = _orderRepository.Orders;
            orderBindingSource.AddingNew += (obj, args) => args.NewObject = _orderRepository.New();
            orderBindingSource.AddNew();
            specificationBindingSource.DataSource = _orderRepository.Specifications;
        }

        private void orderBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Contract.Ensures(Equals(orderItemBindingSource.DataSource, (orderBindingSource.Current as Order).OrderItems));
            orderItemBindingSource.DataSource = (orderBindingSource.Current as Order)?.OrderItems;
            orderItemBindingSource.AddingNew += (obj, args) => args.NewObject = (orderBindingSource.Current as Order)?.New();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOrderItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }
    }
}
