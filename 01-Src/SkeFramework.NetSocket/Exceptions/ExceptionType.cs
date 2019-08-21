namespace SkeFramework.NetSocket.Exceptions
{
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum ExceptionType
    {
        Unknown,
        NotOpen,
        AlreadyOpen,
        TimedOut,
        EndOfFile,
        NotSupported,
        Closed
    }
}