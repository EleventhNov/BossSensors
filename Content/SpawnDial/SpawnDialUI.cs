﻿#nullable disable
using System;
using BossSensors.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors.Content.SpawnDial
{
    internal class SpawnDialUI : UIState, IStateable<SpawnDialTileEntity>
    {
        private SpawnDialTileEntity _tileEntity;
        private UIPanelTextbox inputX;
        private UIPanelTextbox inputY;

        public override void OnInitialize()
        {
            BasicUIBuilder builder = new(this);
            
            inputX = builder.AddTextbox("Placeholder X", width: 140);
            TextboxValidation.ValidateTextFieldInt(inputX.TextField, x => _tileEntity.StoredSpawnTileX = x);
            inputY = builder.AddTextbox("Placeholder Y", width: 140);
            TextboxValidation.ValidateTextFieldInt(inputY.TextField, y => _tileEntity.StoredSpawnTileY = y);

            var selectPositionButton = builder.AddButton("Select Position", width: 160);
            selectPositionButton.OnLeftClick += (s, e) =>
            {
                ModContent.GetInstance<WorldPositionSelectSystem>().RequestPosition(position =>
                {
                    var tilePosition = position.ToTileCoordinates();
                    inputX.CurrentString = tilePosition.X.ToString();
                    inputY.CurrentString = tilePosition.Y.ToString();
                });
            };
            builder.NextRow();

            builder.AddButton("Activate").OnLeftClick += (_, _) => _tileEntity.HitSignal();
            builder.AddCloseButton();

            builder.Finish();
        }

        public void SetState(SpawnDialTileEntity tileEntity)
        {
            _tileEntity = tileEntity;
            inputX.CurrentString = _tileEntity.StoredSpawnTileX.ToString();
            inputY.CurrentString = _tileEntity.StoredSpawnTileY.ToString();
        }
    }
}
