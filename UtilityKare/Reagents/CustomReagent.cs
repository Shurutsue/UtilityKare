using UnityEngine.Localization;
using UnityEngine;
using HarmonyLib;

namespace UtilityKare
{
    public class CustomReagent
    {
        #region [Custom Reagent Fields]
        /// <summary>
        /// The name to identify it with (For example with the give command)
        /// </summary>
        public string UniqueName;
        /// <summary>
        /// The LocalizedString which return the translated name for the given language. See <see cref="Localization"/> for more information.
        /// </summary>
        public LocalizedString LocalizedName;
        /// <summary>
        /// The Color for the fluid
        /// </summary>
        public Color FluidColor;
        /// <summary>
        /// The Emission Color for the fluid
        /// </summary>
        public Color Emission;
        /// <summary>
        /// The reagent's value for each unity of it. (It's worth when selling)
        /// </summary>
        public float? Value;
        /// <summary>
        /// The metabolizationHalfLife defines how fast(or slow) it metabolizes. The lower, the faster.
        /// </summary>
        public float? MetabolizationHalfLife;
        /// <summary>
        /// Whether it acts as a cleaning agent or not. (Like water)
        /// </summary>
        public bool? CleaningAgent;
        /// <summary>
        /// The calories it'll have. (Also by default how much fat or energy it provides)
        /// </summary>
        public float? Calories;
        /// <summary>
        /// Still uncertain, feel free to assist with finding out!
        /// </summary>
        public GameObject Display;
        /// <summary>
        /// The consumption event to use (instance of a ReagentConsumptionEvent class)
        /// </summary>
        public ReagentConsumptionEvent ConsumptionEvent;
        #endregion [Custom Reagent Fields]

        /// <summary>
        /// Creates and returns a ScriptableReagent based of it's own (still public) fields.
        /// </summary>
        public ScriptableReagent GetScriptableReagent()
        {
            ScriptableReagent reagent = ScriptableReagent.CreateInstance<ScriptableReagent>();
            reagent.name = UniqueName ?? "Unnamed";
            var traverse = Traverse.Create(reagent);
            traverse.Field<LocalizedString>("localizedName").Value = LocalizedName ?? (new("StringDatabase", "Unnamed"));
            traverse.Field<Color>("color").Value = (FluidColor != null) ? FluidColor : new(1f, 1f, 1f, 1f);
            traverse.Field<Color>("emission").Value = (Emission != null) ? Emission : new(1f, 1f, 1f, 1f);
            traverse.Field<float>("value").Value = (Value != null) ? (float)Value : 0.25f;
            traverse.Field<float>("metabolizationHalfLife").Value = (MetabolizationHalfLife != null) ? (float)MetabolizationHalfLife : 1.5f;
            traverse.Field<bool>("cleaningAgent").Value = (CleaningAgent != null) && (bool)CleaningAgent;
            traverse.Field<float>("calories").Value = (Calories != null) ? (float)Calories : 0.05f;
            traverse.Field<GameObject>("display").Value = Display ?? (new());
            traverse.Field<ReagentConsumptionEvent>("consumptionEvent").Value = ConsumptionEvent ?? new DefaultConsumption();
            return reagent;
        }

        /// <summary>
        /// Creates and returns a ScriptableReagent based of an existing one with custom content.
        /// This will change all fields of the existing one to the one that have been set for the custom one.
        /// </summary>
        /// <param name="existingReagent"></param>
        /// <returns></returns>
        public ScriptableReagent GetScriptableReagent(ScriptableReagent existingReagent)
        {
            ScriptableReagent reagent = ScriptableReagent.CreateInstance<ScriptableReagent>();
            reagent.name = UniqueName ?? existingReagent.name;
            var traverse = Traverse.Create(reagent);
            var existingTraverse = Traverse.Create(existingReagent);
            traverse.Field<LocalizedString>("localizedName").Value = LocalizedName ?? existingTraverse.Field<LocalizedString>("localizedName").Value;
            traverse.Field<Color>("color").Value = (FluidColor != null) ? FluidColor : existingTraverse.Field<Color>("color").Value;
            traverse.Field<Color>("emission").Value = (Emission != null) ? Emission : existingTraverse.Field<Color>("emission").Value;
            traverse.Field<float>("value").Value = (Value != null) ? (float)Value : existingTraverse.Field<float>("value").Value;
            traverse.Field<float>("metabolizationHalfLife").Value = (MetabolizationHalfLife != null) ? (float)MetabolizationHalfLife : existingTraverse.Field<float>("metabolizationHalfLife").Value;
            traverse.Field<bool>("cleaningAgent").Value = (CleaningAgent != null) ? (bool)CleaningAgent : existingTraverse.Field<bool>("cleaningAgent").Value;
            traverse.Field<float>("calories").Value = (Calories != null) ? (float)Calories : existingTraverse.Field<float>("calories").Value;
            traverse.Field<GameObject>("display").Value = Display ?? existingTraverse.Field<GameObject>("display").Value;
            traverse.Field<ReagentConsumptionEvent>("consumptionEvent").Value = ConsumptionEvent ?? existingTraverse.Field<ReagentConsumptionEvent>("consumptionEvent").Value;
            return reagent;
        }

        #region [Additional Constructors]
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param.</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to "Unnamed", should be changed. See <see cref="Localization"/> to make custom locales</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Default color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Default emission color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName)
        {
            this.UniqueName = uniqueName;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Default color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Default emission color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey)
            : this(uniqueName)
        {
            LocalizedName = new("StringDatabase", localizedKey);
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Default emission color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color)
            : this(uniqueName, localizedKey)
        {
            this.FluidColor = color;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Default emission color will be pure white.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB)
            : this (uniqueName, localizedKey)
        {
            this.FluidColor = new(colorR, colorG, colorB, 1f);
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission)
            : this(uniqueName, localizedKey, color)
        {
            this.Emission = emission;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The value for each unit of the reagent (worth of it)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB)
            : this(uniqueName, localizedKey, colorR, colorG, colorB)
        {
            this.Emission = new(emissionR, emissionG, emissionB, 1f);
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>Fifth param, the (float) value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission, float value)
            : this(uniqueName, localizedKey, color, emission)
        {
            this.Value = value;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The ninth param, the value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB, float value)
            : this(uniqueName, localizedKey, colorR, colorG, colorB, emissionR, emissionG, emissionB)
        {
            this.Value = value;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>Fifth param, the (float) value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission, float value, float metabolizationHalfLife)
            : this(uniqueName, localizedKey, color, emission, value)
        {
            this.MetabolizationHalfLife = metabolizationHalfLife;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The ninth param, the value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB, float value, float metabolizationHalfLife)
            : this(uniqueName, localizedKey, colorR, colorG, colorB, emissionR, emissionG, emissionB, value)
        {
            this.MetabolizationHalfLife = metabolizationHalfLife;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>Fifth param, the (float) value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission, float value, float metabolizationHalfLife, bool cleaningAgent)
            : this(uniqueName, localizedKey, color, emission, value, metabolizationHalfLife)
        {
            this.CleaningAgent = cleaningAgent;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The ninth param, the value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB, float value, float metabolizationHalfLife, bool cleaningAgent)
            : this(uniqueName, localizedKey, colorR, colorG, colorB, emissionR, emissionG, emissionB, value, metabolizationHalfLife)
        {
            this.CleaningAgent = cleaningAgent;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>Fifth param, the (float) value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission, float value, float metabolizationHalfLife, bool cleaningAgent, float calories)
            : this(uniqueName, localizedKey, color, emission, value, metabolizationHalfLife, cleaningAgent)
        {
            this.Calories = calories;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The ninth param, the value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB, float value, float metabolizationHalfLife, bool cleaningAgent, float calories)
            : this(uniqueName, localizedKey, colorR, colorG, colorB, emissionR, emissionG, emissionB, value, metabolizationHalfLife, cleaningAgent)
        {
            this.Calories = calories;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Set to the third param.</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Set to the fourth param.</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>Fifth param, the (float) value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, Color color, Color emission, float value, float metabolizationHalfLife, bool cleaningAgent, float calories, ReagentConsumptionEvent consumptionEvent)
            : this(uniqueName, localizedKey, color, emission, value, metabolizationHalfLife, cleaningAgent, calories)
        {
            this.ConsumptionEvent = consumptionEvent;
        }
        /// <summary>
        /// Creates a CustomReagent with default content.
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>Set to the first param</description>
        /// </item>
        /// <item>
        /// <term>localizedName</term>
        /// <description>Set to the second param. See <see cref="Localization"/> to get a working localizedString</description>
        /// </item>
        /// <item>
        /// <term>color</term>
        /// <description>Will be set by the third, fourth and fith params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>emission</term>
        /// <description>Will be set by the sixth, seventh and eight params, based on RGB values</description>
        /// </item>
        /// <item>
        /// <term>value</term>
        /// <description>The ninth param, the value of the reagent (it's worth)</description>
        /// </item>
        /// <item>
        /// <term>metabolizationHalfLife</term>
        /// <description>The time required for the next action of metabolization (the lower, the faster it metabolizes)</description>
        /// </item>
        /// <item>
        /// <term>cleaningAgent</term>
        /// <description>Whether or not it acts as a cleaning agents (cleans decals instead of creating them)</description>
        /// </item>
        /// <item>
        /// <term>calories</term>
        /// <description>The calories for each unity of the reagent.</description>
        /// </item>
        /// <item>
        /// <term>display</term>
        /// <description>Still unsure, feel free to assist finding out!</description>
        /// </item>
        /// <item>
        /// <term>consumptionEvent</term>
        /// <description>The instance of the class to use for it's consumption event.</description>
        /// </item>
        /// </list>
        /// </summary>
        public CustomReagent(string uniqueName, string localizedKey, float colorR, float colorG, float colorB, float emissionR, float emissionG, float emissionB, float value, float metabolizationHalfLife, bool cleaningAgent, float calories, ReagentConsumptionEvent consumptionEvent)
            : this(uniqueName, localizedKey, colorR, colorG, colorB, emissionR, emissionG, emissionB, value, metabolizationHalfLife, cleaningAgent, calories)
        {
            this.ConsumptionEvent = consumptionEvent;
        }
        #endregion [Additional Constructors]

    }
}
