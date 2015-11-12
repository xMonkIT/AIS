using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class OrderGenMainForm : Form
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();
        private readonly Random _rand = new Random();

        private IList<Product> Products { get; }

        private IList<ProductsGroup> ProductsGroups { get; } = new List<ProductsGroup>
        {
            new ProductsGroup("Еда", 0.12M),
            new ProductsGroup("Не еда", 0.22M)
        };

        private IList<Vat> Vats { get; } = new List<Vat>
        {
            Vat.GetVatObject(VatType.WithoutVat),
            Vat.GetVatObject(VatType.ExcludingVat),
            Vat.GetVatObject(VatType.IncludingVat)
        };

        private void AddNewOrderItem(Order order)
        {
            var orderItem = order.New();
            orderItem.Product = Products[_rand.Next(Products.Count)];
            orderItem.Count = _rand.Next(1, 100);
            orderItem.Price = _rand.Next(100);
            orderItem.RateVat = _rand.Next(30);
            order.OrderItems.Add(orderItem);
        }

        private void AddNewOrder(OrderRepository orderRepository)
        {
            var order = orderRepository.New();
            order.Accounting = _rand.Next()%2 == 0 ? new Accounting() : null;
            order.Created = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - _rand.Next(7));
            order.Vat = Vats[_rand.Next(Vats.Count)];
            for (int i = 0; i < _rand.Next(3, 20); i++) AddNewOrderItem(order);
            orderRepository.Orders.Add(order);
        }

        public OrderGenMainForm()
        {
            InitializeComponent();
            var food    = ProductsGroups[0];
            var notFood = ProductsGroups[1];
            Products = new List<Product>
            {
                new Product("Хлеб", food),
                new Product("Молоко", food),
                new Product("Колбаса", food),
                new Product("Чебурек", notFood)
            };
            for (int i = 0; i < _rand.Next(3, 10); i++) AddNewOrder(_orderRepository);
        }

        public OrderGenMainForm(Order order, IList<Product> products)
        {
            InitializeComponent();
            Products = products;
            _orderRepository.Orders.Add(order);
            tlpOrderRepository.RowStyles[0].Height = 0;
            bConductAccounting.Visible  = false;
            bnAddNewOrderItem.Visible   = false;
            bnDeleteOrderItem.Visible   = false;
        }

        private void OrderGenMainForm_Load(object sender, EventArgs e)
        {
            Contract.Ensures(Equals(orderRepositoryBindingSource.DataSource, _orderRepository));
            Contract.Ensures(Equals(orderBindingSource.DataSource, _orderRepository.Orders));
            orderRepositoryBindingSource.DataSource = _orderRepository;
            orderBindingSource.DataSource = _orderRepository.Orders;
            orderBindingSource.AddingNew += (obj, args) => args.NewObject = _orderRepository.New();
            productBindingSource.DataSource = Products;
            vatBindingSource.DataSource = Vats;
        }

        private void orderBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Contract.Ensures(Equals(orderItemBindingSource.DataSource, (orderBindingSource.Current as Order).OrderItems));
            orderItemBindingSource.DataSource = (orderBindingSource.Current as Order)?.OrderItems;
            orderItemBindingSource.AddingNew += (obj, args) => args.NewObject = (orderBindingSource.Current as Order)?.New();
            SetStateOfOrderControls();
        }

        private void SetStateOfOrderControls()
        {
            var orderControls = new List<Control>(
                new Control[]
                {
                    tbOrderNumber,
                    dtpOrderDate,
                    cbVatType,
                    bnOrderItems,
                    dgvOrderItems,
                    bConductAccounting
                });
            var canEdit = (orderBindingSource.Current as Order)?.CanEdit;
            if (canEdit == null) return;
            orderControls.ForEach(x => x.Enabled = (bool) canEdit);
            SetStateOfAccountingButton();
        }

        private void bConductAccounting_Click(object sender, EventArgs e)
        {
            var accountingDialog = new AccountingDialog();
            if (accountingDialog.ShowDialog() != DialogResult.OK) return;
            var order = orderBindingSource.Current as Order;
            if (order != null)
                order.Accounting = accountingDialog.Accounting;
            SetStateOfOrderControls();
        }

        private void bCreateReport_Click(object sender, EventArgs e)
        {
            new ReportForm(_orderRepository).Show();
        }

        private void SetStateOfAccountingButton() => bConductAccounting.Enabled = orderItemBindingSource.Count != 0;

        private void orderItemBindingSource_CurrentChanged(object sender, EventArgs e) => SetStateOfAccountingButton();

        private void dgvOrderItems_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var property = dgvOrderItems.Columns[e.ColumnIndex].DataPropertyName;
            orderItemBindingSource.Sort = Equals(orderItemBindingSource.Sort, $"{property} ASC")
                ? $"{property} DESC"
                : $"{property} ASC";
            var isDesc = orderItemBindingSource.Sort.EndsWith("DESC");
            orderItemBindingSource.DataSource =
                (orderBindingSource.Current as Order)?.OrderItems?.OrderBy($"{property}{(isDesc ? " descending" : "")}");
        }
    }
}
