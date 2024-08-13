using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Emails
{
    internal class EmailConfigration
    {
        public int From { get; set; }
        public int SmtpServer { get; set; }
        public int Port { get; set; }
        public int UserName { get; set; }
        public int Password { get; set; }
    }
}
