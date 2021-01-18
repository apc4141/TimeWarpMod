using UnityEngine;

namespace TimeWarpMod.Main
{
    class EstimateVelocity
    {

        private int index;
        private float[] velocities;
        private Vector3 prevPos;

        public EstimateVelocity(int numAverage)
        {
            index = 0;
            velocities = new float[numAverage];
        }

        public void AddContext(Vector3 nextPosition)
        {
            float vel = 0;
            if (prevPos != null)
                vel = Vector3.Distance(prevPos, nextPosition)/Time.deltaTime;

            velocities[index] = vel;
            index++;
            index %= velocities.Length;

            prevPos = nextPosition;
        }

        public float Estimate()
        {
            float vel = 0;
            foreach (float i in velocities)
                vel += i;
            vel /= velocities.Length;

            return vel;
        }
    }
}
