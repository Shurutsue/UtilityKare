using System.Collections.Generic;
using HarmonyLib;

namespace UtilityKare
{
    public class CustomReagentReaction
    {
        /// <summary>
        /// The Reactions that will occurs (if used just for combining fluids, it's not needed).
        /// </summary>
        public List<ReagentReaction> Reactions;
        /// <summary>
        /// The Reactants (reagents required) for this combination.
        /// </summary>
        public List<CustomReactant> Reactants;
        /// <summary>
        /// The Products (reagents produced) by this combination.
        /// </summary>
        public List<CustomReactant> Products;

        /// <summary>
        /// Gets an actual ScriptableReagentReaction for use in the game.
        /// </summary>
        /// <param name="__instance">The current instance of ReagentDatabase, required since some Reagents might not yet have been created/added</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>NULL</term> <description>If a certain reagent wasn't found but is required, it will return null.</description>
        /// </item>
        /// <item>
        /// <term>ScriptableReagentReaction</term> <description>If no issues were found, a ScriptableReagentReaction will be returned.</description>
        /// </item>
        /// </list>
        /// </returns>
        public ScriptableReagentReaction GetScriptableReagentReaction(ReagentDatabase __instance)
        {
            ScriptableReagentReaction scriptableReagentReaction = ScriptableReagentReaction.CreateInstance<ScriptableReagentReaction>();

            // Fill in the reactions
            ReagentReaction[] reactions = new ReagentReaction[Reactions.Count];
            for (int i = 0; i < Reactions.Count; i++)
                reactions[i] = Reactions[i];
            Traverse.Create(scriptableReagentReaction).Field<ReagentReaction[]>("reactions").Value = reactions;


            // Fill in the reactants
            scriptableReagentReaction.reactants = new ScriptableReagentReaction.Reactant[Reactants.Count];
            for (int i = 0; i < Reactants.Count; i++)
            {
                ScriptableReagentReaction.Reactant reactant = Reactants[i].GetReactant(__instance);
                if (reactant == null) { UtilityKarePlugin.Log.LogError($"CustomReagentReaction error: Reactant Reagent with name \"{Reactants[i].ReagentUniqueName}\" could not be found!"); return null; }
                scriptableReagentReaction.reactants[i] = reactant;
            }
            
            // Fill in the products
            scriptableReagentReaction.products = new ScriptableReagentReaction.Reactant[Products.Count];
            for (int i = 0; i < Products.Count; i++)
            {
                ScriptableReagentReaction.Reactant reactant = Products[i].GetReactant(__instance);
                if (reactant == null) { UtilityKarePlugin.Log.LogError($"CustomReagentReaction error: Product Reagent with name \"{Products[i].ReagentUniqueName}\" could not be found!"); return null; }
                scriptableReagentReaction.products[i] = reactant;
            }

            return scriptableReagentReaction;
        }

        /// <summary>
        /// Add a reaction to it (ex. explosion)
        /// </summary>
        /// <param name="reagentReaction">A ReagentReaction to add.</param>
        public void AddReaction(ReagentReaction reagentReaction)
        {
            this.Reactions.Add(reagentReaction);
        }

        /// <summary>
        /// Add a reactant to it (required reagent). <br/>
        /// See <see cref="CustomReactant"/> for more information.
        /// </summary>
        /// <param name="customReactant">The CustomReactant acting as reactant.</param>
        public void AddReactant(CustomReactant customReactant)
        {
            this.Reactants.Add(customReactant);
        }

        /// <summary>
        /// Create and add a new reactent to it (a required reagent). <br/>
        /// See <see cref="CustomReactant"/> for more information.
        /// </summary>
        /// <param name="reagentUniqueName">The unique Name of the reagent.</param>
        /// <param name="coefficient">the coefficient to use (required amount of the fluid).</param>
        public void AddReactant(string reagentUniqueName, float coefficient)
        {
            this.Reactants.Add(new(reagentUniqueName, coefficient));
        }

        /// <summary>
        /// Add a reactant to it (produced reagent). <br/>
        /// See <see cref="CustomReactant"/> for more information.
        /// </summary>
        /// <param name="customReactant">The CustomReactant acting as product.</param>
        public void AddProduct(CustomReactant customReactant)
        {
            this.Products.Add(customReactant);
        }

        /// <summary>
        /// Create and add a new reactant to it (produced reagent). <br/>
        /// See <see cref="CustomReactant"/> for more information.
        /// </summary>
        /// <param name="reagentUniqueName">The unique Name of the reagent.</param>
        /// <param name="coefficient">the coefficient to use (produced amount of the fluid).</param>
        public void AddProduct(string reagentUniqueName, float coefficient)
        {
            this.Products.Add(new(reagentUniqueName, coefficient));
        }


        /// <summary>
        /// Create a new instance of a custom ReagentReaction.
        /// Used to make two Reagents combine with each other to produce a new one for example.
        /// </summary>
        public CustomReagentReaction()
        {
            this.Reactions = new();
            this.Reactants = new();
            this.Products = new();
        }

    }
}
