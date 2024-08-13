using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DreamDazzle.Model;
public enum ActionEnum
{
    None = 0,
    Insert = 1,
    Update = 2,
    Delete = 3
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobType
{
    [Display(Name = "Weekly")]
    Weekly = 1,
    [Display(Name = "Daily")]
    Daily = 2,
}

public enum Gender
{
    Undisclosed,
    Male,
    Female,
    Other
}