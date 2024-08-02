using BossSensors.Content.TimeDial;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BossSensors.Content.SpawnDial
{
    internal class SpawnDialTile : CommonTile
    {
        public override string Texture => ModContent.GetInstance<SpawnDialItem>().Texture;
        public override Color MapColor => Color.Green;
        public override ModTileEntity? TileEntity => ModContent.GetInstance<SpawnDialTileEntity>();
        public override void HitWire(int i, int j)
        {
            TileUtils.GetTileEntity<SpawnDialTileEntity>(i, j).HitSignal();
        }

        public override bool RightClick(int i, int j)
        {
            ModContent.GetInstance<BossSensorsUI>().ShowUI<SpawnDialUI, SpawnDialTileEntity>(TileUtils.GetTileEntity<SpawnDialTileEntity>(i, j));
            return true;
        }
    }
}
