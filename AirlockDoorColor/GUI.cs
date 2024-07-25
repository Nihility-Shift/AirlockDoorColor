using UnityEngine;
using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace AirlockDoorColor
{
    class ConfigGUI : ModSettingsMenu
    {
        public override string Name() => "Airlock Door Color";

        public override void Draw()
        {
            if (GUITools.DrawColorPicker(new Rect(8, 58, 480, 160), "Open Door Color", ref Configs.CurrentColor, Configs.DefaultColor, false))
            {
                Configs.UpdateColorConfig();
            }
        }
    }
}
