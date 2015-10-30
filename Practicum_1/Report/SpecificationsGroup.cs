using Practicum_1.Domain;

namespace Practicum_1.Report
{
    internal class SpecificationsGroup
    {
        private readonly decimal _total;
        private readonly decimal _count;
        private readonly Specification _specification;

        public SpecificationsGroup(Specification specification, decimal total, decimal count)
        {
            _total = total;
            _count = count;
            _specification = specification;
        }

        public decimal Total => _total;

        public decimal Count => _count;

        public Specification Specification => _specification;
    }
}
