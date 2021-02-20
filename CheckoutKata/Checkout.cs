using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        public List<string> BasketList = new List<string>();

        private IOffers _offers;

        public void Configure(IOffers offers)
        {
            _offers = offers;
        }

        public void Scan(string sku)
        {
            BasketList.Add(sku);
        }

        public void Remove(string sku)
        {
            BasketList.Remove(sku);
        }

        public void Empty()
        {
            BasketList.Clear();
        }

        public float Subtotal()
        {
            return BasketList.Distinct().Sum(s => BasketList.Count(t => t == s) * _offers.GetProductPrice(s));
        }

        public float Savings()
        {
            var totalSavings = 0.0f;
            var distinctBasket = BasketList.Distinct();
            foreach (var sku in distinctBasket)
            {
                var deals = _offers.GetDealForProduct(sku).OrderByDescending(d => d.count).ToList();

                if (!deals.Any()) continue;

                var countOfItems = BasketList.Count(s => s == sku);

                var basePrice = _offers.GetProductPrice(sku);

                var originalPrice = countOfItems * basePrice;

                var savings = 0.0f;
                foreach (var deal in deals)
                {
                    if (countOfItems < deal.count) continue;
                    savings += deal.offerPrice;
                    countOfItems -= deal.count;
                    break;
                }

                savings += countOfItems * basePrice;
                totalSavings = originalPrice - savings;
            }

            return totalSavings;
        }

        public float Total()
        {
            return Subtotal() - Savings();
        }
    }

    public interface ICheckout
    {
        void Configure(IOffers offers);
        void Scan(string sku);
        void Remove(string sku);
        void Empty();
        float Subtotal();
        float Savings();
        float Total();
    }
}
