using System;

namespace Practicum_1.Domain
{
    public enum VatType
    {
        WithoutVat,
        ExcludingVat,
        IncludingVat
    }

    public class Vat
    {
        private class ExcludingVat : Vat
        {
            public ExcludingVat()
            {
                Name = "не включая НДС";
            }

            public override decimal GetVatAmount(decimal price, decimal rateVat) => price * (rateVat / 100);

            public override decimal GetPriceWithVat(decimal price, decimal rateVat) => price + GetVatAmount(price, rateVat);
        }

        private class IncludingVat : Vat
        {
            public IncludingVat()
            {
                Name = "включая НДС";
            }

            public override decimal GetVatAmount(decimal price, decimal rateVat) => price / (1 + rateVat / 100) * rateVat / 100;

            public override decimal GetPriceWithVat(decimal price, decimal rateVat) => price;
        }

        public string Name { get; private set; } = "без НДС";

        private Vat() { }

        private static readonly Vat _withoutVat = new Vat();
        private static readonly ExcludingVat _excludingVat = new ExcludingVat();
        private static readonly IncludingVat _includingVat = new IncludingVat();

        public static Vat GetVatObject(VatType vatType)
        {
            switch (vatType)
            {
                case VatType.WithoutVat:
                    return _withoutVat;
                case VatType.ExcludingVat:
                    return _excludingVat;
                case VatType.IncludingVat:
                    return _includingVat;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vatType), vatType, null);
            }
        }

        public virtual decimal GetVatAmount(decimal price, decimal rateVat) => 0M;

        public virtual decimal GetPriceWithVat(decimal price, decimal rateVat) => price;

        public Vat GetThis => this;

        public override string ToString() => Name;
    }
}
