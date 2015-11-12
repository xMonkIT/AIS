using Practicum_1.Domain;

namespace Practicum_1.Report
{
    internal class ProductsGroup
    {
        private readonly decimal _total;
        private readonly decimal _count;
        private readonly Product _product;

        public ProductsGroup(Product product, decimal total, decimal count)
        {
            _total = total;
            _count = count;
            _product = product;
        }

        public decimal Total => _total;

        public decimal Count => _count;

        public Product Product => _product;
    }
}
