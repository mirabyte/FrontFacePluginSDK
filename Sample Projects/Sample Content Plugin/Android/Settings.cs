namespace Sample_Content_Plugin_Android
{
    /// <summary>
    /// This class represents the settings of the plugin
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Determines whether the background is animated or not
        /// </summary>
        public bool AnimateBackground { get; set; }

        /// <summary>
        /// Property for the displayed text
        /// </summary>
        public string SomeText { get; set; } = "Hello World!";
    }
}