using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Emails;
public class EmailSettingsOptions
{
    public string UserDisplayName { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }
    public bool EnableSSL { get; set; }

    //in case its not SMTP
    //public string Key { get; set; }
    //public string SenderName { get; set; }
    //public string SenderEmail { get; set; }
}

