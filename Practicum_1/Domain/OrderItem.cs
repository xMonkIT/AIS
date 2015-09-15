﻿using System.Diagnostics.Contracts;

namespace Practicum_1.Domain
{
    internal class OrderItem
    {
        private decimal _count;
        private decimal _price;
        private decimal _rateVAT;
        private Specification _specification;

        /// <summary>
        /// Получает сумму по записи в накладной
        /// </summary>
        public decimal Total
        {
            get { return _count*_price; }
        }

        /// <summary>
        /// Получает цену за единицу товара в записи накладной
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                Contract.Requires(IsValidPrice(value));
                Contract.Ensures(_price == value);
                _price = value;
            }
        }

        /// <summary>
        /// Получает цену за единицу товара в записи накладной
        /// </summary>
        public Specification Specification {
            get { return _specification; }
            set { _specification = value; }
        }

        /// <summary>
        /// Получает количество единиц товара в записи накладной
        /// </summary>
        public decimal Count
        {
            get { return _count; }
            set
            {
                Contract.Requires(IsValidCount(value));
                Contract.Ensures(_count == value);
                _count = value;
            }
        }

        /// <summary>
        /// Получает ставку НДС в записи накладной
        /// </summary>
        public decimal RateVAT
        {
            get { return _rateVAT; }
            set
            {
                Contract.Requires(IsValidRateVAT(value));
                Contract.Ensures(_rateVAT == value);
                _rateVAT = value;
            }
        }

        /// <summary>
        /// Получает сумму НДС в записи накладной
        /// </summary>
        public decimal TotalVAT
        {
            get { return _rateVAT * Total / 100; }
        }

        /// <summary>
        /// Получает сумму с НДС по записи накладной
        /// </summary>
        public decimal TotalWithVAT
        {
            get { return Total + TotalVAT; }
        }

        /// <summary>
        /// Возвращает признак того, что аргумент является корректной ценой
        /// </summary>
        /// <param name="value">Цена</param>
        /// <returns>Истина, если аргумент является корректной ценой</returns>
        public static bool IsValidPrice(decimal value)
        {
            return value > 0.0M;
        }

        /// <summary>
        /// Возвращает признак того, что аргумент является корректным количеством
        /// </summary>
        /// <param name="value">Цена</param>
        /// <returns>Истина, если аргумент является корректным количеством</returns>
        public static bool IsValidCount(decimal value)
        {
            return value > 0.0M;
        }

        /// <summary>
        /// Возвращает признак того, что аргумент является корректной ставкой НДС
        /// </summary>
        /// <param name="value">Ставка НДС</param>
        /// <returns>Истина, если аргумент является корректной ставкой НДС</returns>
        public static bool IsValidRateVAT(decimal value)
        {
            return value > 0.0M;
        }

        public int Number
        {
            get { return 0; }
        }
    }
}