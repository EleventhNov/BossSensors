using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.TimeDial
{
    internal class TimeDialTileEntity : CommonTileEntity
    {
        public override int TileType => ModContent.TileType<TimeDialTile>();
        public float TimeToSet;
        public bool FreezeTime;
        public void HitSignal()
        {
            var system = ModContent.GetInstance<TimeDialSystem>();
            system.SetTime(TimeToSet);
            system.FrozenTimeToSet = FreezeTime ? TimeToSet : null;
        }

        public override void SaveData(TagCompound tag)
        {
            if (TimeToSet != default) tag["TimeToSet"] = TimeToSet;
            if (FreezeTime != default) tag["FreezeTime"] = FreezeTime;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGet("TimeToSet", out TimeToSet);
            tag.TryGet("FreezeTime", out FreezeTime);
        }
    }

    internal enum WeatherDialFreezingMode
    {
        DoNotModify,
        EnableNaturalChanging,
        DisableNaturalChanging,
    }
}
