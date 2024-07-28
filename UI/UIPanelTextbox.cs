using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace BossSensors.UI
{
    internal class UIPanelTextbox(string hintText) : UIPanel
    {
        public UIFocusInputTextField TextField;
        public string CurrentString
        {
            get => TextField.CurrentString;
            set => TextField.CurrentString = value;
        }

        public override void OnInitialize()
        {
            UIPanel textbox = this;
            TextField = new(hintText);
            textbox.Append(TextField);
            textbox.OnLeftClick += (a, b) => TextField.LeftClick(a);
        }
    }
}
