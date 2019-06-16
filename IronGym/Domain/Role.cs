using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Role : BaseEntity
    {
        public static int DefaultRoleId = 2;
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
