using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Net
{
    /// <summary>
    /// Typed delegate used for handling received data
    /// </summary>
    /// <param name="incomingData">a <see cref="NetworkData"/> instance that contains information that's arrived over the network</param>
    public delegate void ReceivedDataCallback(NetworkData incomingData, IConnection responseChannel);

    /// <summary>
    /// Delegate used when a new connection is successfully established
    /// </summary>
    /// <param name="remoteAddress">The remote endpoint on the other end of this connection</param>
    public delegate void ConnectionEstablishedCallback(INode remoteAddress, IConnection responseChannel);

    /// <summary>
    /// Delegate used when a connection is closed
    /// </summary>
    /// <param name="closedChannel">The channel that is now closed</param>
    public delegate void ConnectionTerminatedCallback(HeliosConnectionException reason, IConnection closedChannel);

    /// <summary>
    /// Delegate used when an unexpected error occurs
    /// </summary>
    /// <param name="connection">The connection object responsible for propagating this error</param>
    /// <param name="ex">The exception that occurred</param>
    public delegate void ExceptionCallback(Exception ex, IConnection connection);

    public class IConnection
    {
    }
}
