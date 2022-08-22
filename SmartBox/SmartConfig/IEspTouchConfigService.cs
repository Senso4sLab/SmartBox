using Com.Espressif.Iot.Esptouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.SmartConfig;

public interface IEspTouchConfigService
{
    event EventHandler<EspTouchEventArgs> EspTouch;    
    void SetPackageBroadcast(bool packageBrodcast);
    Task<IEnumerable<EspTouchResult>> ExecuteForResultsAsync(WifiDeviceInfo wifiInfo, int expectedEspTouchResult);
    void Stop();
}
