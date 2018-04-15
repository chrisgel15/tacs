using System;
using System.Runtime.Serialization;

namespace Tacs.Models.Exceptions
{
    [Serializable]
    public class InsufficientAmountException : Exception
    {
        public InsufficientAmountException()
        {
        }

        public InsufficientAmountException(string message) : base(message)
        {
        }

        public InsufficientAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}