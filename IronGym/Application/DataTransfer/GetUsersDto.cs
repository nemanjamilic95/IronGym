using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetUsersDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }

        public string RoleName { get; set; }
    }
}
