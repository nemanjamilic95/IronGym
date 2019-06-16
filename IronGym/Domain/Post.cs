using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Post : BaseEntity
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
