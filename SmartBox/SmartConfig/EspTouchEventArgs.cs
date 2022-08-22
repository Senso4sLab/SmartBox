using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.SmartConfig
{
    public class EspTouchEventArgs : EventArgs
    {
        public EspTouchResult EspTouchResult { get; }
        public EspTouchEventArgs(EspTouchResult espTouchResult)
        {
            this.EspTouchResult = espTouchResult;
        }
    }
}
