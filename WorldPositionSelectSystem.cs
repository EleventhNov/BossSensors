using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors
{
    internal class WorldPositionSelectSystem : ModSystem
    {
        private ReceivePosition? _callback;

        public delegate void ReceivePosition(Vector2 position);

        public void RequestPosition(ReceivePosition callback)
        {
            _callback = callback;
        }

        public override void PostUpdatePlayers()
        {
            if (_callback is not ReceivePosition callback) return;
            
            Vector2 mouseWorld = Main.MouseWorld;
            Dust.QuickDust(mouseWorld, Color.Red);

            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                callback(mouseWorld);
                _callback = null;
                SelectionEffect(mouseWorld);
            }
        }

        public void SelectionEffect(Vector2 position)
        {
            SoundEngine.PlaySound(SoundID.Item4, position);
            for (int i = 0; i < 8; i++)
            {
                float speedX = MathF.Sin(i * MathHelper.TwoPi * 0.125f);
                float speedY = MathF.Cos(i * MathHelper.TwoPi * 0.125f);

                Dust.NewDust(position, 4, 4, DustID.AmberBolt, speedX, speedY);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if(_callback is not null)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
