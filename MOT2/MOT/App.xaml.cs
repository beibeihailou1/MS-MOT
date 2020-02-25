using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Client;

namespace MOT
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        private Client.Client client = new Client.Client();
        private void SocketConnect()
        {
            client.SocketConnect();
            
            
        }

        public bool is_Connected()
        {
            return client.is_Connetcted();
        }

       public void SendMessage(string msg)
        {
            client.SendMessage(msg);
        }


        

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ThreadStart childref = new ThreadStart(SocketConnect);
            Thread socketConnect = new Thread(childref);
            socketConnect.Start();
        }
    }
}
