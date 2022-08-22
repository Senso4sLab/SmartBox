using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.SmartConfig;

public interface IWifiInfoService
{
    event EventHandler<WifiDeviceInfo> WifiStateChanged;

    void ActivateWifiService();
    void DeactivateWifiService();
}
