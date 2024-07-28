using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors.UI
{
    internal class BasicUIBuilder
    {
        public UIPanel Container = new();
        public float RowStart = 0;
        public float RowElementSpacing = 15;
        public float ColumnSize = 35;
        public float ColumnSpacing = 15;
        public Vector2 Cursor = new(0, 0);

        public void NextRow()
        {
            Cursor.X = RowStart;
            Cursor.Y += ColumnSize + ColumnSpacing;
        }

        public void AddToCurrentRow(UIElement element)
        {
            element.Left.Set(Cursor.X, 0);
            element.Top.Set(Cursor.Y, 0);
            Cursor.X += element.Width.Pixels + RowElementSpacing;
            Container.Append(element);
        }

        public void AddCloseButton()
        {
            var button = AddButton("Close");
            button.BackgroundColor = Color.PaleVioletRed;
            button.OnLeftClick += (_, _) => ModContent.GetInstance<BossSensorsUI>().HideUI();
        }

        public UIPanelTextbox AddTextbox(string hintText, int width = 120)
        {
            UIPanelTextbox textbox = new(hintText);
            textbox.Width.Set(width, 0);
            textbox.Height.Set(40, 0);
            AddToCurrentRow(textbox);
            textbox.Activate();
            return textbox;
        }

        public UIPanel AddButton(string buttonText, int width = 80)
        {
            UIPanel button = new();
            button.Width.Set(width, 0);
            button.Height.Set(40, 0);

            UIText text = new(buttonText);
            button.Append(text);
            AddToCurrentRow(button);

            return button;
        }

        public UIPanelTextbox AddNpcSelect()
        {
            var inputNpc = AddTextbox("NPC Name", width: 300);
            var selectNpcButton = AddButton("Select Nearest", width: 150);
            selectNpcButton.OnLeftClick += (s, e) =>
            {
                Vector2 playerPosition = Main.LocalPlayer.Center;
                if (NPCUtils.GetNearestNpc(playerPosition) is not NPC npc) return;
                inputNpc.CurrentString = NPCID.Search.GetName(npc.type);
            };
            return inputNpc;
        }
    }
}
