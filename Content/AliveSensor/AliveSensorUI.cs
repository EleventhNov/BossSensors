#nullable disable
using BossSensors.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BossSensors.Content.AliveSensor
{
    internal class AliveSensorUI : UIState, IStateable<AliveSensorTileEntity>
    {
        private AliveSensorTileEntity _tileEntity;
        private UIPanelTextbox _npcInput;
        private UIPanel panel;

        public override void OnInitialize()
        {
            BasicUIBuilder builder = new(this);
            panel = builder.Container;

            _npcInput = builder.AddNpcSelect();
            _npcInput.TextField.OnTextChange += (_, _) =>
            {
                _tileEntity.NpcName = _npcInput.CurrentString;
            };
            builder.NextRow();
            builder.AddCloseButton();
            builder.Finish();
        }

        public void SetState(AliveSensorTileEntity tileEntity)
        {
            _tileEntity = tileEntity;
            _npcInput.CurrentString = tileEntity.NpcName;
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
