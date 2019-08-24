﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    ///     Extension methods to make it easier to work with INode implementations
    /// </summary>
    public static class NodeExtensions
    {
        public static INode ToNode(this IPEndPoint endPoint, TransportType transportType)
        {
            return new Node { Host = endPoint.Address, Port = endPoint.Port, TransportType = transportType };
        }

        public static Uri ToUri(this INode node)
        {
            return new NodeUri(node);
        }

#if !NET35 && !NET40
        public static INode ToNode(this Uri uri)
        {
            return NodeUri.GetNodeFromUri(uri);
        }
#endif

        public static bool IsEmpty(this INode node)
        {
            return node is EmptyNode;
        }
    }
}