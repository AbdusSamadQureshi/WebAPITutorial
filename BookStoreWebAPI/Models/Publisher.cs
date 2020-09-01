using System;
using System.Collections.Generic;

namespace BookStoreWebAPI.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Book = new HashSet<Book>();
            User = new HashSet<User>();
        }

        public int PubId { get; set; }
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public ICollection<Book> Book { get; set; }
        public ICollection<User> User { get; set; }
    }
}
