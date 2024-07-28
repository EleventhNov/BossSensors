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
    internal class SpawnerUI : UIState
    {
        private SpawnerTileEntity _tileEntity;
        private UIPanel panel;
        private UIPanelTextbox inputX;
        private UIPanelTextbox inputY;
        private UIPanelTextbox inputNpc;

        public void SetTileEntity(SpawnerTileEntity tileEntity)
        {
            _tileEntity = tileEntity;
            inputNpc.CurrentString = tileEntity.State.NpcName;
            inputX.CurrentString = tileEntity.State.SpawnX.ToString();
            inputY.CurrentString = tileEntity.State.SpawnY.ToString();
        }

        public override void OnInitialize()
        {
            Left.Set(300, 0);
            Top.Set(300, 0);

            BasicUIBuilder builder = new();
            panel = builder.Container;
            panel.Width.Set(500, 0);
            panel.Height.Set(300, 0);
            Append(panel);

            inputX = builder.AddTextbox("Placeholder X", width: 140);
            ValidateIntTextbox(inputX, x => _tileEntity.State.SpawnX = x);
            inputY = builder.AddTextbox("Placeholder Y", width: 140);
            ValidateIntTextbox(inputY, y => _tileEntity.State.SpawnY = y);
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
            ValidateNpcTextbox(inputNpc, npc => _tileEntity.State.NpcName = npc);
            builder.NextRow();
            //inputNpc.OnTextChange += (s, e) => { }

            var spawnButton = builder.AddButton("Spawn");
            builder.AddCloseButton();

            spawnButton.OnLeftClick += (_, _) =>
            {
                _tileEntity?.Spawn();
            };
        }

        private void ValidateNpcTextbox(UIPanelTextbox textbox, Action<string> handler)
        {
            textbox.TextField.OnTextChange += (_, _) =>
            {
                if(NPCID.Search.TryGetId(textbox.TextField.CurrentString, out int _))
                {
                    textbox.TextField.TextColor = Color.White;
                    handler(textbox.TextField.CurrentString);
                }
                else
                {
                    textbox.TextField.TextColor = Color.Red;
                }
            };
        }
        private void ValidateIntTextbox(UIPanelTextbox textbox, Action<int> valueHandler)
        {
            textbox.TextField.OnTextChange += (_, _) =>
            {
                if(int.TryParse(textbox.TextField.CurrentString, out int value))
                {
                    textbox.TextField.TextColor = Color.White;
                    valueHandler(value);
                }
                else
                {
                    textbox.TextField.TextColor = Color.Red;
                }
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
