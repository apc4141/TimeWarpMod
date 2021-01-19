using IPA;
using IPALogger = IPA.Logging.Logger;
using SiraUtil.Zenject;
using TimeWarpMod.Main;
using BeatSaberMarkupLanguage.GameplaySetup;
using TimeWarpMod.Settings;

namespace TimeWarpMod
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static bool hitForSpeedMode = false;

        public static IPALogger log { get; private set; }

        [Init]
        public Plugin(Zenjector zenjector, IPALogger logger)
        {
            log = logger;
            zenjector.OnGame<GameInstaller>();

            TimeWarpConfig.Init();

            log.Debug("Time Warp Mod initialized!");
        }

        [OnEnable]
        public void OnEnable()
        {
            PersistentSingleton<GameplaySetup>.instance.AddTab("Time Warp", "TimeWarpMod.Views.modifiers.bsml", PersistentSingleton<TimeWarpSettingsUI>.instance);
        }
    }
}
