using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UtilityKare
{

    public class LocalizationKey
    {
        /// <summary>
        /// The key to identify it for the localization.
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// This contains all the translations as a dictionary (KeyCode, Text)
        /// The provided default value will be stored in the "default" key.
        /// </summary>
        public readonly Dictionary<string, string> Translations;

        /// <summary>
        /// Creates a new Entry for the localization of the given key with a default value
        /// </summary>
        /// <param name="Key">The Key to use (ex. "REAGENT_CUSTOM")</param>
        /// <param name="DefaultValue">The text to use (ex. "Custom Reagent")</param>
        public LocalizationKey(string Key, string DefaultValue)
        {
            this.Key = Key;
            this.Translations = new();
            this.Translations["default"] = DefaultValue;
            
        }

        /// <summary>
        /// Adds a string for the given language code.
        /// </summary>
        /// <param name="LanguageCode">The language code (ex. "en")</param>
        /// <param name="Value">The Text for the given language</param>
        public void AddTranslation(string LanguageCode, string Value)
        {
            if (LocalizationSettings.AvailableLocales.Locales.FindIndex(x => x.Identifier.Code == LanguageCode) != -1)
                Translations[LanguageCode] = Value;
            else
                Debug.LogError($"[{PluginInfo.PLUGIN_GUID}] ERROR: Could not find Language Code {LanguageCode} for the game.");
        }
    }
}
