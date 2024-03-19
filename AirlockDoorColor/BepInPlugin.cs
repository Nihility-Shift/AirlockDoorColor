using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace AirlockDoorColor
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency("VoidManager")]
    public class BepinPlugin : BaseUnityPlugin
    {
        //internal static ManualLogSource Log;
        private void Awake()
        {
            //Log = Logger;
            Harmony.CreateAndPatchAll(typeof(AirlockButtonPanelPatch), MyPluginInfo.PLUGIN_GUID);
            Bindings.OpenDoorColorR = Config.Bind("DoorColor", "DoorColorR", 5.2f);
            Bindings.OpenDoorColorG = Config.Bind("DoorColor", "DoorColorG", 5.2f);
            Bindings.OpenDoorColorB = Config.Bind("DoorColor", "DoorColorB", 6f);
            Bindings.OpenDoorColorA = Config.Bind("DoorColor", "DoorColorA", 1f);
            Bindings.CurrentColor = new Color(Bindings.OpenDoorColorR.Value, Bindings.OpenDoorColorG.Value, Bindings.OpenDoorColorB.Value, Bindings.OpenDoorColorA.Value);
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
        
        internal static class Bindings
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
        }
    }
}