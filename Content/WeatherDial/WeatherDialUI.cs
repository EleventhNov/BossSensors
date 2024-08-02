#nullable disable
using System;
using BossSensors.UI;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Utilities.Terraria.Utilities;

namespace BossSensors.Content.WeatherDial
{
    internal class WeatherDialUI : UIState, IStateable<WeatherDialTileEntity>
    {
        private WeatherDialTileEntity _tileEntity;
        private BasicUIBuilder builder;
        private UIPanelTextbox windSpeed;
        private UIPanelTextbox rainSpeed;
        private UIOptionSelect windOptions;
        private UIOptionSelect rainOptions;

        private static readonly string[] weatherPatternOptions = 
            [
            "Do not modify",
            "Allow changing speed",
            "Freeze at this speed",
            ];

        public override void OnInitialize()
        {
            builder = new(this);

            windSpeed = AddMaybeFloatTextbox("Wind speed", target => _tileEntity.WindTarget = target, -0.8f, 0.8f);
            builder.NextRow();

            windOptions = AddWeatherPatternSelect(freeze => _tileEntity.FreezeWind = freeze);
            builder.NextRow();

            rainSpeed = AddMaybeFloatTextbox("Rain speed", target => _tileEntity.RainTarget = target, 0, 1);
            builder.NextRow();

            rainOptions = AddWeatherPatternSelect(freeze => _tileEntity.FreezeRain = freeze);
            builder.NextRow();

            builder.AddButton("Activate").OnLeftClick += (_, _) => _tileEntity.HitSignal();
            builder.AddCloseButton();

            builder.Finish();
        }

        public void SetState(WeatherDialTileEntity tileEntity)
        {
            _tileEntity = tileEntity;
            windSpeed.CurrentString = tileEntity.WindTarget?.ToString() ?? string.Empty;
            rainSpeed.CurrentString = tileEntity.RainTarget?.ToString() ?? string.Empty;
            windOptions.OptionIndex = (int)tileEntity.FreezeWind;
            rainOptions.OptionIndex = (int)tileEntity.FreezeRain;
        }

        private UIPanelTextbox AddMaybeFloatTextbox(string hintText, Action<float?> valueHandler, float minValue, float maxValue)
        {
            UIPanelTextbox textbox = builder?.AddTextbox(hintText);
            textbox.Width.Pixels = 250;
            TextboxValidation.ValidateTextFieldOptionalFloat(textbox.TextField, valueHandler, minValue, maxValue);
            return textbox;
        }

        private UIOptionSelect AddWeatherPatternSelect(Action<WeatherDialFreezingMode> setValue)
        {
            UIOptionSelect options = new(weatherPatternOptions);
            options.Width.Pixels = 250;
            options.Height.Pixels = 40;
            builder?.AddToCurrentRow(options);
            options.OnOptionChange += (_, value) => setValue((WeatherDialFreezingMode)value);
            return options;
        }
    }
}
