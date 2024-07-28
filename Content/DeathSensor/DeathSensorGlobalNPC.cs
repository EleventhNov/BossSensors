using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BossSensors.Content.DeathSensor
{
    internal class DeathSensorGlobalNPC : GlobalNPC
    {
        private static readonly HashSet<int> deadNpcs = [];

        public bool HasNpcDied(int npcType)
        {
            return deadNpcs.Contains(npcType);
        }

        public void ClearNpcDeaths()
        {
            deadNpcs.Clear();
        }

        public override void OnKill(NPC npc)
        {
            deadNpcs.Add(npc.type);
        }
    }
}
