using System.Collections.Generic;

namespace UtilityKare
{
    /// <summary>
    /// This class modifies the version to require certain mods in multiplayer.<br/>
    /// See <see cref="AddModGUID(string)"/> for more information.
    /// </summary>
    public static class VersionController
    {
        /// <summary>
        /// List of Mod GUIDs
        /// </summary>
        private static List<string> ModGUIDs = new List<string>();

        /// <summary>
        /// This will add the GUID provided to the required mod for multiplayer,<br/>
        /// effectively seperating non-modded with modded rooms.
        /// </summary>
        /// <param name="GUID">Pass in your mod GUID here.</param>
        public static void AddModGUID(string GUID)
        {
            ModGUIDs.Add(GUID);
        }

        /// <summary>
        /// Will run whenever the version is set.
        /// </summary>
        /// <param name="value"></param>
        internal static void OnVersionSet(ref string value)
        {
            // We sort the list, so ideally there's no difference in version even with mods loaded at different times.
            ModGUIDs.Sort();
            foreach (string GUID in ModGUIDs)
            {
                UtilityKarePlugin.Log.LogInfo($"Adding Mod-Requirement for multiplayer for mod: \"{GUID}\".");
                value = $"[{GUID}]{value}";
            }
        }
    }
}
