using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using SkeFramework.Core.Common.Enums;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.NetSerialPort.Bootstrap;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.WebSocket;
using SkeFramework.Push.WebSocket.Constants;
using SkeFramework.Push.WebSocket.DataEntities;
using SkeFramework.Push.WebSocket.PushServices.Bootstrap;
using SkeFramework.Push.WebSocket.PushServices.PushFactorys;
using SuperWebSocket;

namespace SkeFramework.Winform.AutoUpdates.Test
{
    public partial class UdpServerForm : Form
    {
        public UdpServerForm()
        {
            InitializeComponent();
        }

        private void UdpServerForm_Load(object sender, EventArgs e)
        {
            //Port = DEFAULT_PORT;
            //var ip = IPAddress.Any;

            //Console.WriteLine("Starting echo server...");
            //Console.WriteLine("Will begin listening for requests on {0}:{1}", ip, Port);
            //var bootstrapper =
            //    new ServerBootstrap()
            //        .WorkerThreads(2)
            //        .SetTransport(TransportType.Udp);
            //var reactor = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(ip).WithPort(Port));
            //reactor.OnConnection += (node, connection) =>
            //{
            //    ServerPrint(node,
            //        string.Format("Accepting connection from... {0}:{1}", node.Host, node.Port));
            //    connection.BeginReceive(Receive);
            //};
            //reactor.OnDisconnection += (reason, address) => ServerPrint(address.RemoteHost,
            //    string.Format("Closed connection to... {0}:{1} [Reason:{2}]", address.RemoteHost.Host,
            //        address.RemoteHost.Port, reason.Type));

            //reactor.Start();
            IConnectionConfig connectionConfig = new DefaultConnectionConfig();
            connectionConfig.SetOption(WebSocketParamEumns.Port.ToString(), 8088);
            var bootstrapper =new PushBootstrap()
                                .SetPushType(PushTypeEumns.WebSocket)
                                .WorkerThreads(2)
                                .BuildPushServerFactory<WebSocketNotifications>();
            IPushBroker<WebSocketNotifications> pushBroker = bootstrapper.NewPushBroker(connectionConfig) as IPushBroker<WebSocketNotifications>;
            pushBroker.OnConnection += PushBroker_OnNewConnection;
            pushBroker.Start();
        }

        private void PushBroker_OnNewConnection(WebSocketNotifications notification)
        {
            notification.session.Close();
        }

        private const int DEFAULT_PORT = 1337;

        //private static int Port;

        private static void ServerPrint(INode node, string message)
        {
            //Console.WriteLine("[{0}] {1}:{2}: {3}", DateTime.UtcNow, node.Host, node.ToString(), message);
        }

        

        public static void Receive(NetworkData data, IConnection connection)
        {
            var node = connection.RemoteHost;

            ServerPrint(connection.RemoteHost, string.Format("recieved {0} bytes", data.Length));
            var str = Encoding.UTF8.GetString(data.Buffer).Trim();
            if (str.Trim().Equals("close"))
            {
                connection.Close();
                return;
            }
            ServerPrint(connection.RemoteHost, string.Format("recieved \"{0}\"", str));
            ServerPrint(connection.RemoteHost,
                string.Format("sending \"{0}\" back to {1}}", str, node.ToString()));
            var sendBytes = Encoding.UTF8.GetBytes(str + Environment.NewLine);
            //connection.Send(new NetworkData { Buffer = sendBytes, Length = sendBytes.Length, RemoteHost = node });
        }
    }
}
