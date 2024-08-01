using System.Collections.Generic;
using BossSensors.Content.AliveSensor;
using BossSensors.Content.DeathSensor;
using BossSensors.Content.Spawner;
using BossSensors.Content.WeatherDial;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors
{
    [Autoload(Side =ModSide.Client)]
    internal class BossSensorsUI : ModSystem
    {
        internal UserInterface? UserInterface;
        internal SpawnerUI? SpawnerUI;
        internal AliveSensorUI? AliveSensorUI;
        internal DeathSensorUI? DeathSensorUI;
        internal WeatherDialUI? WeatherDialUI;

        public void ShowSpawnerUI(SpawnerTileEntity spawner)
        {
            Load();
            SpawnerUI?.SetTileEntity(spawner);
            UserInterface?.SetState(SpawnerUI);
        }

        public void ShowAliveSensorUI(AliveSensorTileEntity aliveSensor)
        {
            Load();
            bool close = UserInterface?.CurrentState == AliveSensorUI;
            if(AliveSensorUI?.SetTileEntity(aliveSensor) == true) close = false;
            UserInterface?.SetState(close ? null : AliveSensorUI);
        }

        public void ShowDeathSensorUI(DeathSensorTileEntity deathSensor)
        {
            Load();
            bool close = UserInterface?.CurrentState == DeathSensorUI;
            if (DeathSensorUI?.SetTileEntity(deathSensor) == true) close = false;
            UserInterface?.SetState(close ? null : DeathSensorUI);
        }

        public void ShowWeatherDialUI(WeatherDialTileEntity weatherDial)
        {
            Load();
            bool close = UserInterface?.CurrentState == WeatherDialUI;
            if (WeatherDialUI?.SetTileEntity(weatherDial) == true) close = false;
            UserInterface?.SetState(close ? null : WeatherDialUI);
        }

        public void HideUI()
        {
            UserInterface?.SetState(null);
        }

        public override void Load()
        {
            UserInterface = new();
            SpawnerUI = new SpawnerUI();
            SpawnerUI.Activate();
            AliveSensorUI = new AliveSensorUI();
            AliveSensorUI.Activate();
            DeathSensorUI = new DeathSensorUI();
            DeathSensorUI.Activate();
            WeatherDialUI = new WeatherDialUI();
            WeatherDialUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (UserInterface?.CurrentState != null)
            {
                UserInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex == -1) return;
            
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "BossSensors: UI",
                delegate
                {
                    if (UserInterface?.CurrentState != null)
                    {
                        UserInterface.Draw(Main.spriteBatch, new GameTime());
                    }
                    return true;
                },
                InterfaceScaleType.UI));
        }
    }
}
