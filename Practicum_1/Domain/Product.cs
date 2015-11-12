using System.Diagnostics.Contracts;

namespace Practicum_1.Domain
{
    public class Product
    {
        private string _name;

        public Product(string name, ProductsGroup productsGroup)
        {
            Name = name;
            ProductsGroup = productsGroup;
        }

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
        /// Возвращает группу товаров, к которой принадлежит товар
        /// </summary>
        public ProductsGroup ProductsGroup { get; }

        public Product GetThis => this;

        /// <summary>
        /// Возвращает признак того, что аргумент является корректным названием спецификации
        /// </summary>
        /// <param name="value">Название спецификации</param>
        /// <returns>Истина, если аргумент является корректным названием спецификации товара</returns>
        private static bool IsValidName(string value) => !string.IsNullOrWhiteSpace(value);

        public override string ToString() => Name;
    }
}