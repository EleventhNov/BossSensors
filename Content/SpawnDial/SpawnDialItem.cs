using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.SpawnDial
{
    internal class SpawnDialItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<SpawnDialTile>();
            Item.placeStyle = 0;
        }
    }
}
