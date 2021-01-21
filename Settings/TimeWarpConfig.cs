using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWarpMod.Settings
{
    class TimeWarpConfig
    {
        public static TimeWarpConfig Instance {get; set;}

        public bool SuperHotModifier 
        {
            get;
            set;
        }

        public bool MatrixModifier
        {
            get;
            set;
        }
    }
}
