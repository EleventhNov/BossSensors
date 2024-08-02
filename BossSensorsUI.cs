#nullable disable
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BossSensors
{
    [Autoload(Side =ModSide.Client)]
    internal class BossSensorsUI : ModSystem
    {
        internal UserInterface UserInterface;

        internal UIState currentState;
        internal object currentStateObject;

        public static void ShowTileEntityUI<UI, TE>(int i, int j)
            where UI : UIState, IStateable<TE>, new()
            where TE : ModTileEntity
        {
            TE tileEntity = TileUtils.GetTileEntity<TE>(i, j);
            ModContent.GetInstance<BossSensorsUI>().ShowUI<UI, TE>(tileEntity);
        }

        public void ShowUI<T, S>(S stateObject) where T: UIState, IStateable<S>, new()
        {
            bool close = currentState?.GetType() == typeof(T);
            if (currentStateObject != stateObject as object) close = false;

            if(close)
            {
                HideUI();
            }
            else
            {
                currentState = new T();
                currentState.Activate();
                currentStateObject = stateObject;
                ((IStateable<S>)currentState).SetState(stateObject);
                UserInterface?.SetState(currentState);
            }
        }

        public void HideUI()
        {
            currentState = null;
            currentStateObject = null;
            UserInterface?.SetState(null);
        }

        public override void Load()
        {
            UserInterface = new();
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
