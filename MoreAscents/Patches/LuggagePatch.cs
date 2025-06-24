using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents.Patches;

public class LuggagePatches
{
    [HarmonyPatch(typeof(Spawner), "SpawnItems")]
    public static class GetSpawnSpots
    {
        [HarmonyPrefix]
        public static void Prefix(Spawner __instance, ref List<Transform> spawnSpots)
        {

            
            LuggageGimmick gimmick = AscentGimmickHandler.GetGimmick<LuggageGimmick>();
            
        }
    }
}