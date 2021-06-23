using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWheelZoomLibrary
{
    static public class Extension
    {
        static public string ToStringInvariant(this double val) => val.ToString(CultureInfo.InvariantCulture);
    }
}
