using Android.Content;

namespace Sample_Content_Plugin_Android
{
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

            // Initialize plugin ui (only if context is valid)
            // The context is essentially for initialization of Android views!
            if (context is Context con)
            {
                pluginUI = new MainUserControl(con, PluginSettings);
                return (T)(object)pluginUI.RootLayout;
            }
            
            return (T)(object)null;
        }

        /// <summary>
        /// NotifyAppear is called shortly before this plugin will be attached to the screen.
        /// </summary>
        /// <param name="liveView">Determines whether it should be animated yet or not. On Android liveView is always fale</param>
        public override void NotifyAppear(bool liveView)
        {

        }

        /// <summary>
        /// Start is called when the plugin turns visible.
        /// </summary>
        public override void Start()
        {
            if (!hasStarted)
            {
                if (PluginSettings.AnimateBackground)
                    pluginUI.StartAnimation();

                hasStarted = true;
            }
        }

        public override void Pause()
        {
            if (PluginSettings.AnimateBackground)
                pluginUI.StopAnimation();
        }

        public override void Resume()
        {
            if (PluginSettings.AnimateBackground)
                pluginUI.StartAnimation();
        }

        /// <summary>
        /// NotifyDisappear is called shortly after the plugin disappears from the screen
        /// </summary>
        /// <param name="liveView">Determines whether it should be animated yet or not.</param>
        public override void NotifyDisappear(bool liveView)
        {
            if (PluginSettings.AnimateBackground)
                pluginUI.StopAnimation();
        }

        /// <summary>
        /// It gives you the ability to initialize e.g. libraries once at the start of the Player App
        /// </summary>
        public override void Initialize()
        {

        }

        /// <summary>
        /// It's called when the Player App terminates. E.g. you can clear all your resources that you've initialized in Initialize()
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

        }
    }
}