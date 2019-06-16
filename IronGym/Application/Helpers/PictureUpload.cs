using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helpers
{
    public class PictureUpload
    {
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpeg", ".jpg", ".gif", ".png" };
    }
}
