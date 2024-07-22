using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BossSensors.Content.BossAlive
{
    internal class BossAliveSensorTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.DrawsWalls[Type] = true;
            TileID.Sets.DontDrawTileSliced[Type] = true;
            TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[Type] = true;
            TileID.Sets.IsAMechanism[Type] = true;

            Main.tileFrameImportant[Type] = true;

            AddMapEntry(new Color(80, 60, 60), Language.GetText("MapObject.BossAliveSensor"));
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            SetStaticDefaults();
        }
    }
}
