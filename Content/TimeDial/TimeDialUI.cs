using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BossSensors.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BossSensors.Content.TimeDial
{
    internal class TimeDialUI : UIState
    {
        private TimeDialTileEntity _tileEntity;
        private UISlider slider;
        private UIOptionSelect selectFreeze;
        private UIPanel container;
        public override void OnInitialize()
        {
            BasicUIBuilder builder = new(this);
            container = builder.Container;
            slider = new();
            slider.Width.Pixels = 200;
            slider.Height.Pixels = 20;
            builder.AddToCurrentRow(slider);
            builder.NextRow();

            selectFreeze = new UIOptionSelect(["Do not freeze time", "Freeze time"]);
            selectFreeze.Width.Pixels = 200;
            selectFreeze.Height.Pixels = 40;
            builder.AddToCurrentRow(selectFreeze);
            selectFreeze.OnOptionChange += (_, value) =>
            {
                _tileEntity.FreezeTime = value == 1;
            };
            builder.NextRow();

            var activate = builder.AddButton("Activate");
            activate.OnLeftClick += (_, _) => _tileEntity.HitSignal();
            builder.AddCloseButton();
            builder.NextRow();
            slider.OnChangeValue += (_, value) =>
            {
                value = MathF.Round(value * 48) / 48;
                _tileEntity.TimeToSet = value;
                _tileEntity.HitSignal();
            };
            builder.Finish();
        }

        public bool SetTileEntity(TimeDialTileEntity tileEntity)
        {
            bool different = _tileEntity != tileEntity;
            _tileEntity = tileEntity;
            slider.Value = _tileEntity.TimeToSet;
            selectFreeze.OptionIndex = _tileEntity.FreezeTime.ToInt();
            return different;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (container.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
