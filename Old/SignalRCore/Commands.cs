namespace SignalRCore
{
    public static class Commands
    {
        public const string Hub = "Hub";
        public static class HubCommands
        {
            public const string RunJob = "RunJob";
            public const string JoinGroup = "JoinGroup";
            public const string UpdatePolling = "UpdatePolling";
        }
    }
}