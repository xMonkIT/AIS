using System.Collections.Generic;
using System.Linq;

namespace Practicum_1.Domain
{
    class OrderRepository
    {
        private readonly IList<Order> _orders = new List<Order>();

        /// <summary>
        /// Получает список накладных
        /// </summary>
        public IList<Order> Orders => _orders;

        /// <summary>
        /// Получает сумму по всем накладным
        /// </summary>
        public decimal Total
        {
            get { return _orders.Sum(x => x.Total); }
        }
    }
}
