using System.Text.Json;

namespace Client_Server_Refactored.Server
{
    internal static class MessageSerializer
    {
        public static Dictionary<string, string> Deserialize(Span<byte> message)
        {
            return 
                JsonSerializer.Deserialize<Dictionary<string, string>>(message)
                ??
                new Dictionary<string, string>();
        }

        public static byte[] SerializeToBytes(Dictionary<string, string> message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message);
        }
    }
}
