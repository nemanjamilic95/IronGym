using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronGym.MVC.DataTransfer
{
    public class PostDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Post heading must contain et least 3,up to 40 characters.")]
        public string Heading { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Post text must contain et least 3 characters.")]
        public string Text { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
