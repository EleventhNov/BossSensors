using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.WeatherDial
{
    internal class WeatherDialSystem : ModSystem
    {
        // TODO these values dont save!
        public float? FrozenWindTarget;
        public float? FrozenRainTarget;

        public void SetWindTarget(float target)
        {
            Main.windSpeedCurrent = Main.windSpeedTarget = target;
        }

        public void SetRainTarget(float target)
        {
            bool raining = target > 0;
            if(raining && Main.rainTime <= 0)
            {
                Main.StartRain();
            }

            Main.raining = raining;
            Main.maxRaining = target;
            Main.cloudAlpha = target;
        }

        public override void PostUpdateWorld()
        {
            if(FrozenWindTarget is float windTarget)
            {
                SetWindTarget(windTarget);
            }
            
            if(FrozenRainTarget is float rainTarget)
            {
                SetRainTarget(rainTarget);
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (FrozenWindTarget != default) tag["FrozenWindTarget"] = FrozenWindTarget;
            if (FrozenRainTarget != default) tag["FrozenRainTarget"] = FrozenRainTarget;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            tag.TryGet("FrozenWindTarget", out FrozenWindTarget);
            tag.TryGet("FrozenRainTarget", out FrozenRainTarget);
        }
    }
}
