#nullable disable
using System;
using Terraria.GameContent.UI.Elements;

namespace BossSensors.UI
{
    internal class UIOptionSelect(string[] options) : UIPanel
    {
        public event EventHandler<int> OnOptionChange;
        private int optionIndex;
        private UIText optionLabel;

        public int OptionIndex
        {
            get => optionIndex;
            set {
                optionIndex = value;
                UpdateOptionLabel();
                OnOptionChange.Invoke(this, optionIndex);
            }
        }

        public override void OnInitialize()
        {
            optionLabel = new(string.Empty)
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            Append(optionLabel);

            UpdateOptionLabel();
            OnLeftClick += (_, _) => ChangeOption();
        }

        private void ChangeOption()
        {
            OptionIndex = (OptionIndex + 1) % options.Length;
        }

        private void UpdateOptionLabel()
        {
            optionLabel.SetText(options[optionIndex]);
        }
    }
}
