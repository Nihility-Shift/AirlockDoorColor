using UnityEngine;
using VoidManager.CustomGUI;

namespace AirlockDoorColor
{
    class ConfigGUI : ModSettingsMenu
    {
        public static void ColorPicker(Rect rect, string Name, ref Color Color)
        {
            GUILayout.BeginArea(rect, "", "Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{Name}");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"R", GUILayout.Width(10));
            Color.r = GUILayout.HorizontalSlider(Color.r, 0f, 20f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"G", GUILayout.Width(10));
            Color.g = GUILayout.HorizontalSlider(Color.g, 0f, 20f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"B", GUILayout.Width(10));
            Color.b = GUILayout.HorizontalSlider(Color.b, 0f, 20f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"A", GUILayout.Width(10));
            Color.a = GUILayout.HorizontalSlider(Color.a, 0f, 1f);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box", new GUILayoutOption[] { GUILayout.Width(44), GUILayout.Height(44) });
            GUI.color = Color;
            GUILayout.Label(new Texture2D(60, 40));
            GUI.color = UnityEngine.Color.white;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Label($"{Color.r},{Color.g},{Color.b},{Color.a}");
            GUILayout.EndArea();
        }

        public override void Draw()
        {
            ColorPicker(new Rect(8, 58, 480, 160), "Open Door Color", ref BepinPlugin.Bindings.CurrentColor);
            if(GUILayout.Button("Reset Color"))
            {
                BepinPlugin.Bindings.CurrentColor = BepinPlugin.Bindings.DefaultColor;
            }
            BepinPlugin.Bindings.UpdateColorConfig(); //Inefficient, but only active while settings menu is up so should be fine.
        }

        public override string Name()
        {
            return MyPluginInfo.PLUGIN_NAME;
        }
    }
}
