using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Services
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class MailSettings
    {
        public string From { get; set; } 
        public string Host { get; set; } 
        public int Port { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string DisplayName { get; set; } 


    }
}
