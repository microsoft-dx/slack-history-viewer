namespace SlackHistoryViewer.RestoreFromLocal
{
    internal class RestoreFromLocal
    {
        public void Run(string directoryPath)
        {
            Database.TruncateTables("Messages", "Channels", "Users");

            Database.InsertUsers(directoryPath);
            Database.InsertChannels(directoryPath);
            Database.InsertMessages(directoryPath);
        }
    }
}
