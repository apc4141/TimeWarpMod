using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS_Utils.Utilities;

namespace TimeWarpMod.Utils
{
    public static class TimeWarpUtils
    {
        public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public static NoteMovement GetClosestNote(this BasicBeatmapObjectManager manager)
        {
            HashSet<GameNoteController> noteControllers = manager.GetPrivateField<MonoMemoryPoolContainer<GameNoteController>>("_gameNotePoolContainer").activeItems;

            NoteMovement closest = null;
            foreach (GameNoteController noteController in noteControllers)
            {
                if (closest == null)
                {
                    closest = noteController.GetPrivateField<NoteMovement>("_noteMovement");
                    continue;
                }

                NoteMovement movement = noteController.GetPrivateField<NoteMovement>("_noteMovement");
                if (movement.distanceToPlayer <= closest.distanceToPlayer)
                {
                    closest = movement;
                }
            }

            return closest;
        }
    }
}
