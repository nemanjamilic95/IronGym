using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetLikeDto
    {
 
        public int PostId { get; set; }
        public int LikesPerPost { get; set; }
        public string PostName { get; set; }
    }
}
