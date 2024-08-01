using BossSensors.Content.Spawner;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.WeatherDial
{
    internal class WeatherDialTileEntity : CommonTileEntity
    {
        public override int TileType => ModContent.TileType<WeatherDialTile>();

        public float? WindTarget;
        public float? RainTarget;
        public WeatherDialFreezingMode FreezeWind;
        public WeatherDialFreezingMode FreezeRain;

        public void HitSignal()
        {
            WeatherDialSystem system = ModContent.GetInstance<WeatherDialSystem>();
            if (WindTarget is float windTarget) system.SetWindTarget(windTarget);
            if (RainTarget is float rainTarget) system.SetRainTarget(rainTarget);

            switch(FreezeWind)
            {
                case WeatherDialFreezingMode.DisableNaturalChanging:
                    system.FrozenWindTarget = WindTarget;
                    break;
                case WeatherDialFreezingMode.EnableNaturalChanging:
                    system.FrozenWindTarget = null;
                    break;
            }

            switch (FreezeRain)
            {
                case WeatherDialFreezingMode.DisableNaturalChanging:
                    system.FrozenRainTarget = RainTarget;
                    break;
                case WeatherDialFreezingMode.EnableNaturalChanging:
                    system.FrozenRainTarget = null;
                    break;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (WindTarget != default) tag["WindTarget"] = WindTarget;
            if (RainTarget != default) tag["RainTarget"] = RainTarget;
            if (FreezeWind != default) tag["FreezeWind"] = (int)FreezeWind;
            if (FreezeRain != default) tag["FreezeRain"] = (int)FreezeRain;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGet("WindTarget", out WindTarget);
            tag.TryGet("RainTarget", out RainTarget);
            if (tag.TryGet("FreezeWind", out int freezeWind)) FreezeWind = (WeatherDialFreezingMode)freezeWind;
            if (tag.TryGet("FreezeRain", out int freezeRain)) FreezeRain = (WeatherDialFreezingMode)freezeRain;
        }
    }

    internal enum WeatherDialFreezingMode
    {
        DoNotModify,
        EnableNaturalChanging,
        DisableNaturalChanging,
    }
}
