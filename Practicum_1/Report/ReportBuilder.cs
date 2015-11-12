using System;
using System.Collections.Generic;
using System.Linq;
using Practicum_1.Domain;

namespace Practicum_1.Report
{
    class ReportBuilder
    {
        private readonly OrderRepository _orderRepository;

        public ReportBuilder(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public ReportByComing CreateReportModel(DateTime beginDate, DateTime endDate)
        {
            var orderItems = _orderRepository.Orders
                .Where(order => order.Created.Date >= beginDate.Date && order.Created.Date <= endDate.Date)
                .SelectMany(order => order.OrderItems)
                .ToList();
            var productsGroups =
                new HashSet<Product>(orderItems.Select(orderItem => orderItem.Product))
                    .Select(
                        product => new ProductsGroup(
                            product,
                            orderItems
                                .Where(orderItem => orderItem.Product == product)
                                .Sum(orderItem => orderItem.Total),
                            orderItems
                                .Where(orderItem => orderItem.Product == product)
                                .Sum(orderItem => orderItem.Count)))
                    .ToList();
            return new ReportByComing(beginDate, endDate, productsGroups);
        }
    }
}
