using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using BossSensors.Content.WeatherDial;

namespace BossSensors.Content.TimeDial
{
    internal class TimeDial : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LogicSensor_Above);
            Item.createTile = ModContent.TileType<TimeDialTile>();
            Item.placeStyle = 0;
        }
    }

    internal class TimeDialTile : CommonTile
    {
        public override string Texture => ModContent.GetInstance<TimeDial>().Texture;
        public override Color MapColor => Color.LightGoldenrodYellow;
        public override ModTileEntity? TileEntity => ModContent.GetInstance<TimeDialTileEntity>();
        public override void HitWire(int i, int j)
        {
            TileUtils.GetTileEntity<TimeDialTileEntity>(i, j).HitSignal();
        }

        public override bool RightClick(int i, int j)
        {
            BossSensorsUI.ShowTileEntityUI<TimeDialUI, TimeDialTileEntity>(i, j);
            return true;
        }
    }
}
