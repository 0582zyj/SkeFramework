using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.DataEntities.Constants
{
    public enum MqttQualityLevel
    {
        AtMostOnce = 0,
        AtLeastOnce = 1,
        ExactlyOnce = 2
    }
}
