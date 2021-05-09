namespace Worktop.Core.Helpers
{
    public class StorageLocation
    {
        public static string ServerAddress { get; private set; }

        public static void Init(string serverAddress) => ServerAddress = serverAddress;

        public static string BuildLocation(string path)
            => !path.Contains("http")
                ? $"{ServerAddress}{(path.StartsWith("/") ? path : $"/{path}")}"
                : path;
    }
}