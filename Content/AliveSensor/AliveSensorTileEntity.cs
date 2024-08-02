using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.AliveSensor
{
    internal class AliveSensorTileEntity : CommonTileEntity
    {
        public override int TileType => ModContent.TileType<AliveSensorTile>();
        public string NpcName = string.Empty;
        private bool wasAlive;

        public override void Update()
        {
            bool npcAlive = false;

            if (NPCID.Search.TryGetId(NpcName, out int npcType))
            {
                npcAlive = NPC.FindFirstNPC(npcType) != -1;
            }
            
            bool stateChange = npcAlive != wasAlive;

            Tile tile = Main.tile[Position];
            tile.TileFrameY = (short)(npcAlive.ToInt() * 16);

            if (stateChange)
            {
                //Main.NewText($"{Position} {ID} State change!");
                Wiring.TripWire(Position.X, Position.Y, 1, 1);
            }

            wasAlive = npcAlive;
        }

        public override void SaveData(TagCompound tag)
        {
            if (wasAlive != default) tag["wasAlive"] = wasAlive;
            if (NpcName != string.Empty) tag["NpcName"] = NpcName;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGet("wasAlive", out wasAlive);
            tag.TryGet("NpcName", out NpcName);
        }
    }
}
