﻿using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace FlaUI.Core.Exceptions
{
    [Serializable]
    public class MethodNotSupportedException : FlaUIException
    {
        public MethodNotSupportedException()
        {
        }

        public MethodNotSupportedException(string message)
            : base(message)
        {
        }

        public MethodNotSupportedException(Exception innerException)
            : base(String.Empty, innerException)
        {
        }

        public MethodNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected MethodNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
