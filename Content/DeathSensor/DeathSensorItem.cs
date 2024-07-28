using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.DeathSensor
{
    internal class DeathSensorItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<DeathSensorTile>();
            Item.placeStyle = 0;
        }
    }
}
