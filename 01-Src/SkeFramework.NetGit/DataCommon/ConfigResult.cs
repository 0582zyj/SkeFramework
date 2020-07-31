using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataCommon
{
    public class ConfigResult
    {
        private readonly Result result;
        private readonly string configName;

        public ConfigResult(Result result, string configName)
        {
            this.result = result;
            this.configName = configName;
        }

        public bool TryParseAsString(out string value, out string error, string defaultValue = null)
        {
            value = defaultValue;
            error = string.Empty;

            if (this.result.ExitCodeIsFailure && this.result.StderrContainsErrors())
            {
                error = "Error while reading '" + this.configName + "' from config: " + this.result.Errors;
                return false;
            }

            if (this.result.ExitCodeIsSuccess)
            {
                value = this.result.Output?.TrimEnd('\n');
            }

            return true;
        }

        public bool TryParseAsInt(int defaultValue, int minValue, out int value, out string error)
        {
            value = defaultValue;
            error = string.Empty;

            if (!this.TryParseAsString(out string valueString, out error))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(valueString))
            {
                // Use default value
                return true;
            }

            if (!int.TryParse(valueString, out value))
            {
                error = string.Format("Misconfigured config setting {0}, could not parse value `{1}` as an int", this.configName, valueString);
                return false;
            }

            if (value < minValue)
            {
                error = string.Format("Invalid value {0} for setting {1}, value must be greater than or equal to {2}", value, this.configName, minValue);
                return false;
            }

            return true;
        }
    }
}
