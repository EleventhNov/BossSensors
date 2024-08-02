using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.DeathSensor
{
    internal class DeathSensorTileEntity : CommonTileEntity
    {
        public override int TileType => ModContent.TileType<DeathSensorTile>();
        public string NpcName = string.Empty;

        public override void PostGlobalUpdate()
        {
            ModContent.GetInstance<DeathSensorGlobalNPC>().ClearNpcDeaths();
        }

        public override void Update()
        {
            if (NPCID.Search.TryGetId(NpcName, out int npcType))
            {
                if (ModContent.GetInstance<DeathSensorGlobalNPC>().HasNpcDied(npcType))
                {
                    Wiring.TripWire(Position.X, Position.Y, 1, 1);
                }
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (NpcName != string.Empty) tag["NpcName"] = NpcName;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGet("NpcName", out NpcName);
        }
    }
}
