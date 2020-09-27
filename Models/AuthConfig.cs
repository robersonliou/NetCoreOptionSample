using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreOptionSample.Models
{
    public class AuthConfig
    {
        public AuthConfig()
        {
            Console.WriteLine($"AuthConfig be created: {DateTime.Now}");
        }
        public string LoginUrl { get; set; }
        public string RedirectTo { get; set; }
        public string AppName { get; set; }
        public string AppSecret { get; set; }
    }
}
