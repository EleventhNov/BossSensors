using BossSensors.Content.Spawner;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossSensors.Content.WeatherDial
{
    internal class WeatherDial : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<WeatherDialTile>();
            Item.placeStyle = 0;
        }
    }

    internal class WeatherDialTile : CommonTile
    {
        public override string Texture => ModContent.GetInstance<WeatherDial>().Texture;
        public override Color MapColor => Color.LightGoldenrodYellow;
        public override ModTileEntity? TileEntity => ModContent.GetInstance<WeatherDialTileEntity>();
        public override void HitWire(int i, int j)
        {
            TileUtils.GetTileEntity<WeatherDialTileEntity>(i, j).HitSignal();
        }

        public override bool RightClick(int i, int j)
        {
            ModContent.GetInstance<BossSensorsUI>().ShowWeatherDialUI(TileUtils.GetTileEntity<WeatherDialTileEntity>(i, j));
            return true;
        }
    }
}
