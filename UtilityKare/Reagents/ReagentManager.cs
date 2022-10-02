using System.Collections.Generic;

namespace UtilityKare
{
    public static class ReagentManager
    {
        /// <summary>
        /// Contains all custom Reagents.
        /// </summary>
        private static readonly List<CustomReagent> CustomReagents = new();

        /// <summary>
        /// Contains all custom ReagentReactions.
        /// </summary>
        private static readonly List<CustomReagentReaction> CustomReagentReactions = new();

        /// <summary>
        /// Adds your own custom reagent reaction to the list, to conver them to ScriptableReagentReactions.<br/>
        /// See <see cref="CustomReagentReaction"/> for more information.
        /// </summary>
        /// <param name="reaction"></param>
        public static void AddReagentReaction(CustomReagentReaction reaction)
        {
            CustomReagentReactions.Add(reaction);
        }

        /// <summary>
        /// Add your own custom reagent to the list, to convert them to ScriptableReagents.<br/>
        /// See <see cref="CustomReagent"/> for more information.
        /// </summary>
        /// <param name="reagent"></param>
        public static void AddReagent(CustomReagent reagent)
        {
            CustomReagents.Add(reagent);
        }

        /// <summary>
        /// Not necessary to call this. It will add the reagents to the ReagentDatabase on each Awake of it.
        /// </summary>
        /// <param name="__instance"></param>
        public static void OnReagentDatabaseAwake(ref ReagentDatabase __instance)
        {
            // Adding custom reagents.
            foreach(CustomReagent customReagent in CustomReagents)
            {
                int foundReagentIndex = __instance.reagents.FindIndex(x => x.name == customReagent.UniqueName);
                if (foundReagentIndex == -1)
                {
                    // Not found so we can add as new with default data (if certain fields are not set)
                    UtilityKarePlugin.Log.LogInfo($"Adding new ScriptableReagent: \"{customReagent.UniqueName}\"");
                    ScriptableReagent scriptableReagent = customReagent.GetScriptableReagent();
                    __instance.reagents.Add(scriptableReagent);
                }
                else
                {
                    // Found, so we will replace it with a copy containing our own data (of those which're set)
                    UtilityKarePlugin.Log.LogInfo($"Found existing ScriptableReagent called \"{customReagent.UniqueName}\". Will replace with modified variant!");
                    ScriptableReagent fsreagent = __instance.reagents[foundReagentIndex];
                    ScriptableReagent scriptableReagent = customReagent.GetScriptableReagent(fsreagent);
                    __instance.reagents[foundReagentIndex] = scriptableReagent;
                }
            }

            // Adding custom reagent reactions.
            foreach(CustomReagentReaction reaction in CustomReagentReactions)
            {
                ScriptableReagentReaction scriptableReaction = reaction.GetScriptableReagentReaction(__instance);
                if (scriptableReaction == null)
                {
                    UtilityKarePlugin.Log.LogWarning("Skipping adding a reaction due to an error getting a working ScriptableReagentReaction.");
                    continue;

                }
                // TODO: Check if a reaction with this combination already exists, and if so, throw an error or overwrite?
                __instance.reactions.Add(scriptableReaction);
            }
        }
    }
}
