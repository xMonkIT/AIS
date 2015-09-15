using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Practicum_1.Domain
{
    internal class Order
    {
        private readonly IList<OrderItem> _orderItems = new List<OrderItem>();
        private int _id;

        /// <summary>
        /// Создаёт новый экземпляр товарной накладной
        /// </summary>
        public Order() {
            _id = 1;
            Created = DateTime.Today;
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
    }
}
