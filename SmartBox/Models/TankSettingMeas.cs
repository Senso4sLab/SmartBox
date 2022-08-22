namespace SmartBox.Models;

public class TankSettingMeas
{
    public IEnumerable<IntervalMeas> Measurements { get; set; } = new List<IntervalMeas>();
    public TankSettings TankSettings { get; set; }    
}
