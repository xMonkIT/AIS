namespace Practicum_1.Domain
{
    /// <summary>
    /// Группа товаров накладной
    /// </summary>
    public class ProductsGroup
    {
        /// <summary>
        /// Создаёт новую группу товаров
        /// </summary>
        /// <param name="name">Название группы товаров</param>
        /// <param name="markup">Наценка на группу товаров</param>
        public ProductsGroup(string name, decimal markup)
        {
            Name = name;
            Markup = markup;
        }

        /// <summary>
        /// Возвращает название группы товаров
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает наценку на группу товаров
        /// </summary>
        public decimal Markup { get; }
    }
}
