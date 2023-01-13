namespace Sample_Content_Plugin
{
    /// <summary>
    /// This is the entry point for this plugin.
    /// "MyContentPlugin" is dervierd from "PluginContent" because it is a Content Plugin.
    /// This plugin also uses the FrontFace.Plugin.Serialization-Namespace for it's own settings class.
    /// </summary>
    public class MyContentPlugin : FrontFace.Plugin.PluginContent
    {
        private bool hasStarted = false;

        /// <summary>
        /// This plugin uses it's own settings class
        /// </summary>
        private Settings PluginSettings;

        /// <summary>
        /// Reference to the actual control that is shown on the screen
        /// </summary>
        private MainUserControl pluginUI;

        /// <summary>
        /// The "main" method that is used to access the plugin's GUI
        /// </summary>
        /// <typeparam name="T">Has to be derived from UIElement</typeparam>
        /// <returns>Returns the presentation user interface</returns>
        public override T GetPresentationUI<T>(object context)
        {
            // Try to load the settings first
            try
            {
                PluginSettings = FrontFace.Plugin.Serialization.Deserialize<Settings>(this.Settings);
            }
            catch
            {
                PluginSettings = new Settings();
            }

            // Create an instance of the UserControl and pass the settings to it
            pluginUI = new MainUserControl();
            pluginUI.tbSomeText.Text = PluginSettings.SomeText;

            // Before you can cast to "(T)", make sure to cast to "object" first!
            return (T)(object)pluginUI;
        }

        /// <summary>
        /// NotifyAppear is called shortly before this plugin will be attached to the screen.
        /// </summary>
        /// <param name="liveView">Determines whether it should be animated yet or not.</param>
        public override void NotifyAppear(bool liveView)
        {
            if (liveView)
            {
                if (PluginSettings.AnimateBackground)
                {
                    pluginUI.Animate = true;
                    hasStarted = true;
                }
            }
        }

        /// <summary>
        /// Start is called when the plugin turns visible.
        /// </summary>
        public override void Start()
        {
            if (!hasStarted)
            {
                if (PluginSettings.AnimateBackground)
                {
                    pluginUI.Animate = true;
                    hasStarted = true;
                }
            }
        }

        public override void Pause()
        {
            if (PluginSettings.AnimateBackground)
            {
                pluginUI.Animate = false;
            }
        }

        public override void Resume()
        {
            if (PluginSettings.AnimateBackground)
            {
                pluginUI.Animate = true;
            }
        }

        /// <summary>
        /// NotifyDisappear is called shortly after the plugin disappears from the screen
        /// </summary>
        /// <param name="liveView">Determines whether it should be animated yet or not.</param>
        public override void NotifyDisappear(bool liveView)
        {
            if (!liveView)
            { 
                if (PluginSettings.AnimateBackground)
                {
                    pluginUI.Animate = false;
                }
            }
        }

        /// <summary>
        /// It gives you the ability to initalize e.g., libraries once at the start of the Player App
        /// </summary>
        public override void Initialize()
        {

        }

        /// <summary>
        /// It's called when the Player App terminates. E.g. you can clear all your resources that you've initalized in Initalize()
        /// </summary>
        public override void Deinitialize()
        {

        }

        /// <summary>
        /// Free is called when the plugin will be destroyed, so make sure to clear all resources and instances, 
        /// that .NET Garbage Collector can clean these up.
        /// </summary>
        public override void Free()
        {
            if (PluginSettings.AnimateBackground)
            {
                pluginUI.Animate = false;
            }
        }
    }
}
