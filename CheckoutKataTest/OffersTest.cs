using System;
using System.Collections.Generic;
using System.Text;
using CheckoutKata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CheckoutKataTest
{
    [TestClass]
    public class OffersTest
    {
        private IOffers _offers;

        public OffersTest()
        {
            _offers = Substitute.For<IOffers>();
        }

        private string _sku = "Product1";

        private float _price = 2.4f;
        private float _price2 = 3.7f;

        private int _quantity = 3;
        private int _quantity2 = 5;


        [TestMethod]
        public void Offers_Does_AddProduct()
        {
            _offers.AddProduct(_sku, _price);

            _offers.Received().AddProduct(Arg.Is(_sku), Arg.Is(_price));
        }

        [TestMethod]
        public void Offers_Does_AddDeal()
        {
            _offers.AddDeal(_sku, _quantity, _price);

            _offers.Received().AddDeal(_sku, _quantity, _price);
        }

        [TestMethod]
        public void Offers_DoesHaveMoreDeals()
        {
            _offers.AddDeal(_sku, _quantity, _price);

            _offers.AddDeal(_sku, _quantity2, _price2);

            _offers.Received(2).AddDeal(Arg.Is<string>(s => s != ""), Arg.Is<int>(c => c > 0), Arg.Is<float>(p => p > 0.0f));
        }
    }
}
