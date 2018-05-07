using System;
using System.Runtime.Serialization;
using Tacs.Services;

namespace Tacs.Models.Exceptions
{
    [Serializable]
    public class InsufficientAmountException : BusinnesException
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