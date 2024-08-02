using BossSensors.Content.AliveSensor;
using BossSensors.Content.WeatherDial;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BossSensors.Content.Spawner
{
    internal class SpawnerTile : ModTile
    {
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

            // Additional edits here, such as lava immunity, alternate placements, and subtiles
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<SpawnerTileEntity>().Hook_AfterPlacement, -1, 0, true);
            // This is required so the hook is actually called.
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(40, 40, 255), CreateMapEntryName());
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            ModContent.GetInstance<SpawnerTileEntity>().Kill(i, j);
        }

        public override bool RightClick(int i, int j)
        {
            BossSensorsUI.ShowTileEntityUI<SpawnerUI, SpawnerTileEntity>(i, j);
            return true;
        }

        public override void HitWire(int i, int j)
        {
            Wiring.SkipWire(i, j);
            TileUtils.GetTileEntity<SpawnerTileEntity>(i, j).Spawn();
        }
    }
}
