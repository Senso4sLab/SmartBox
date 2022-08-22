using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.SmartConfig;

public class WifiDeviceInfo
{
    public string Bssid { get; set; }
    public string Ssid { get; set; }    
    public string Password { get; set; }

    public WifiDeviceInfo()
    {
        Ssid = "unknown ssid";
        Bssid = Ssid = Password = String.Empty;
    }
    public WifiDeviceInfo(string ssid, string bssid)
    {
        this.Bssid = bssid;
        this.Ssid = ssid;
        this.Password = string.Empty;
    }

    public void AddPassword(string password) =>
        this.Password = password;

    public WifiDeviceInfo Empty() => 
        new WifiDeviceInfo();

    public bool IsEqualBssid(string other) =>
        this.Bssid == other;

}
