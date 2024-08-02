#nullable disable

using System;
using System.Collections.Generic;
using BossSensors.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors.Content.Spawner
{
    internal class SpawnerUI : UIState, IStateable<SpawnerTileEntity>
    {
        private SpawnerTileEntity _tileEntity;
        private UIPanel panel;
        private UIPanelTextbox inputX;
        private UIPanelTextbox inputY;
        private UIPanelTextbox inputNpc;

        public void SetState(SpawnerTileEntity tileEntity)
        {
            _tileEntity = tileEntity;
            inputNpc.CurrentString = tileEntity.State.NpcName;
            inputX.CurrentString = tileEntity.State.SpawnX.ToString();
            inputY.CurrentString = tileEntity.State.SpawnY.ToString();
        }

        public override void OnInitialize()
        {
            BasicUIBuilder builder = new(this);
            panel = builder.Container;

            inputX = builder.AddTextbox("Placeholder X", width: 140);
            TextboxValidation.ValidateTextFieldInt(inputX.TextField, x => _tileEntity.State.SpawnX = x);
            inputY = builder.AddTextbox("Placeholder Y", width: 140);
            TextboxValidation.ValidateTextFieldInt(inputY.TextField, y => _tileEntity.State.SpawnY = y);

            var selectPositionButton = builder.AddButton("Select Position", width: 160);
            selectPositionButton.OnLeftClick += (s, e) =>
            {
                ModContent.GetInstance<WorldPositionSelectSystem>().RequestPosition(position =>
                {
                    inputX.CurrentString = ((int)position.X).ToString();
                    inputY.CurrentString = ((int)position.Y).ToString();
                });
            };

            builder.NextRow();
            inputNpc = builder.AddNpcSelect();
            TextboxValidation.ValidateTextFieldNpc(inputNpc.TextField, npc => _tileEntity.State.NpcName = npc);
            builder.NextRow();

            var spawnButton = builder.AddButton("Spawn");
            builder.AddCloseButton();

            builder.Finish();

            spawnButton.OnLeftClick += (_, _) =>
            {
                _tileEntity?.Spawn();
            };
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if(panel.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
