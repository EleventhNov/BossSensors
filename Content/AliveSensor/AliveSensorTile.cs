using BossSensors.Content.Spawner;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BossSensors.Content.AliveSensor
{
    internal class AliveSensorTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.DrawsWalls[Type] = true;
            TileID.Sets.DontDrawTileSliced[Type] = true;
            TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[Type] = true;
            TileID.Sets.IsAMechanism[Type] = true;


            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<AliveSensorTileEntity>().Hook_AfterPlacement, -1, 0, true);
            // This is required so the hook is actually called.
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(80, 60, 60), CreateMapEntryName());
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            ModContent.GetInstance<AliveSensorTileEntity>().Kill(i, j);
        }

        public override bool RightClick(int i, int j)
        {
            BossSensorsUI.ShowTileEntityUI<AliveSensorUI, AliveSensorTileEntity>(i, j);
            return true;
        }
    }
}
