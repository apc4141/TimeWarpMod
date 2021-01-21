using BeatSaberMarkupLanguage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWarpMod.Settings
{
    class TimeWarpSettingsUI : PersistentSingleton<TimeWarpSettingsUI>
    {
        [UIValue("superhot")]
        public bool SuperHotModifier
        {
            get
            {
                return TimeWarpConfig.Instance.SuperHotModifier;
            }
            set
            {
                TimeWarpConfig.Instance.SuperHotModifier = value;
            }
        }

        [UIValue("matrix")]
        public bool MatrixModifier
        {
            get
            {
                return TimeWarpConfig.Instance.MatrixModifier;
            }
            set
            {
                TimeWarpConfig.Instance.MatrixModifier = value;
            }
        }
    }
}
