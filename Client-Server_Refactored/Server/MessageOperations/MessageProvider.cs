using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class MessageProvider
    {
        private readonly NetworkStream _stream;

        public MessageProvider(NetworkStream stream)
        {
            _stream = stream;
        }

        public bool NewMessage()
        {
            if (!_stream.CanRead || !_stream.DataAvailable) { return false; }
            return true;
        }
        
        public Dictionary<string, string> GetMessage()
        {
            Int32 dataLength = GetMessageLength(_stream);

            Span<byte> message = stackalloc byte[dataLength];
            _stream.Read(message);

            return MessageSerializer.Deserialize(message);
        }

        public bool SendMessage(Dictionary<string, string> message)
        {
            if (!_stream.CanWrite) return false;

            List<byte> messageToSend = new(MessageSerializer.SerializeToBytes(message));
            messageToSend.InsertRange(0, BitConverter.GetBytes(messageToSend.Count));

            _stream.Write(new Span<byte>(messageToSend.ToArray()));
            return true;
        }


        private Int32 GetMessageLength(NetworkStream stream)
        {
            byte[] buffer = new byte[4];

            stream.Read(buffer, 0, 4);

            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
