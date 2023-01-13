using FrontFace.Plugin;

namespace Sample_Content_Plugin
{
    /// <summary>
    /// This class represents the settings UI implementation
    /// </summary>
    public class SettingsUI : ISettingsUI
    {
        /// <summary>
        /// The plugin has it's own settings class
        /// </summary>
        private Settings PluginSettings;

        private SettingsUserControl UserControl;
        private PluginEnvironmentInfo EnvironmentInfo;

        /// <summary>
        /// Returns a serialized settings string to the Player App which persists the settings
        /// </summary>
        /// <returns></returns>
        public string GetSettings()
        {
            if (UserControl != null)
            {
                if (PluginSettings == null)
                    PluginSettings = new Settings();

                // Apply settings from user interface to settings instance
                PluginSettings.SomeText = UserControl.tbSomeText.Text;
                PluginSettings.AnimateBackground = (bool)UserControl.cbAnimateBackground.IsChecked;
            }

            // Returns the settings as a serialized string
            return PluginSettings.Serialize();
        }

        /// <summary>
        /// Returns the settings user interface
        /// </summary>
        /// <typeparam name="T">Has to be derived from UIElement</typeparam>
        /// <param name="settings">The current settings</param>
        /// <param name="environmentInfo">Information about what environment the plugin is running</param>
        /// <returns></returns>
        public T GetSettingsUI<T>(string settings, PluginEnvironmentInfo environmentInfo)
        {
            // Try to load the settings first
            try
            {
                PluginSettings = FrontFace.Plugin.Serialization.Deserialize<Settings>(settings);
            }
            catch
            {
                PluginSettings = new Settings();
            }

            EnvironmentInfo = environmentInfo;

            // Apply settings to UserControl
            UserControl = new SettingsUserControl();
            UserControl.tbSomeText.Text = PluginSettings.SomeText;
            UserControl.cbAnimateBackground.IsChecked = PluginSettings.AnimateBackground;

            // Before you can cast to "(T)", make sure to cast to "object" first!
            return (T)(object)UserControl;
        }
    }
}
