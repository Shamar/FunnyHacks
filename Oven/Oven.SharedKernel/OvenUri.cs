using System;
using System.Runtime.Serialization;

namespace Oven
{
    [Serializable]
    public sealed class OvenUri : Uri
    {
        public OvenUri (string uriString)
            : base(uriString, UriKind.Absolute)
        {
        }
        
        protected OvenUri (SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

