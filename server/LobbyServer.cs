using System.Net;
using System.Net.Sockets;

class LobbyServer
{
    private int _port;
    private IPAddress? _localAddress;

    private const int _MaxPlayerCount = 4;

    public LobbyServer(int port, string ipAddress)
    {
        //Console.WriteLine("LobbyServer constructor start");
        _port = port;
        try
        {
            _localAddress = IPAddress.Parse(ipAddress);
        }
        catch (FormatException fe)
        {
            Console.Error.WriteLine(fe.Message);
            Console.Error.WriteLine($"Wrongly formatted string: \"{ipAddress}\"");
            throw;
        }
        //Console.WriteLine("LobbyServer constructor end");
    }

    public void Init()
    {
        Console.WriteLine("LobbyServer Init called");
        var server = new TcpListener(_localAddress, _port);
        server.Start();

        Byte[] bytes = new Byte[256];
        String? data = null;

        var clients = new List<TcpClient>();
        while (clients.Count < _MaxPlayerCount)
        {
            var currentClient = server.AcceptTcpClient();
            clients.Add(currentClient);
            Console.WriteLine("New client connected.");

            data = null;

            var networkStream = currentClient.GetStream();

            var returnMessage = System.Text.Encoding.ASCII.GetBytes($"Your number is {clients.Count - 1}");
            networkStream.Write(returnMessage, 0, returnMessage.Length);
        }

        clients.ForEach(c => c.Close());
        server.Stop();
    }
}