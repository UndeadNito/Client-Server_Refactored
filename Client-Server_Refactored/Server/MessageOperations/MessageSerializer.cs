using System.Text.Json;

namespace Client_Server_Refactored.Server
{
    internal class MessageSerializer
    {

        public MessageSerializer() { }

        public Dictionary<string, string>? Deserialize(Span<byte> message)
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(message);
        }

        public byte[] Serialize(Dictionary<string, string> message)
        {
            return JsonSerializer.SerializeToUtf8Bytes<Dictionary<string, string>>(message);
        }
    }
}
