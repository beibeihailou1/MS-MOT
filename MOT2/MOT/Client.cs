using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Polly;
using System.Net.NetworkInformation;

namespace Client
{
    class Client
    {
        private bool isConnetcted = false;
        public bool is_Connetcted() { return isConnetcted; }
        public ManualResetEvent allDone = new ManualResetEvent(false);
        private Socket s;
        private static IPAddress address = IPAddress.Parse("127.0.0.1");
        private IPEndPoint ipe = new IPEndPoint(address, 6001);
        public  void SocketConnect()
        {
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            while (true)
            {
                IPEndPoint[] ipsTCP = iPGlobalProperties.GetActiveTcpListeners();
                bool flag = false;
                foreach (IPEndPoint iPEndPoint in ipsTCP)
                {
                    if (iPEndPoint.Equals(ipe))
                    {
                        flag = true;
                    }
                }
                if (flag) break;
                Thread.Sleep(500);
            }
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
            allDone.Reset();
            s.BeginConnect(ipe, new AsyncCallback(SocketSend), s);
            allDone.WaitOne();
        }

        public void SocketSend(IAsyncResult ar)
        {
            /*Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipe = new IPEndPoint(address, 6001);
            s.Connect(ipe);*/
            s = (Socket)ar.AsyncState;
            s.EndConnect(ar);
            isConnetcted = true;

        }

        public void SendMessage(string message)
        {
            byte[] msg;
            msg = Encoding.ASCII.GetBytes(message);
            s.Send(msg, 0);
        }
    }
}
