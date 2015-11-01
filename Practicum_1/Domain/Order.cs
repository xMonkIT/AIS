using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Practicum_1.Annotations;

namespace Practicum_1.Domain
{
    internal class Order : INotifyPropertyChanged
    {
        private int _id;
        private Vat _vat;
        private DateTime _created;

        public delegate void Update();

        public event Update OnVatChange;

        /// <summary>
        /// Создаёт новый экземпляр накладной
        /// </summary>
        /// <param name="id">Номер накладной</param>
        public Order(int id) {
            _id = id;
            Created = DateTime.Today;
            Vat = Vat.GetVatObject(VatType.WithoutVat);
        }

        public Order() : this(1) { }

        public OrderItem New()
        {
            var item = new OrderItem();
            item.PropertyChanged += ItemChanged;
            item.OnGetIndex += GetIndex;
            item.OnGetVat += GetVat;
            OnVatChange += item.Update;
            return item;
        }

        private int GetIndex(OrderItem item) => OrderItems.IndexOf(item);

        private Vat GetVat() => _vat;

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(TotalWithVat));
        }

        /// <summary>
        /// Получает или устанавливает номер накладной
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                Contract.Requires(IsValidId(value), "Номер накладной должен быть положительным числом");
                if (_id == value) return;
                _id = value;
            }
        }

        /// <summary>
        /// Получает или устанавливает дату создания накладной
        /// </summary>
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        public Vat Vat
        {
            get { return _vat; }
            set
            {
                _vat = value;
                OnPropertyChanged(nameof(TotalWithVat));
                OnVatChange?.Invoke();
            }
        }

        /// <summary>
        /// Получает сумму по накладной
        /// </summary>
        public decimal Total
        {
            get
            {
                Contract.Requires(OrderItems != null, "Коллекция записей накладной должна быть создана.");
                return OrderItems.Sum(x => x.Total);
            }
        }

        /// <summary>
        /// Получает сумму с НДС по накладной
        /// </summary>
        public decimal TotalWithVat
        {
            get
            {
                Contract.Requires(OrderItems != null, "Коллекция записей накладной должна быть создана.");
                return OrderItems.Sum(x => x.TotalWithVat);
            }
        }

        /// <summary>
        /// Получает список записей в накладной
        /// </summary>
        public IList<OrderItem> OrderItems { get; } = new List<OrderItem>();

        /// <summary>
        /// Возвращает признак, что заданный аргументом номер накладной имеет правильное значение
        /// </summary>
        /// <param name="value">Номер накладной</param>
        /// <returns>Истина, если номер накладной имеет правильное значение</returns>
        public static bool IsValidId(int value)
        {
            return value > 0;
        }

        public void Clear()
        {
            Contract.Requires(OrderItems != null);
            Contract.Ensures(OrderItems.Count == 0);
            OrderItems.Clear();
        }

        #region Accounting

        /// <summary>
        /// Получает или задает объект проводки, связанной с накладной
        /// </summary>
        public Accounting Accounting { get; set; }

        /// <summary>
        /// Получает сумму проводки
        /// </summary>
        public decimal AccountingSum => Accounting != null ? Total : 0M;

        /// <summary>
        /// Получает признак возможности внесения изменений в данные накладной
        /// </summary>
        public bool CanEdit => Accounting == null;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
