using IPA;
using IPALogger = IPA.Logging.Logger;
using SiraUtil.Zenject;
using TimeWarpMod.Main;

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
            zenjector.OnGame<MyGameInstaller>();

            log.Debug("Time Warp Mod initialized!");
        }
    }
}
