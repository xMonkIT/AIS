using System;
using System.Collections.Generic;
using System.Linq;

namespace Practicum_1.Report
{
    internal class ReportByComing
    {
        private readonly DateTime _beginDate;
        private readonly DateTime _endDate;
        private readonly DateTime _createDate;
        private readonly IList<ProductsGroup> _productsGroups;

        public ReportByComing(DateTime beginDate, DateTime endDate, IList<ProductsGroup> productsGroups)
        {
            _beginDate = beginDate;
            _endDate = endDate;
            _productsGroups = productsGroups;
            _createDate = DateTime.Now;
        }

        public DateTime BeginDate => _beginDate;

        public DateTime EndDate => _endDate;

        public decimal Total => _productsGroups.Sum(x => x.Total);

        public DateTime CreateDate => _createDate;

        public IList<ProductsGroup> ProductsGroups => _productsGroups;
    }
}
