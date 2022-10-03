using BepInEx;
using HarmonyLib;
using Photon.Pun;

namespace UtilityKare
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class UtilityKarePlugin : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource Log { get; private set; }
        private void Awake()
        {
            Log = Logger;
            new Harmony(PluginInfo.PLUGIN_GUID).PatchAll();
            Logger.LogInfo($"Plugin is loaded and ready to assist!");
        }
    }

    [HarmonyPatch]
    internal static class Patchers
    {
        [HarmonyPatch(typeof(ReagentDatabase), nameof(ReagentDatabase.Awake))]
        [HarmonyPrefix]
        private static void ReagentDatabase_Awake_Prefix(ref ReagentDatabase __instance)
        {
            UtilityKarePlugin.Log.LogInfo("Patching in custom reagents...");
            ReagentManager.OnReagentDatabaseAwake(ref __instance);
            UtilityKarePlugin.Log.LogInfo("Finished patching in custom reagents.");
        }

        [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.GameVersion), MethodType.Setter)]
        [HarmonyPrefix]
        private static void PhotonNetwork_GameVersion_Set_Prefix(ref string value)
        {
            UtilityKarePlugin.Log.LogInfo("Modifying version...");
            VersionController.OnVersionSet(ref value);
            UtilityKarePlugin.Log.LogInfo("Finished modifying version.");
        }
    }
}