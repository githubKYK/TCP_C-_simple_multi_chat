using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ServerServer
{
    public class Server
    {
        private TcpListener m_server;
        private static List<Client> m_clients;
        char m_char = 'A';
        public Server(string host, int port)
        {
            m_server = new TcpListener(IPAddress.Parse(host), port);
            m_clients = new List<Client>();
        }
        public void start()
        {
            m_server.Start();
            Console.WriteLine("Start server...");

            while (true)
            {
                Socket socket = m_server.AcceptSocket();
                Client client = new Client(socket);
                string name = new string(m_char.ToString());
                m_char++;
                client.name = name;
                m_clients.Add(client);
                client.start();
                Console.WriteLine("Accept new client: " + client.name + "\nNum client: " + m_clients.Count.ToString());
            }
        }
        public static void serverControl(Client fromClient)
        {
            try
            {
                string text;
                while ((text = fromClient.m_reader.ReadString()) != string.Empty)
                {
                    text = fromClient.name + ": " + text;
                    Console.WriteLine(text);
                    foreach (Client client in m_clients)
                    {
                        client.m_writer.Write(text);
                    }
                }
            }
            catch (Exception ex)
            {
                m_clients.Remove(fromClient);
            }
            finally
            {
                fromClient.stop();
                Console.WriteLine("Disconnect client: " + fromClient.name + "Have: " + m_clients.Count.ToString() + " in server");
            }
        }
        public void stop()
        {
            m_server.Stop();
            foreach (Client client in m_clients)
            {
                client.stop();
            }
            Console.WriteLine("Stop server...");
        }

    }
}
