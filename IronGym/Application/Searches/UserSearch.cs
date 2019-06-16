using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class UserSearch
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? OnlyActive { get; set; }
        public int? RoleId { get; set; }
        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}
