namespace Sample_Event_Source_Plugin
{
    /// <summary>
    /// This class represents the settings of the plugin
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Interval for generating events in [sec.]
        /// </summary>
        private int interval = 30;
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }

        /// <summary>
        /// Display duration of playlists started by the plugin
        /// </summary>
        private int displayDuration = 15;
        public int DisplayDuration
        {
            get
            {
                return displayDuration;
            }
            set
            {
                displayDuration = value;
            }
        }

        /// <summary>
        /// Loop/repeat count for the playlist started by the plugin
        /// </summary>
        private int loopCount = -1;
        public int LoopCount
        {
            get
            {
                return loopCount;
            }
            set
            {
                loopCount = value;
            }
        }

        /// <summary>
        /// The type of event that is generated (e.g., 0 = start playlist, or 2 = stop playlist)
        /// </summary>
        private int action = 0;
        public int Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
            }
        }

        /// <summary>
        /// A key/value list of the placeholders and their values (Syntax (line by line): Key=Value)
        /// </summary>
        private string placeholders = string.Empty;
        public string Placeholders
        {
            get
            {
                return placeholders;
            }
            set
            {
                placeholders = value;
            }
        }
    }
}
