using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataCommon
{
    public class EventMetadata : Dictionary<string, object>
    {
        public EventMetadata()
        {
        }

        public EventMetadata(Dictionary<string, object> metadata)
            : base(metadata)
        {
        }
    }
}
