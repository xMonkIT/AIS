using System;

namespace Practicum_1.Domain
{
    /// <summary>
    /// Бухгалтерская проводка
    /// </summary>
    public class Accounting
    {
        /// <summary>
        /// Получает и задает дату проводки
        /// </summary>
        public DateTime AccountingDate { get; set; }

        /// <summary>
        /// Поучает и задает описание проводки
        /// </summary>
        public string Description { get; set; }
    }
}
