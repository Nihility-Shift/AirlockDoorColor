using CG.Client.Ship;
using CG.Client.Ship.Views;
using CG.Ship.Hull;
using HarmonyLib;
using UnityEngine;
using VFX;

namespace AirlockDoorColor
{
    [HarmonyPatch(typeof(AirlockButtonPanel), "UpdateDiodes")]
    internal class AirlockButtonPanelPatch
    {
        //internal static Color openColor;
        private static Color closedColor = new Color(0, 6, 0, 1);
        private static Color unsafeColor = new Color(0, 0, 0, 1);
        static void Postfix(Airlock ____airlock, Diode ___diodeInnerDoor, Diode ___diodeOuterDoor)
        {
            for (int i = 0; i < ____airlock.airlockDoors.Count; i++)
            {
                AirlockDoor airlockDoor = ____airlock.airlockDoors[i];

                if (airlockDoor.airlockDoorType == AirlockDoorType.Inner)
                {
                    bool safe = !airlockDoor.IsLocked && (airlockDoor.IsSafe || !____airlock.IsSafetyEnabled);
                    MaterialUtils.UpdateRendererMaterials(___diodeInnerDoor._materialData, "_EmissiveColor", airlockDoor.IsOpen ? Configs.CurrentColor : safe ? closedColor : unsafeColor);
                }
                else
                {
                    bool safe = !airlockDoor.IsLocked && (airlockDoor.IsSafe || !____airlock.IsSafetyEnabled);
                    MaterialUtils.UpdateRendererMaterials(___diodeOuterDoor._materialData, "_EmissiveColor", airlockDoor.IsOpen ? Configs.CurrentColor : safe ? closedColor : unsafeColor);
                }
            }
        }
    }
}
