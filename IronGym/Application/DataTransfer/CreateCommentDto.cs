using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer
{
    public class CreateCommentDto
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Comment must contain at least 3 characters.")]
        [MaxLength(350, ErrorMessage = "Comment must contain 350 characters  maximum.")]
        public string Text { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
