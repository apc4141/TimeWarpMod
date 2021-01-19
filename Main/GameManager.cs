using Zenject;
using BS_Utils.Utilities;
using UnityEngine;
using System.Linq;
using TimeWarpMod.Settings;

namespace TimeWarpMod.Main
{
    class GameManager : IInitializable, ITickable
    {
        private const int SMOOTH_VEL = 10;

        private static float minBlade = 0.1f;
        private static float maxBlade = 25f;
        private static float minController = 0.1f;
        private static float maxController = 2.7f;
        private static float minTimeScale = 0.5f;
        private static float maxTimeScale = 1.3f;

        private static AudioTimeSyncController ATSC;
        private static SaberManager SM;
        private static VRController[] controllers;
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

        private EstimateVelocity estimateOne;
        private EstimateVelocity estimateTwo;

        public void Initialize()
        {
            controllers = Object.FindObjectsOfType<VRController>();
            estimateOne = new EstimateVelocity(SMOOTH_VEL);
            estimateTwo = new EstimateVelocity(SMOOTH_VEL);
        }

        public void Tick()
        {
            if(TimeWarpConfig.Instance.SuperHotModifier)
                DoSuperHot();
        }

        public void DoSuperHot()
        {
            //Saber Velocity
            estimateOne.AddContext(controllers[0].position);
            float velOne = estimateOne.Estimate();
            estimateTwo.AddContext(controllers[1].position);
            float velTwo = estimateTwo.Estimate();

            //Saber Rotational Velocity
            float leftBlade = Map(SM.leftSaber.bladeSpeed, minBlade, maxBlade, minController, maxController);
            float rightBlade = Map(SM.rightSaber.bladeSpeed, minBlade, maxBlade, minController, maxController);

            float maxVel = Mathf.Clamp(Mathf.Max(new float[] { velOne, velTwo, leftBlade, rightBlade }), minController, maxController);
            float newTimeScale = Map(maxVel, minController, maxController, 0, maxTimeScale);
            float old = TimeScale;
            TimeScale = Mathf.Clamp(Mathf.Lerp(TimeScale, newTimeScale, Time.deltaTime), minTimeScale, 1);
            Plugin.log.Debug("Changing from: " + old + " to: " + TimeScale);
        }

        public static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}