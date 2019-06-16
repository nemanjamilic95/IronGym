using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer
{
    public class CreateUserDto
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-z\d\.\-]{3,20}$", ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string Username { get; set; }
        [RegularExpression(@"^[A-z\d]{5,}$", ErrorMessage = "Password must contain at least 5 characters.")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^([A-ZŠĐČĆŽ][a-zšđčćž]{2,19})(\s[A-ZŠĐČĆŽ][a-zšđčćž]{2,19})?$", ErrorMessage = "First name must be between 3 and 20 characters,first letter capital.")]
        public string FirstName { get; set; }
        [RegularExpression(@"^([A-ZŠĐČĆŽ][a-zšđčćž]{2,19})(\s[A-ZŠĐČĆŽ][a-zšđčćž]{2,19})?$", ErrorMessage = "Last name must be between 3 and 20 characters,first letter capital.")]
        public string LastName { get; set; }
        [Required]
        public string Avatar { get; set; }
        [Required]
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
