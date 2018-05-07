using System;
using System.Runtime.Serialization;

namespace Tacs.Services
{
    [Serializable]
    public class BusinnesException : Exception
    {
        public BusinnesException()
        {
        }

        public BusinnesException(string message) : base(message)
        {
        }

        public BusinnesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinnesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}