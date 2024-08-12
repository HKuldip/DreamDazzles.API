using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Emails;
public class EmailRequestDTO
{
    public string email { get; set; }
    public string subject { get; set; }
    public string message { get; set; }
}

