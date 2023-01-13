using FrontFace.Plugin;

namespace Sample_Event_Source_Plugin
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
                
                // Apply settings from user interface to the settings instance
                if (UserControl.rbStopPlaylist.IsChecked == true)
                    PluginSettings.Action = 2;
                else
                    PluginSettings.Action = 0;

                int.TryParse(UserControl.tbDisplayDuration.Text, out int dispDuration);
                PluginSettings.DisplayDuration = dispDuration;

                int.TryParse(UserControl.tbInterval.Text, out int interval);
                PluginSettings.Interval = interval;

                int.TryParse(UserControl.tbLoopCount.Text, out int loopCount);
                PluginSettings.LoopCount = loopCount;

                PluginSettings.Placeholders = UserControl.tbPlaceholders.Text;
            }

            // Returns the settings as a serialized string
            return PluginSettings.Serialize();
        }

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

            // Create a new user control and apply all currently persisted settings to it
            UserControl = new SettingsUserControl();
            UserControl.tbDisplayDuration.Text = PluginSettings.DisplayDuration.ToString();
            UserControl.tbInterval.Text = PluginSettings.Interval.ToString();
            UserControl.tbLoopCount.Text = PluginSettings.LoopCount.ToString();
            if (PluginSettings.Action == 2)
                UserControl.rbStopPlaylist.IsChecked = true;
            else
                UserControl.rbStartPlaylist.IsChecked = true;

            // Convert placeholders from List to a new-line-seperated string
            if (string.IsNullOrWhiteSpace(PluginSettings.Placeholders))
            {
                if (EnvironmentInfo.AvailablePlaceholders != null)
                {
                    string text = string.Empty;

                    foreach (string placeholder in EnvironmentInfo.AvailablePlaceholders)
                    {
                        text += placeholder + "=" + "\r\n";
                    }

                    UserControl.tbPlaceholders.Text = text.Trim();
                }
                else
                    UserControl.tbPlaceholders.Text = string.Empty;
            }
            else
                UserControl.tbPlaceholders.Text = PluginSettings.Placeholders;

            // Bevor you can cast to (T) make sure to cast to object first!
            return (T)(object)UserControl;
        }
    }
}
