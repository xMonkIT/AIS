using System.Collections.Generic;
using System.Linq;

namespace Practicum_1.Domain
{
    class OrderRepository
    {
        /// <summary>
        /// Получает список накладных
        /// </summary>
        public IEnumerable<Order> Orders { get; } = new List<Order>();

        /// <summary>
        /// Получает сумму по всем накладным
        /// </summary>
        public decimal Total
        {
            get { return Orders.Sum(x => x.Total); }
        }
    }
}
