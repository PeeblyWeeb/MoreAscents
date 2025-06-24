using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using Logger = UnityEngine.Logger;

namespace MoreAscents
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            var ascentData = AscentData.Instance;
            List<AscentData.AscentInstanceData> ascents = ascentData.ascents;
            List<AscentData.AscentInstanceData> newAscents = new List<AscentData.AscentInstanceData>();

            newAscents.Add(ascents[0]);
            newAscents.Add(ascents[1]);
            newAscents.Add(ascents[2]);
            newAscents.Add(ascents[3]);
            newAscents.Add(ascents[4]);
            newAscents.Add(ascents[5]);
            newAscents.Add(ascents[6]);
            newAscents.Add(ascents[7]);
            newAscents.Add(ascents[8]);
            
            // custom ones
            AscentGimmickHandler.RegisterAscent<LuggageGimmick>(newAscents);
            
            ascentData.ascents = newAscents;

            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(BoardingPass), "UpdateAscent")]
    public static class boarding_initilize_patch
    {
        public static void Prefix(BoardingPass __instance)
        {
            FieldInfo info = __instance.GetType().GetField("maxAscent",BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(__instance, AscentData.Instance.ascents.Count - 2);
            Plugin.Logger.LogInfo($"Ascents capped {info.GetValue(__instance)}");
        }
    }
}
