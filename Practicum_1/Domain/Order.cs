using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;
using Practicum_1.Annotations;

namespace Practicum_1.Domain
{
    internal class Order : INotifyPropertyChanged
    {
        private readonly IList<OrderItem> _orderItems = new List<OrderItem>();
        private int _id;

        /// <summary>
        /// Создаёт новый экземпляр накладной
        /// </summary>
        /// <param name="id">Номер накладной</param>
        public Order(int id) {
            _id = id;
            Created = DateTime.Today;
        }

        public Order() : this(1) { }

        public OrderItem New()
        {
            var _item = new OrderItem();
            _item.PropertyChanged += ItemChanged;
            _item.GetIndex += GetIndex;
            return _item;
        }

        private int GetIndex(OrderItem item)
        {
            return _orderItems.IndexOf(item);
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Total));
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
        public DateTime Created { get; set; }

        /// <summary>
        /// Получает сумму по накладной
        /// </summary>
        public decimal Total
        {
            get
            {
                Contract.Requires(OrderItems != null, "Коллекция записей накладной должна быть создана.");
                return _orderItems.Sum(x => x.Total);
            }
        }

        /// <summary>
        /// Получает сумму с НДС по накладной
        /// </summary>
        public decimal TotalWithVAT
        {
            get
            {
                Contract.Requires(OrderItems != null, "Коллекция записей накладной должна быть создана.");
                return _orderItems.Sum(x => x.TotalWithVAT);
            }
        }

        /// <summary>
        /// Получает список записей в накладной
        /// </summary>
        public IList<OrderItem> OrderItems => _orderItems;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
