using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.Spawner;

internal class SpawnerTileEntity : CommonTileEntity
{
    public SpawnerState State = new();
    public override int TileType => ModContent.TileType<SpawnerTile>();

    public void Spawn()
    {
        NPC.NewNPC(new EntitySource_Spawner(), State.SpawnX, State.SpawnY, NPCID.Search.GetId(State.NpcName));
    }

    public override void SaveData(TagCompound tag)
    {
        if (State.NpcName != string.Empty) tag["NpcName"] = State.NpcName;
        if (State.SpawnX != default) tag["SpawnX"] = State.SpawnX;
        if (State.SpawnY != default) tag["SpawnY"] = State.SpawnY;
    }

    public override void LoadData(TagCompound tag)
    {
        tag.TryGet("NpcName", out State.NpcName);
        tag.TryGet("SpawnX", out State.SpawnX);
        tag.TryGet("SpawnY", out State.SpawnY);
    }
}
