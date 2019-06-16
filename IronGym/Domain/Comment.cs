using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

    }
}
