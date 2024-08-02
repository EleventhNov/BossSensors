using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace BossSensors.UI
{
    internal static class TextboxValidation
    {
        public static void ValidateTextFieldInt(UIFocusInputTextField textField, Action<int> valueHandler)
        {
            textField.OnTextChange += (_, _) =>
            {
                if (int.TryParse(textField.CurrentString, out int value))
                {
                    textField.TextColor = Color.White;
                    valueHandler(value);
                }
                else
                {
                    textField.TextColor = Color.Red;
                }
            };
        }

        public static void ValidateTextFieldOptionalFloat(UIFocusInputTextField textField, Action<float?> valueHandler, float minValue, float maxValue)
        {
            textField.OnTextChange += (_, _) =>
            {
                if (textField.CurrentString == string.Empty)
                {
                    valueHandler(null);
                    return;
                }

                if (float.TryParse(textField.CurrentString, out float value))
                {
                    if (value >= minValue && value <= maxValue)
                    {
                        textField.TextColor = Color.White;
                        valueHandler(value);
                        return;
                    }
                }

                textField.TextColor = Color.Red;
            };
        }

        public static void ValidateTextFieldNpc(UIFocusInputTextField textField, Action<string> valueHandler)
        {
            textField.OnTextChange += (_, _) =>
            {
                if (NPCID.Search.TryGetId(textField.CurrentString, out int _))
                {
                    textField.TextColor = Color.White;
                    valueHandler(textField.CurrentString);
                }
                else
                {
                    textField.TextColor = Color.Red;
                }
            };
        }

    }
}
