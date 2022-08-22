using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBox.Enums;

public enum Interval : uint
{
    MINUTELY = 60000,
    HOURLY   = 3600000,
    DAILY    = 86400000,
    WEEKLY   = 604800000,
    MONTHLY  = 2592000000,
};
