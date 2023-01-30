﻿using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Sample_Event_Source_Plugin
{
    /// <summary>
    /// This is the entry point for this plugin.
    /// "MyEventSourcePlugin" is dervierd from "PluginEventSource" because it is an Event Source Plugin.
    /// This plugin also uses the FrontFace.Plugin.Serialization-Namespace for it's own settings class.
    /// </summary>
    public class MyEventSourcePlugin : FrontFace.Plugin.PluginEventSource
    {
        /// <summary>
        /// This plugin uses it's own settings class
        /// </summary>
        private Settings PluginSettings;

        /// <summary>
        /// The timer that triggers the events generated by this plugin
        /// </summary>
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Starts the timer to trigger the events
        /// </summary>
        public override void Start()
        {
            // Try to load settings
            try
            {
                PluginSettings = FrontFace.Plugin.Serialization.Deserialize<Settings>(this.Settings);
            }
            catch
            {
                PluginSettings = new Settings();
            }

            // Start timer with specified interval from the settings
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(PluginSettings.Interval);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Apply settings
            FrontFace.Plugin.TriggerEventAction action = FrontFace.Plugin.TriggerEventAction.StartPlaylist;

            if (PluginSettings.Action == 2)
                action = FrontFace.Plugin.TriggerEventAction.StopPlaylist;

            // Prepare the placeholders dictionary
            Dictionary<string, string> placeholders = new Dictionary<string, string>();

            foreach (string line in PluginSettings.Placeholders.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    string key = line.Split("=".ToCharArray())[0];
                    string value = line.Split("=".ToCharArray())[1];

                    placeholders.Add(key.Trim(), value.Trim());
                }
                catch
                {
                    // ignore invalid lines
                }
            }

            // This timer triggers the OnTrigger-Event with the paramters set in the settings
            OnTrigger(new FrontFace.Plugin.EventSourcePluginTriggerEventArgs()
            {
                Action = action,
                Duration = TimeSpan.FromSeconds(PluginSettings.DisplayDuration),
                LoopCount = PluginSettings.LoopCount,
                Placeholders = placeholders
            });
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

        public override void Pause()
        {
            // Pause your event source control here
            timer.Stop();
        }

        public override void Resume()
        {
            // Resume your event source control here
            timer.Start();
        }

        /// <summary>
        /// Free is called when the plugin will be destroyed, so make sure to clear all resources and instances, 
        /// that .NET Garbage Collector can clean these up.
        /// </summary>
        public override void Free()
        {
            timer.Stop();
            timer.Tick -= Timer_Tick;
        }
    }
}