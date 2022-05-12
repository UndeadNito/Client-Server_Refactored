using System.Net.Sockets;

namespace Client_Server_Refactored.Server
{
    internal class MessageProvider
    {
        private Client _client;
        private MessageSerializer _serializer;

        private NetworkStream _stream;

        public MessageProvider(Client client)
        {
            _client = client;
            _stream = client.client.GetStream();
            _serializer = new MessageSerializer();
        }
        
        public bool GetMessage()
        {
            if (!_stream.CanRead || !_stream.DataAvailable) { return false; }

            Int32 dataLength = GetMessageLength(_stream);

            Span<byte> message = stackalloc byte[dataLength];
            _stream.Read(message);

            var parsedMessage = _serializer.Deserialize(message); //TODO create MessageOperator working with messages

            return true;
        }

        private Int32 GetMessageLength(NetworkStream stream)
        {
            byte[] buffer = new byte[4];

            stream.Read(buffer, 0, 4);

            return BitConverter.ToInt32(buffer, 0);
        }

        public bool SendMessage(Dictionary<string, string> message)
        {
            if (!_stream.CanWrite) return false;

            List<byte> messageToSend = new List<byte>(_serializer.Serialize(message));
            messageToSend.InsertRange(0, BitConverter.GetBytes(messageToSend.Count));

            _stream.Write(new Span<byte>(messageToSend.ToArray()));
            return true;
        }
    }
}
