#nullable disable
using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BossSensors.UI
{
    internal class UISlider : UIElement
    {
        private static float sliderLineThickness = 20;
        private static float sliderWidth = 20;
        
        private UIPanel slider;
        private bool movingSlider;
        public event EventHandler<float> OnChangeValue;
        private float _currentValue;

        public float Value
        {
            get => _currentValue;
            set {
                value = MathHelper.Clamp(value, 0, 1);
                if (value != _currentValue)
                {
                    _currentValue = value;
                    OnChangeValue.Invoke(this, value);
                }
            }
        }

        public override void OnInitialize()
        {
            UIPanel sliderLine = new();
            sliderLine.Width.Percent = 1;
            sliderLine.Height.Pixels = sliderLineThickness;
            sliderLine.VAlign = 0.5f;
            Append(sliderLine);

            slider = new();
            slider.Width.Pixels = sliderWidth;
            slider.Height.Percent = 1;
            slider.VAlign = 0.5f;
            Append(slider);

            OnLeftMouseDown += (_, _) => movingSlider = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.mouseLeftRelease) movingSlider = false;
            if (movingSlider)
            {
                var dims = GetDimensions();
                var value = (Main.MouseScreen.X - dims.X) / dims.Width;
                Value = value;
            }
            slider.Left.Pixels = Value * GetDimensions().Width - slider.GetDimensions().Width / 2;
        }
    }
}
