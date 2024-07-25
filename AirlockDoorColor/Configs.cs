using BepInEx.Configuration;
using UnityEngine;

namespace AirlockDoorColor
{
    internal class Configs
    {
        internal static Color DefaultColor = new Color(5.2f, 5.2f, 6, 1);
        internal static Color CurrentColor;
        internal static ConfigEntry<float> OpenDoorColorR;
        internal static ConfigEntry<float> OpenDoorColorG;
        internal static ConfigEntry<float> OpenDoorColorB;
        internal static ConfigEntry<float> OpenDoorColorA;

        internal static void UpdateColorConfig()
        {
            OpenDoorColorR.Value = CurrentColor.r;
            OpenDoorColorG.Value = CurrentColor.g;
            OpenDoorColorB.Value = CurrentColor.b;
            OpenDoorColorA.Value = CurrentColor.a;
        }

        internal static void Load(BepinPlugin plugin)
        {
            OpenDoorColorR = plugin.Config.Bind("DoorColor", "DoorColorR", 5.2f);
            OpenDoorColorG = plugin.Config.Bind("DoorColor", "DoorColorG", 5.2f);
            OpenDoorColorB = plugin.Config.Bind("DoorColor", "DoorColorB", 6f);
            OpenDoorColorA = plugin.Config.Bind("DoorColor", "DoorColorA", 1f);
            CurrentColor = new Color(OpenDoorColorR.Value, OpenDoorColorG.Value, OpenDoorColorB.Value, OpenDoorColorA.Value);
        }
    }
}
