using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.SmartConfig;

public class EspTouchResult
{
    public string Bssid { get; }

    public bool IsCancelled { get; }

    public bool IsSucceed { get; }

    public EspTouchResult(string bssid, bool isCancelled, bool isSucceed)
    {
        this.Bssid = bssid;
        this.IsCancelled = isCancelled;
        this.IsSucceed = isSucceed;
    }
}
