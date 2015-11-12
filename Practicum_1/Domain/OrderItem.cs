using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Practicum_1.Annotations;

namespace Practicum_1.Domain
{
    internal class OrderItem : INotifyPropertyChanged
    {
        private decimal _count;
        private decimal _price;
        private decimal _rateVat;

        public delegate int OrderItemToInt(OrderItem item);

        public delegate Vat ToVat();

        public event OrderItemToInt OnGetIndex;

        public event ToVat OnGetVat;

        public void Update() => OnPropertyChanged(nameof(TotalWithVat));

        /// <summary>
        /// Получает спецификацию товара в записи накладной
        /// </summary>
        public Product Product { get; set; }

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
                if (_price == value) return;
                _price = value;
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalWithVat));

            }
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
                if (_count == value) return;
                _count = value;
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalWithVat));
            }
        }

        /// <summary>
        /// Получает сумму по записи в накладной
        /// </summary>
        public decimal Total => _count * _price;

        /// <summary>
        /// Получает ставку НДС в записи накладной
        /// </summary>
        public decimal RateVat
        {
            get { return _rateVat; }
            set
            {
                Contract.Requires(IsValidRateVat(value));
                Contract.Ensures(_rateVat == value);
                if (_rateVat == value) return;
                _rateVat = value;
                OnPropertyChanged(nameof(TotalWithVat));
            }
        }

        /// <summary>
        /// Получает сумму с НДС по записи в накладной
        /// </summary>
        public decimal TotalWithVat => _count * (OnGetVat?.Invoke().GetPriceWithVat(_price, _rateVat) ?? _price);

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
        public static bool IsValidRateVat(decimal value)
        {
            return value > 0.0M;
        }

        public int Number => OnGetIndex?.Invoke(this) + 1 ?? -1;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
