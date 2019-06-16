using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronGym.MVC.DataTransfer
{
    public class ProgramDto
    {
        public int Id { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]

        public string Text { get; set; }
        [Required]

        public IFormFile Picture { get; set; }
        
    }
}
