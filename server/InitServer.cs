class InitServer 
{
    public static int Main() 
    {
        var port = 33333;
        var address = "127.0.0.1";

        Console.WriteLine("InitServer started");
        // TODO: handle exception gracefully
        var lobby = new LobbyServer(port, address);

        //Thread InstanceCaller = new Thread(
            //new ThreadStart(serverObject.InstanceMethod));
        var ServerThread = new Thread(new ThreadStart(lobby.Init));
        ServerThread.Start();

        var c1 = new LobbyClient(1, port, address);
        var c2 = new LobbyClient(2, port, address);
        var c3 = new LobbyClient(3, port, address);
        var c4 = new LobbyClient(4, port, address);

        return 0;
    }
}