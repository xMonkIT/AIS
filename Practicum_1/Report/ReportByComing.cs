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
        private readonly IList<SpecificationsGroup> _specificationsGroups;

        public ReportByComing(DateTime beginDate, DateTime endDate, IList<SpecificationsGroup> specificationsGroups)
        {
            _beginDate = beginDate;
            _endDate = endDate;
            _specificationsGroups = specificationsGroups;
            _createDate = DateTime.Now;
        }

        public DateTime BeginDate => _beginDate;

        public DateTime EndDate => _endDate;

        public decimal Total => _specificationsGroups.Sum(x => x.Total);

        public DateTime CreateDate => _createDate;

        public IList<SpecificationsGroup> SpecificationsGroups => _specificationsGroups;
    }
}
