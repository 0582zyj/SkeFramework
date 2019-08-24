﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    public class NodeUri : Uri
    {
        public NodeUri(INode node) : base(GetUriStringForNode(node))
        {
        }

        public NodeUri(string uriString)
            : base(uriString)
        {
        }

        protected NodeUri(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        #region Static helper methods

        public static string GetUriStringForNode(INode node)
        {
            if (node.IsEmpty()) return string.Empty;
            return string.Format("{0}://{1}:{2}", GetProtocolStringForTransportType(node.TransportType),
                GetHostStringForAddress(node.Host),
                node.Port);
        }

        public static string GetProtocolStringForTransportType(TransportType transport)
        {
            switch (transport)
            {
                case TransportType.Tcp:
                    return "tcp";
                case TransportType.Udp:
                    return "udp";
                default:
                    return "socket";
            }
        }

        public static string GetHostStringForAddress(IPAddress address)
        {
            var host = address.ToString();
            if (address.AddressFamily == AddressFamily.InterNetworkV6)
            {
                host = string.Format("[{0}]", host);
            }
            return host;
        }

#if !NET35 && !NET40
        public static INode GetNodeFromUri(Uri uri)
        {
            uri.NotNull();
            var transport = TransportType.All;
            Enum.TryParse(uri.Scheme, true, out transport);
            return NodeBuilder.BuildNode().Host(uri.Host).WithPort(uri.Port).WithTransportType(transport);
        }
#endif

        #endregion
    }
}
