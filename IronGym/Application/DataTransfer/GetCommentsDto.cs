using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetCommentsDto
    {
        public int Id { get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
    }
}
