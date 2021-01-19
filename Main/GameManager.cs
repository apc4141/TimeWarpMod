using Zenject;
using BS_Utils.Utilities;
using UnityEngine;
using System.Linq;
using TimeWarpMod.Settings;

namespace TimeWarpMod.Main
{
    class GameManager : IInitializable, ILateDisposable
    {
        private const int SMOOTH_VEL = 10;

        private static AudioTimeSyncController ATSC;
        private static SaberManager SM;
        private static float _timeScale = 1f;

        internal static float TimeScale
        {
            get { return _timeScale; }
            set
            {
                _timeScale = value;
                AudioTimeSyncController.InitData initData = ATSC.GetPrivateField<AudioTimeSyncController.InitData>("_initData");
                AudioTimeSyncController.InitData newInitData = new AudioTimeSyncController.InitData(initData.audioClip, ATSC.songTime, initData.songTimeOffset, _timeScale);
                ATSC.SetPrivateField("_initData", newInitData);
                SetTimeSync(ATSC, _timeScale);
            }
        }

        public static void SetTimeSync(AudioTimeSyncController timeSync, float newTimeScale)
        {

            timeSync.SetPrivateField("_timeScale", newTimeScale);
            timeSync.SetPrivateField("_startSongTime", timeSync.songTime);
            timeSync.SetPrivateField("_playbackLoopIndex", 0);
            timeSync.audioSource.pitch = newTimeScale;
        }

        public GameManager(AudioTimeSyncController timeSync, SaberManager saberManager)
        {
            ATSC = timeSync;
            SM = saberManager;
        }

        private SuperHot superHot;

        public void Initialize()
        {
            if (TimeWarpConfig.Instance.SuperHotModifier)
                superHot = new SuperHot(SM, SMOOTH_VEL);
        }

        public void LateDispose()
        {
            if (superHot != null)
                superHot.Dispose();
        }
    }
}