﻿using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents.Patches;

public class CharacterPatches
{
    [HarmonyPatch(typeof(CharacterMovement), "CheckFallDamage")]
    public static class CheckFallDamage {
        public static bool InFallDamage = false;
        
        [HarmonyPrefix]
        public static void Prefix(CharacterMovement __instance) {
            InFallDamage = true;
        }
        
        [HarmonyPostfix]
        public static void Postfix(CharacterMovement __instance) {
            InFallDamage = false;
        }
    }
    
    [HarmonyPatch(typeof(CharacterGrabbing), "Reach")]
    public static class Reach {
        public static Character character;
        
        [HarmonyPrefix]
        public static void Prefix(CharacterGrabbing __instance) {
            character = __instance.GetComponent<Character>();

        }
        
        [HarmonyPostfix]
        public static void Postfix() {
            character = null;
        }
    }
    
    [HarmonyPatch(typeof(CharacterAfflictions), "UpdateNormalStatuses")]
    public static class UpdateNormalStatuses {
        public static bool InUpdateNormalStatuses = false;
        
        [HarmonyPrefix]
        public static void Prefix() {
            InUpdateNormalStatuses = true;
        }
        
        [HarmonyPostfix]
        public static void Postfix() {
            InUpdateNormalStatuses = false;
        }
    }

    [HarmonyPatch(typeof(CharacterAfflictions), "AddStatus")]
    public static class AddStatus {
        [HarmonyPrefix]
        public static void Prefix(CharacterAfflictions __instance, ref float amount, CharacterAfflictions.STATUSTYPE statusType) {
            float totalMultiplier = 1f;
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                totalMultiplier += gimmick.AfflictionMultiplier(__instance,statusType,amount);
            }
            amount *= totalMultiplier;
        }
        
        [HarmonyPostfix]
        public static void Postfix(CharacterAfflictions __instance, bool __result, float amount, CharacterAfflictions.STATUSTYPE statusType) {
            if (!__result)
                return;
            
            if (CheckFallDamage.InFallDamage && statusType == CharacterAfflictions.STATUSTYPE.Injury) {
                foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                    if (!gimmick.active)
                        continue;
                    gimmick.OnCharacterFall(__instance.character);
                }
            }
        }
    }
}