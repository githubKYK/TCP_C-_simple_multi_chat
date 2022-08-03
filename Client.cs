using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerServer
{
    public class Client
    {
        public Socket m_socket;
        public NetworkStream m_stream;
        public BinaryReader m_reader;
        public BinaryWriter m_writer;
        public bool m_run = false;
        public string name;

        public Thread m_thread;

        public Client(Socket socket)
        {
            m_socket = socket;
            m_stream = new NetworkStream(m_socket, true);
            m_reader = new BinaryReader(m_stream, Encoding.UTF8);
            m_writer = new BinaryWriter(m_stream, Encoding.UTF8);
        }
        public void start()
        {
            m_run = true;
            m_thread = new Thread(clientControl);
            m_thread.Start();
        }

        public void stop()
        {
            m_socket.Close();
            m_run = false;
        }

        public void clientControl()
        {
            Server.serverControl(this);
        }

    }
}
