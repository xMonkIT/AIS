using System.Diagnostics.Contracts;

namespace Practicum_1.Domain
{
    public class Specification
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                Contract.Requires(IsValidName(value));
                Contract.Ensures(_name == value);
                _name = value;
            }
        }

        /// <summary>
        /// Возвращает признак того, что аргумент является корректным названием спецификации
        /// </summary>
        /// <param name="value">Название спецификации</param>
        /// <returns>Истина, если аргумент является корректным названием спецификации товара</returns>
        private bool IsValidName(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}