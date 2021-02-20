using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Models
{
    public class Offer
    {
        public string sku { get; set; }
        public int count { get; set; }
        public float offerPrice { get; set; }

        public Offer(string _sku, int _count, float _offerPrice)
        {
            sku = _sku;
            count = _count;
            offerPrice = _offerPrice;
        }
    }
}
