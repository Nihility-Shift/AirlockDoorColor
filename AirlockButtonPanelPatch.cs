using CG.Client.Ship;
using CG.Client.Ship.Views;
using CG.Ship.Hull;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using VFX;

namespace AirlockDoorColor
{
    [HarmonyPatch(typeof(AirlockButtonPanel), "UpdateDiodes")]
    internal class AirlockButtonPanelPatch
    {
        private static FieldInfo diodeMaterial = AccessTools.Field(typeof(Diode), "_materialData");
        private static FieldInfo diodeproperty = AccessTools.Field(typeof(Diode), "_propertyBlock");
        private static Color openColor = new Color(5.2f, 5.2f, 6, 1);
        private static Color closedColor = new Color(0, 6, 0, 1);
        private static Color unsafeColor = new Color(0, 0, 0, 1);
        static void Postfix(AirlockButtonPanel __instance, Airlock ____airlock, Diode ___diodeInnerDoor,
            Diode ___diodeOuterDoor, Diode ___safetyOnDiode)
        {
            for (int i = 0; i < ____airlock.airlockDoors.Count; i++)
            {
                AirlockDoor airlockDoor = ____airlock.airlockDoors[i];

                if (airlockDoor.airlockDoorType == AirlockDoorType.Inner)
                {
                    bool safe = !airlockDoor.IsLocked && (airlockDoor.IsSafe || !____airlock.IsSafetyEnabled);
                    List<RendererSourceData> materialData = (List<RendererSourceData>)diodeMaterial.GetValue(___diodeInnerDoor);
                    MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)diodeproperty.GetValue(___diodeInnerDoor) ?? new MaterialPropertyBlock();
                    MaterialUtils.UpdateRendererMaterials(materialData, "_EmissiveColor", airlockDoor.IsOpen ? openColor : safe ? closedColor : unsafeColor, propertyBlock);
                }
                else
                {
                    bool safe = !airlockDoor.IsLocked && (airlockDoor.IsSafe || !____airlock.IsSafetyEnabled);
                    List<RendererSourceData> materialData = (List<RendererSourceData>)diodeMaterial.GetValue(___diodeOuterDoor);
                    MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)diodeproperty.GetValue(___diodeOuterDoor) ?? new MaterialPropertyBlock();
                    MaterialUtils.UpdateRendererMaterials(materialData, "_EmissiveColor", airlockDoor.IsOpen ? openColor : safe ? closedColor : unsafeColor, propertyBlock);
                }
            }
        }
    }
}
