using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.BossAlive
{
    internal class BossAliveSensorItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<BossAliveSensorTile>();
        }
    }
}
