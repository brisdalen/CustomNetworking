using System.Net;
using System.Net.Sockets;

class LobbyClient
{
    private int _id;
    private int _port;
    private string _localAddress;

    public LobbyClient(int id, int port, string ipAddress)
    {
        Console.WriteLine($"Client {id} initiating.");
        _id = id;
        _port = port;
        _localAddress = ipAddress;
        var client = new TcpClient(_localAddress, _port);

        var stream = client.GetStream();

        var dataBuffer = new Byte[256];
        var bytes = stream.Read(dataBuffer, 0, dataBuffer.Length);

        var responseData = System.Text.Encoding.ASCII.GetString(dataBuffer, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);

        //stream.Close();
        //client.Close();
    }
}