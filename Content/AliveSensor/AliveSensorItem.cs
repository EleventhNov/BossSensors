using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.AliveSensor
{
    internal class AliveSensorItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<AliveSensorTile>();
            Item.placeStyle = 0;
        }
    }
}
