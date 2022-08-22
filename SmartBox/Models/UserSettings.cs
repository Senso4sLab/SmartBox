
using SmartBox.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.Models;

public class UserSettings : IBaseDevice
{
    public string Id { get; set; }
    public Unit Unit { get; set; }
    public Interval  Interval { get; set; }
    public Language Language { get; set; }
    
}
