using constant;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Queue;

namespace MOT_PLC
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public class StateObject
        {
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }
        Window1 window1 = new Window1();

        private Queue.Queue queue0=new Queue.Queue();
        private Queue.Queue queue2=new Queue.Queue();
        private static object o0 = new object();
        private static object o2 = new object();


        public ManualResetEvent accptDone = new ManualResetEvent(false), recevDone = new ManualResetEvent(false);

        private void Accpt()
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1"); //服务器地址
            IPEndPoint ipe = new IPEndPoint(address,6001); //端口
            listenSocket.Bind(ipe);

            listenSocket.Listen(10); //最大等待连接数

            while (true)
            {
                accptDone.Reset();
                listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);
                accptDone.WaitOne();
            }
        }

        private  void AcceptCallback(IAsyncResult ar)
        {
            accptDone.Set();
            Socket listensocket = (Socket)ar.AsyncState;
            Socket handler = listensocket.EndAccept(ar);

            StateObject state = new StateObject();

            byte[] heartBeatMsg = Encoding.ASCII.GetBytes("heartbeat");

            while (true)
            {
                recevDone.Reset();
                try
                {
                    handler.Send(heartBeatMsg,0);
                }
                catch(SocketException e)
                {
                    return;
                }
                handler.BeginReceive(state.buffer, 0,sizeof(char), 0, new AsyncCallback(ReadCallback), state);
                recevDone.WaitOne();
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            string messge = Encoding.ASCII.GetString(state.buffer);
            if (messge[0] == '1')
            {
                Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++收到消息+++++++++++|"); }));
               
                lock (o0)
                {
                    queue0.Enqueue(1);
                }
            }
            recevDone.Set();

        }

        private void Delivery()
        {
            while (true)
            {
                while (true)
                {
                    bool flag = false;
                    lock (o0)
                    {
                        flag=queue0.Dequeue();
                    }
                    if (!flag)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(5000);
                        DataRowCollection collectionPreOrder = query(0);
                        if (Convert.ToBoolean(collectionPreOrder.Count))
                        {
                            Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++开始传输+++++++++++|"); }));

                            Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++正在传输+++++++++++|"); }));

                            Thread.Sleep(10000);
                            Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++传输完成+++++++++++|"); }));

                            // PLC通讯出货
                            string orderId = collectionPreOrder[0]["out_id"].ToString();
                            update(orderId, 1);

                        }
                        
                    }
                }
                queue0.Reset();
                queue0.Wait();
            }
        }

        private void Open()
        {
            while (true)
            {
                while (true)
                {
                    bool flag = false;
                    lock (o2)
                    {
                        flag = queue2.Dequeue();
                    }
                    if (!flag)
                    {
                        break;
                    }
                    else
                    {
                        DataRowCollection collection = query(2);
                        Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++开始开门+++++++++++|"); }));
                        Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++正在开门+++++++++++|"); }));
                        Thread.Sleep(60000);
                        Dispatcher.Invoke(new Action(delegate { window1.richtextbox.AppendText("|++++++++++++开门成功+++++++++++|"); }));
                        
                        // PLC通讯，
                        String orderId = collection[0]["out_id"].ToString();
                        update(orderId, 3);
                    }
                }
                queue2.Reset();
                queue2.Wait();
            }
        }

        public void Queue2Enqueue(int state)
        {
            queue2.Enqueue(state);
        }

        private void QueryOrder()
        {
            // 查询订单状态
            // 0 未操作
            // 1 需要PLC出货

            Thread AccptTread = new Thread(new ThreadStart(Accpt));

            AccptTread.Start();
            

            Thread deliveryThread = new Thread(new ThreadStart(Delivery));

            deliveryThread.Start();

            Thread openTread = new Thread(new ThreadStart(Open));

            openTread.Start();


            while (true)
            {
                Thread.Sleep(500);
                // PLC心跳
            }





            /*while (true)
            {
                DataRowCollection collection = query(2);

                if (collection.Count >= 1) // 有状态为2，有人来领货
                {
                    // PLC通讯，
                    String orderId = collection[0]["out_id"].ToString();
                    update(orderId, 3);
                }
                else
                {
                    // 查询等待出货的订单
                    DataRowCollection collectionPreOrder = query(0);
                    if (collectionPreOrder.Count >= 1) // 预下单订单 通知PLC出货
                    {
                        // PLC通讯出货
                        String orderId = collectionPreOrder[0]["out_id"].ToString();
                        update(orderId, 1);
                    }
                    else
                    {
                        // PLC心跳
                    }
                }

                Thread.Sleep(500);
            }
            */
        }

        private DataRowCollection query(int state)
        {
            
            using(MySqlConnection conn = new MySqlConnection(Constant.myConnectionString))
            {
                String sql = "select * from out_order where state = " + state;
                conn.Open();
                using (MySqlDataAdapter md = new MySqlDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    md.Fill(ds, "out_order");
                    return ds.Tables["out_order"].Rows;
                }
            }
        }

        private Boolean update(string orderId, int state)
        {
            using (MySqlConnection conn = new MySqlConnection(Constant.myConnectionString))
            {
                String sql = String.Format("update out_order set state = {0} where out_id = '{1}'", state, orderId);
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    return null != cmd.ExecuteScalar();
                }
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            window1.Show();
            ThreadStart childref = new ThreadStart(QueryOrder);
            Thread query = new Thread(childref);
            query.Start();
        }

    }
}
