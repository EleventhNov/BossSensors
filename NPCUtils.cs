using Microsoft.Xna.Framework;
using Terraria;

namespace BossSensors
{
    internal static class NPCUtils
    {
        public static NPC? GetNearestNpc(Vector2 position)
        {
            NPC? curNpc = null;
            float curDistSq = float.PositiveInfinity;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                float distSq = npc.DistanceSQ(position);
                if (distSq > curDistSq) continue;
                curNpc = npc;
                curDistSq = distSq;
            }

            return curNpc;
        }
    }
}
