using UnityEngine;

namespace UtilityKare
{
    /// <summary>
    /// CustomReactant in exchange of the actual one.
    /// Required since the actual ScriptableReagents might not yet have been created/added.
    /// </summary>
    public class CustomReactant
    {
        /// <summary>
        /// The unique identifying name of the ScriptableReagent.
        /// </summary>
        public string ReagentUniqueName;
        /// <summary>
        /// The coefficient to use, which either is required amount, or produced amount. (clamped between 0.01f and 10.0f)
        /// </summary>
        public float Coefficient;
        /// <summary>
        /// Creates a new custom reactant.
        /// </summary>
        /// <param name="reagentUniqueName">The identifying name for the ScriptableReagent.</param>
        /// <param name="coefficient">The coefficient, meaning the amount required or produced (clamped between 0.01f and 10.0f)</param>
        public CustomReactant(string reagentUniqueName, float coefficient)
        {
            this.ReagentUniqueName = reagentUniqueName;
            this.Coefficient = Mathf.Clamp(coefficient, 0.01f, 10.0f);
        }

        /// <summary>
        /// Returns an actual Reactant for use.
        /// </summary>
        /// <param name="__instance">The current instance of ReagentDatabase to reference and check for ScriptableReagents by name.</param>
        /// <returns></returns>
        public ScriptableReagentReaction.Reactant GetReactant(ReagentDatabase __instance)
        {
            ScriptableReagent reagent = __instance.reagents.Find(x => x.name == ReagentUniqueName);
            if (reagent == null)
            {
                UtilityKarePlugin.Log.LogError($"Could not find a ScriptableReagent with name \"{ReagentUniqueName}\"");
                return null;
            }
            ScriptableReagentReaction.Reactant Reactant = new() { coefficient = Coefficient, reactant = reagent };
            return Reactant;
        }
    }
}
