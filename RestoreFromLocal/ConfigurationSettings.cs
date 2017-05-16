using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    class ConfigurationSettings
    {
        private static ConfigurationSettings instance;

        private ConfigurationSettings() { }

        public string UsersFile { get; set; }

        public string ChannelsFile { get; set; }

        public string SlackBotId { get; set; }

        public string ConnectionString { get; set; }

        public static ConfigurationSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationSettings();
                }
                return instance;
            }
        }
    }
}
