using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonExperimenting
{
    public class ErrorInformation
    {
        [JsonProperty("ErrorType")]
        public string ErrorType { get; set; }

        [JsonProperty("ErrorCode")]
        public int? ErrorCode { get; set; }

        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        [Serializable]
        internal class PolitburoException : Exception
        {
            public PolitburoException()
            {
            }

            public PolitburoException(string message) : base(message)
            {
            }

            public PolitburoException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected PolitburoException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        internal class AuditError : Exception
        {
            public AuditError()
            {
            }

            public AuditError(string message) : base(message)
            {
            }

            public AuditError(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected AuditError(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}