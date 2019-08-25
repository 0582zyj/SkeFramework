using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Net.Udp;
using SkeFramework.NetSocket.Ops;
using SkeFramework.NetSocket.Serialization;
using SkeFramework.NetSocket.Topology;
using SkeFramework.Topology;

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
            Port = DEFAULT_PORT;
            var ip = IPAddress.Any;

            Console.WriteLine("Starting echo server...");
            Console.WriteLine("Will begin listening for requests on {0}:{1}", ip, Port);
            //var bootstrapper =
            //    new ServerBootstrap()
            //        .WorkerThreads(2)
            //        .SetTransport(TransportType.Udp)
            //        .Build();
            INode listentNode = NodeBuilder.BuildNode().Host(ip).WithPort(Port);
            IExecutor InternalExecutor = new BasicExecutor();
            NetworkEventLoop EventLoop = new NetworkEventLoop(InternalExecutor, 2);
            IMessageEncoder Encoder = new NoOpEncoder();
            IMessageDecoder Decoder = new NoOpDecoder();
            IByteBufAllocator Allocator = null;
            var reactor = new UdpProxyReactor(listentNode.Host, listentNode.Port, EventLoop, Encoder, Decoder, Allocator,
                1024);
            reactor.OnConnection += (node, connection) =>
            {
                ServerPrint(node,
                    string.Format("Accepting connection from... {0}:{1}", node.Host, node.Port));
                connection.BeginReceive(Receive);
            };
            reactor.OnDisconnection += (reason, address) => ServerPrint(address.RemoteHost,
                string.Format("Closed connection to... {0}:{1} [Reason:{2}]", address.RemoteHost.Host,
                    address.RemoteHost.Port, reason.Type));

            reactor.Start();
        }

        private const int DEFAULT_PORT = 1337;

        private static int Port;

        private static void ServerPrint(INode node, string message)
        {
            Console.WriteLine("[{0}] {1}:{2}: {3}", DateTime.UtcNow, node.Host, node.Port, message);
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
                string.Format("sending \"{0}\" back to {1}:{2}", str, node.Host, node.Port));
            var sendBytes = Encoding.UTF8.GetBytes(str + Environment.NewLine);
            connection.Send(new NetworkData { Buffer = sendBytes, Length = sendBytes.Length, RemoteHost = node });
        }
    }
}
