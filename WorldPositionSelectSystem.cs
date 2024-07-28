using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

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
            }
        }
    }
}
