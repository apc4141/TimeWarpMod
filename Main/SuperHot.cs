using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWarpMod.Util;
using UnityEngine;
using Zenject;

namespace TimeWarpMod.Main
{
    class SuperHot : IInitializable, ITickable, IDisposable
    {
        private static float minBlade = 0.1f;
        private static float maxBlade = 25f;
        private static float minController = 0.1f;
        private static float maxController = 2.7f;
        private static float minTimeScale = 0.5f;
        private static float maxTimeScale = 1.3f;

        private SaberManager SM;
        private VRController[] controllers;
        private EstimateVelocity estimateOne;
        private EstimateVelocity estimateTwo;

        private int velSmooth;

        public SuperHot(SaberManager SM, int velSmooth)
        {
            this.velSmooth = velSmooth;
            this.SM = SM;
        }

        public void Initialize()
        {
            controllers = GameObject.FindObjectsOfType<VRController>();
            estimateOne = new EstimateVelocity(velSmooth);
            estimateTwo = new EstimateVelocity(velSmooth);
        }

        public void Tick()
        {
            //Saber Velocity
            estimateOne.AddContext(controllers[0].position);
            float velOne = estimateOne.Estimate();
            estimateTwo.AddContext(controllers[1].position);
            float velTwo = estimateTwo.Estimate();

            //Saber Rotational Velocity
            float leftBlade = SM.leftSaber.bladeSpeed.Map(minBlade, maxBlade, minController, maxController);
            float rightBlade = SM.rightSaber.bladeSpeed.Map(minBlade, maxBlade, minController, maxController);

            float maxVel = Mathf.Clamp(Mathf.Max(new float[] { velOne, velTwo, leftBlade, rightBlade }), minController, maxController);
            float newTimeScale = maxVel.Map(minController, maxController, 0, maxTimeScale);
            float old = GameManager.TimeScale;
            GameManager.TimeScale = Mathf.Clamp(Mathf.Lerp(GameManager.TimeScale, newTimeScale, Time.deltaTime), minTimeScale, 1);
            Plugin.log.Debug("Changing from: " + old + " to: " + GameManager.TimeScale);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
