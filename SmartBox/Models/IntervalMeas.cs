using Java.Lang;
using Kotlin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.Models;

public class IntervalMeas: IBaseDevice
{
    public string Id { get; set; }
    public int Volume { get; set; }
    public int MaxAllowedTankVolume { get; set; }
    public byte Battery { get; set; }   
    public byte Percent { get; set; }    
    public double Height { get; set; }       
    public DateTime RecordedDate { get; set; }
    
}
