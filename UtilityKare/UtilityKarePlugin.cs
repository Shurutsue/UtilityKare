using BepInEx;
using HarmonyLib;

namespace UtilityKare
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class UtilityKarePlugin : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource Log { get; private set; }
        private void Awake()
        {
            Log = Logger;
            new Harmony(PluginInfo.PLUGIN_GUID).PatchAll();
            Logger.LogInfo($"Plugin is loaded and ready to assist!");
        }
    }

    [HarmonyPatch(typeof(ReagentDatabase), nameof(ReagentDatabase.Awake))]
    public static class Patchers
    {
        [HarmonyPrefix]
        public static void ReagentDatabase_Awake_Prefix(ref ReagentDatabase __instance)
        {
            UtilityKarePlugin.Log.LogInfo("Patching in custom reagents...");
            ReagentManager.OnReagentDatabaseAwake(ref __instance);
            UtilityKarePlugin.Log.LogInfo("Finished patching in custom reagents.");
        }
    }
}