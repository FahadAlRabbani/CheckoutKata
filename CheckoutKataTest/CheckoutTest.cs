using System;
using System.Collections.Generic;
using System.Text;
using CheckoutKata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace CheckoutKataTest
{
    [TestClass]
    public class CheckoutTest
    {
        private ICheckout _checkout;
        private IOffers _offers;

        private string _sku = "Product1";
        private float _price = 1.5F;

        public CheckoutTest()
        {
            _checkout = Substitute.For<ICheckout>();
            _offers = Substitute.For<IOffers>();
            _offers.AddProduct(_sku, _price);
            _offers.AddDeal(_sku, 3, 2.4f);
            _offers.AddDeal(_sku, 5, 3.7f);
            _offers.AddDeal(_sku, 7, 4.9f);
        }

        [TestMethod]
        public void Checkout_Scans_A_Item()
        {
            _checkout.Scan(_sku);
            _checkout.Received().Scan(_sku);
        }

        [TestMethod]
        public void Checkout_Basket_IsEmpty_True()
        {
            _checkout.Scan(_sku);
            _checkout.Empty();
            _checkout.IsEmpty().Returns(true);
            _checkout.Received().Empty();
            bool isBasketEmpty = _checkout.IsEmpty();
            Assert.IsTrue(isBasketEmpty);
        }

        [TestMethod]
        public void Checkout_Basket_IsEmpty_False()
        {
            _checkout.Scan(_sku);
            _checkout.IsEmpty().Returns(false);
            _checkout.DidNotReceive().Empty();
            bool isBasketEmpty = _checkout.IsEmpty();
            Assert.IsFalse(isBasketEmpty);
        }

        [TestMethod]
        public void Checkout_Configure_Offers()
        {
            _offers.AddProduct(_sku, _price);
            _checkout.Configure(_offers);
            _checkout.Received().Configure(Arg.Any<IOffers>());
        }

        [TestMethod]
        public void Checkout_Calculates_SubTotal_Of_Basket()
        {
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Subtotal().Returns(6.0f);
            Assert.AreEqual(6.0f, _checkout.Subtotal());
        }

        [TestMethod]
        public void Checkout_Calculates_Savings_Of_Basket()
        {
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Savings().Returns(2.1f);
            Assert.AreEqual(2.1f, _checkout.Savings());
        }

        [TestMethod]
        public void Checkout_Calculates_Total_Of_Basket()
        {
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Scan(_sku);
            _checkout.Total().Returns(3.9f);
            Assert.AreEqual(3.9f, _checkout.Total());
        }
    }
}