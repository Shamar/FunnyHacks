using System;
using System.Runtime.Serialization;

namespace Oven
{
    [Serializable]
    public sealed class TimerId : Uri
    {
        public TimerId (string uriString)
            : base(uriString, UriKind.Absolute)
        {
        }
        
        protected TimerId (SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}

