using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Practicum_1.Domain;

namespace Practicum_1
{
    public partial class OrderGenMainForm : Form
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();

        private IList<Specification> Specifications { get; } = new List<Specification>(
            new[]
            {
                new Specification("Хлеб"),
                new Specification("Молоко"),
                new Specification("Колбаса"),
                new Specification("Чебурек")
            });

        private IList<Vat> Vats { get; } = new List<Vat>(
            new[]
            {
                Vat.GetVatObject(VatType.WithoutVat),
                Vat.GetVatObject(VatType.ExcludingVat),
                Vat.GetVatObject(VatType.IncludingVat)
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
            specificationBindingSource.DataSource = Specifications;
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
    }
}
