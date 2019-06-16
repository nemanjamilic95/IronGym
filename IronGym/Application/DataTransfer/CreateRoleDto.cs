using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer
{
    public class CreateRoleDto
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Role name must contain from 3 to 40 characters.")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
