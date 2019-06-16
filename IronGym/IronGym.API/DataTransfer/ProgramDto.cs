using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronGym.API.DataTransfer
{
    public class ProgramDto
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public IFormFile Picture { get; set; }
        
    }
}
