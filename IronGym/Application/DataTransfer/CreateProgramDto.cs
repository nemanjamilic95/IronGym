using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer
{
   public class CreateProgramDto
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-z\d\.\-]{3,50}$", ErrorMessage = "Program heading must be between 3 and 50 characters.")]
        public string Heading { get; set; }
        [RegularExpression(@"^[A-z\d]{3,649}$", ErrorMessage = "Text must must be between 3 and 650 characters.")]
        public string Text { get; set; }       
        [Required]
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }
    }
}
