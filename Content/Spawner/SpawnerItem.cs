using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.Spawner
{
    internal class SpawnerItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<SpawnerTile>();
            Item.placeStyle = 0;
        }
    }
}
