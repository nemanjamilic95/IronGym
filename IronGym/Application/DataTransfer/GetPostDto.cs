using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public int Comments { get; set; }
        public int Likes { get; set; }
    }
}
