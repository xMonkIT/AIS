using System.Collections.Generic;
using System.Linq;

namespace Practicum_1.Domain
{
    public class OrderRepository
    {
        private int _nextOrderId;

        /// <summary>
        /// Получает список накладных
        /// </summary>
        public IList<Order> Orders { get; } = new List<Order>();

        /// <summary>
        /// Получает сумму по всем накладным
        /// </summary>
        public decimal Total => Orders.Sum(x => x.Total);

        /// <summary>
        /// Получает сумму проводок по всем накладным
        /// </summary>
        public decimal TotalAccountingSum => Orders.Sum(x => x.AccountingSum);

        public Order New() => new Order(++_nextOrderId);
    }
}
