using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataHandle.FileStreamHandle
{
    public class FlushToDiskFileStream : FileStream
    {
        public FlushToDiskFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
            : base(path, mode, access, share, bufferSize, options)
        {
        }

        public override void Flush()
        {
            // Ensure that all buffered data in intermediate file buffers is written to disk
            // Passing in true below results in a call to FlushFileBuffers
            base.Flush(true);
        }
    }
}
