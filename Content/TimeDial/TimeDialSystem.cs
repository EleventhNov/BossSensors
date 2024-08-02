using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossSensors.Content.TimeDial
{
    internal class TimeDialSystem : ModSystem
    {
        public float? FrozenTimeToSet;

        public override void PreUpdateWorld()
        {
            if (FrozenTimeToSet is float time) SetTime(time);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (FrozenTimeToSet != default) tag["FrozenTimeToSet"] = FrozenTimeToSet;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            tag.TryGet("FrozenTimeToSet", out FrozenTimeToSet);
        }

        public void SetTime(float time)
        {
            float hour24 = time * 24;
            bool dayTime = hour24 >= 4.5 && hour24 < 19.5;

            if (dayTime)
            {
                Main.dayTime = true;
                Main.time = ((hour24 - 4.5f) * 54000) / 15f;
            }
            else
            {
                Main.dayTime = false;
                float hourNight = (hour24 >= 19.5f) ? (hour24 - 19.5f) : (hour24 + 4.5f);
                Main.time = (hourNight * 32400) / 9f;
            }
        }
    }
}
