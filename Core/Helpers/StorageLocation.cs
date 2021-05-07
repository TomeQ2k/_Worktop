namespace Worktop.Core.Helpers
{
    public class StorageLocation
    {
        public static string ServerAddress { get; private set; }

        public static void Init(string serverAddress) => ServerAddress = serverAddress;

        public static string BuildLocation(string path) =>
            $"{ServerAddress}{(path.StartsWith("/") ? path : $"/{path}")}";
    }
}