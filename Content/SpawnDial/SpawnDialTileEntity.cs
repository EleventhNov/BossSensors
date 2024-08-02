
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.SpawnDial
{
    internal class SpawnDialTileEntity : CommonTileEntity
    {
        public override int TileType => ModContent.TileType<SpawnDialTile>();
        public int StoredSpawnTileX;
        public int StoredSpawnTileY;

        public void HitSignal()
        {
            Main.spawnTileX = StoredSpawnTileX;
            Main.spawnTileY = StoredSpawnTileY;
        }

        public override void SaveData(TagCompound tag)
        {
            if (StoredSpawnTileX != default) tag["StoredSpawnTileX"] = StoredSpawnTileX;
            if (StoredSpawnTileY != default) tag["StoredSpawnTileY"] = StoredSpawnTileY;
        }

        public override void LoadData(TagCompound tag)
        {
            StoredSpawnTileX = tag.Get<int>("StoredSpawnTileX");
            StoredSpawnTileY = tag.Get<int>("StoredSpawnTileY");
        }
    }
}
