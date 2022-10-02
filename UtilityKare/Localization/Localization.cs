using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine;

namespace UtilityKare
{
    /// <summary>
    /// Class to create custom localization and to automatically include them into the base game.
    /// </summary>
    public class Localization
    {

        public List<LocalizationKey> LocalizationKeys;

        /// <summary>
        /// A new Localization will automatically add your custom ones into the database once the locale is changed.
        /// </summary>
        public Localization()
        {
            LocalizationKeys = new();
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChange;
        }

        /// <summary>
        /// A new Localization will automatically add your custom ones into the database once the locale is changed.
        /// If you created the entries seperately, you can just pass a list of those in instead!
        /// </summary>
        public Localization(List<LocalizationKey> entries) : base()
        {
            LocalizationKeys = entries;
        }

        /// <summary>
        /// Add a new Key entry with a default value.
        /// It will return the entry and allow you to add translations to it with a method call.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns>LocalizedEntry</returns>
        public LocalizationKey AddKeyEntry(string Key, string DefaultValue)
        {
            LocalizationKey newEntry = new(Key, DefaultValue);
            LocalizationKeys.Add(newEntry);
            return newEntry;
        }

        /// <summary>
        /// Returns a unity LocalizedString ready for use wherever you may have a need for it.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>LocalizedString</returns>
        public LocalizedString GetLocalizedString(string Key)
        {
            int index = LocalizationKeys.FindIndex(x => x.Key == Key);
            new LocalizedString("StringDatabase", Key).GetLocalizedString();
            if (index != -1) return new LocalizedString("StringDatabase", Key);
            Debug.LogWarning($"[{PluginInfo.PLUGIN_GUID}] WARNING: No Entry found for {Key}");
            return new LocalizedString("StringDatabase", Key);
        }

        /// <summary>
        /// Applies custom locale entries on change, since Unity resets the tables each time.
        /// </summary>
        /// <param name="currentLocale"></param>
        private void OnLocaleChange(Locale currentLocale)
        {
            UtilityKarePlugin.Log.LogInfo("Applying custom localization");
            foreach(LocalizationKey Entry in LocalizationKeys)
            {
                string Code = currentLocale.Identifier.Code;
                var Table = LocalizationSettings.StringDatabase.GetTable("StringDatabase", currentLocale);

                if (Entry.Translations.ContainsKey(Code))
                    Table.AddEntry(Entry.Key, Entry.Translations[Code]);
                else
                    Table.AddEntry(Entry.Key, Entry.Translations["default"]);
            }
        }
    }
}
