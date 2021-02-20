using CheckoutKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Offers : IOffers
    {
        public List<Product> _Products = new List<Product>();

        public List<Offer> _Deals = new List<Offer>();

        public void AddProduct(string sku, float price)
        {
            if (sku.Length > 0 && price > 0.0f)
                _Products.Add(new Product(sku, price));
        }

        public void AddDeal(string sku, int count, float price)
        {
            if (sku.Length > 0 && count > 0 && price > 0.0f)
                _Deals.Add(new Offer(sku, count, price));
        }

        public float GetProductPrice(string sku)
        {
            return _Products.Find(p => p.sku == sku).price;
        }

        public IEnumerable<Offer> GetDealForProduct(string sku)
        {
            return _Deals.FindAll(deal => deal.sku == sku);
        }
    }

    public interface IOffers
    {
        void AddProduct(string sku, float price);
        void AddDeal(string sku, int count, float price);
        float GetProductPrice(string sku);
        IEnumerable<Offer> GetDealForProduct(string sku);
    }
}
