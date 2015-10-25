using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class OrderGenMainForm : Form
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();

        private IList<Specification> _specifications { get; } = new List<Specification>
            (new[]
            {
                new Specification("Хлеб"),
                new Specification("Молоко"),
                new Specification("Колбаса"),
                new Specification("Чебурек")
            });

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
            specificationBindingSource.DataSource = _specifications;
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
    }
}
