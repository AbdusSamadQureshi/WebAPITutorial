using System;
using System.Collections.Generic;

namespace BookStoreWebAPI.Models
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public string StoreId { get; set; }
        public string OrderNum { get; set; }
        public DateTime OrderDate { get; set; }
        public short Quantity { get; set; }
        public string PayTerms { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public Store Store { get; set; }
    }
}
