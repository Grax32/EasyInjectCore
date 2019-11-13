using System;
using System.Runtime.Serialization;

namespace Grax32.EasyInjectCore
{
    [Serializable]
    internal class ServiceRegistrationException : Exception
    {
        public ServiceRegistrationException()
        {
        }

        public ServiceRegistrationException(string message) : base(message)
        {
        }

        public ServiceRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}