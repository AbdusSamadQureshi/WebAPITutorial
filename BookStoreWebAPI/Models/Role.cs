using System;
using System.Collections.Generic;

namespace BookStoreWebAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public short RoleId { get; set; }
        public string RoleDesc { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
