﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BossSensors.Content;

internal abstract class CommonTileEntity : ModTileEntity
{
    public abstract int TileType { get; }

    public override bool IsTileValidForEntity(int x, int y)
    {
        Tile tile = Main.tile[x, y];
        return tile.HasTile && tile.TileType == TileType;
    }

    public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            // Sync the entire multitile's area.  Modify "width" and "height" to the size of your multitile in tiles
            int width = TileObjectData.GetTileData(TileType, -1).Width;
            int height = TileObjectData.GetTileData(TileType, -1).Height;
            NetMessage.SendTileSquare(Main.myPlayer, i, j, width, height);

            // Sync the placement of the tile entity with other clients
            // The "type" parameter refers to the tile type which placed the tile entity, so "Type" (the type of the tile entity) needs to be used here instead
            NetMessage.SendData(MessageID.TileEntityPlacement, number: i, number2: j, number3: Type);
            return -1;
        }

        // ModTileEntity.Place() handles checking if the entity can be placed, then places it for you
        int placedEntity = Place(i, j);
        return placedEntity;
    }

    public override void OnNetPlace()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendData(MessageID.TileEntitySharing, number: ID, number2: Position.X, number3: Position.Y);
        }
    }
}
