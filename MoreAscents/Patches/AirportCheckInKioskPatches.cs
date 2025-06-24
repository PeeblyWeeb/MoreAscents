using HarmonyLib;

namespace MoreAscents.Patches;

public class AirportCheckInKioskPatches
{
    [HarmonyPatch(typeof(AirportCheckInKiosk), nameof(AirportCheckInKiosk.LoadIslandMaster))]
    public static class LoadIslandMaster
    {
        [HarmonyPrefix]
        public static void Prefix(int ascent)
        {
            AscentData.AscentInstanceData data = GUIManager.instance.boardingPass.ascentData.ascents[ascent + 1];
            AscentGimmickHandler.MarkGimmickAsActive(data);
        }
    }
}