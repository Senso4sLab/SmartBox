using Kotlin;
using SmartBox.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.Models;

public class TankSettings : IBaseDevice
{
    public string Id { get; set; }
    public UserSettings UserSettings { get; set; }
    public DateTime SetupDate { get; set; }
    IEnumerable<HvPair> HvPairs { get; set; } = new List<HvPair>();
    public int FillingLimit { get; set; }
    public TankShape TankShape { get; set; }
    public Density Density { get; set; }    
}
