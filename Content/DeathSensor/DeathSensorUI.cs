#nullable disable
using BossSensors.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BossSensors.Content.DeathSensor
{
    internal class DeathSensorUI : UIState
    {
        private DeathSensorTileEntity _tileEntity;
        private UIPanelTextbox _npcInput;
        private UIPanel panel;
        public override void OnInitialize()
        {
            BasicUIBuilder builder = new(this);
            builder.Container.Width.Pixels = 500;
            builder.Container.Height.Pixels = 200;
            panel = builder.Container;

            _npcInput = builder.AddNpcSelect();
            _npcInput.TextField.OnTextChange += (_, _) =>
            {
                _tileEntity.NpcName = _npcInput.CurrentString;
            };
            builder.NextRow();
            builder.AddCloseButton();
        }

        public bool SetTileEntity(DeathSensorTileEntity tileEntity)
        {
            bool changed = _tileEntity != tileEntity;
            _tileEntity = tileEntity;
            _npcInput.CurrentString = tileEntity.NpcName;
            return changed;
        }


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (panel.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
