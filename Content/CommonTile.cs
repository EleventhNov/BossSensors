using BossSensors.Content.AliveSensor;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BossSensors.Content
{
    internal abstract class CommonTile : ModTile
    {
        public abstract Color MapColor { get; }
        public virtual ModTileEntity? TileEntity => null;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.DrawsWalls[Type] = true;
            TileID.Sets.DontDrawTileSliced[Type] = true;
            TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[Type] = true;
            TileID.Sets.IsAMechanism[Type] = true;

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16];
            TileObjectData.newTile.CoordinatePadding = 2;

            if (TileEntity is not null)
            {
                TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(TileEntity.Hook_AfterPlacement, -1, 0, true);
                // This is required so the hook is actually called.
                TileObjectData.newTile.UsesCustomCanPlace = true;
            }

            TileObjectData.addTile(Type);

            AddMapEntry(MapColor, CreateMapEntryName());
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            TileEntity?.Kill(i, j);
        }
    }
}
