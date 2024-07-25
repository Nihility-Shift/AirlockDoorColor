using CG.Client.Ship;
using CG.Client.Ship.Views;
using CG.Ship.Hull;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VFX;

namespace AirlockDoorColor
{
    [HarmonyPatch(typeof(AirlockButtonPanel), "UpdateDiodes")]
    internal class AirlockButtonPanelPatch
    {
        private static readonly FieldInfo diodeMaterial = AccessTools.Field(typeof(Diode), "_materialData");
        private static readonly FieldInfo diodeproperty = AccessTools.Field(typeof(Diode), "_propertyBlock");
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
                    List<RendererSourceData> materialData = (List<RendererSourceData>)diodeMaterial.GetValue(___diodeInnerDoor);
                    MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)diodeproperty.GetValue(___diodeInnerDoor) ?? new MaterialPropertyBlock();
                    MaterialUtils.UpdateRendererMaterials(materialData, "_EmissiveColor", airlockDoor.IsOpen ? Configs.CurrentColor : safe ? closedColor : unsafeColor, propertyBlock);
                }
                else
                {
                    bool safe = !airlockDoor.IsLocked && (airlockDoor.IsSafe || !____airlock.IsSafetyEnabled);
                    List<RendererSourceData> materialData = (List<RendererSourceData>)diodeMaterial.GetValue(___diodeOuterDoor);
                    MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)diodeproperty.GetValue(___diodeOuterDoor) ?? new MaterialPropertyBlock();
                    MaterialUtils.UpdateRendererMaterials(materialData, "_EmissiveColor", airlockDoor.IsOpen ? Configs.CurrentColor : safe ? closedColor : unsafeColor, propertyBlock);
                }
            }
        }
    }
}
