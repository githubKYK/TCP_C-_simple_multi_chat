public class Program
{
    public static void Main()
    {
        ServerServer.Server server = new ServerServer.Server("127.0.0.1", 3000);
        server.start();
        server.stop();
    }
}