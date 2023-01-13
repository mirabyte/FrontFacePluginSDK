namespace Sample_Content_Plugin
{
    /// <summary>
    /// This class represents the settings of the plugin
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Determines whether the background is animated or not
        /// </summary>
        private bool animateBackground = true;
        public bool AnimateBackground
        {
            get { return animateBackground; }
            set { animateBackground = value; }
        }

        /// <summary>
        /// Property for the displayed text
        /// </summary>
        private string someText = "Hello World!";
        public string SomeText
        {
            get { return someText; }
            set { someText = value; }
        }
    }
}