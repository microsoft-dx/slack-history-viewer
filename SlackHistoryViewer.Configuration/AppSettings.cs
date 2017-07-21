namespace SlackHistoryViewer.Configuration

{
    public class AppSettings
    {
        public string UsersFile { get; set; }

        public string ChannelsFile { get; set; }

        public string IntegrationLogsFile { get; set; }

        public string SlackBotId { get; set; }

        public string SlackBotName { get; set; }

        public string UnknownBotId { get; set; }

        public string UnknownBotName { get; set; }

        public string ConnectionString { get; set; }

        public string UsersListUrl { get; set; }

        public string ChannelsListUrl { get; set; }

        public string ChannelsHistoryUrl { get; set; }

        public string SlackToken { get; set; }

        #region Singleton

        private static AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettings();
                }

                return _instance;
            }
        }

        private AppSettings()
        {
        }

        #endregion
    }
}
