using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Exceptions
{
    public class NSIException : Exception
    {
        public Enums.Level Severity { get; set; }
        public Enums.ErrorType ErrorType { get; set; }
        private object _requestData;

        public NSIException(): base() {
            Severity = Enums.Level.Error;
            ErrorType = Enums.ErrorType.Unknown;
        }

        public NSIException(Enums.Level severity, Enums.ErrorType errorType): base() {
            Severity = severity;
            ErrorType = errorType;
        }

        public NSIException(string message, Enums.Level severity, Enums.ErrorType errorType) : base(message)
        {
            Severity = severity;
            ErrorType = errorType;
        }

        public NSIException(string message, Enums.Level severity, Enums.ErrorType errorType, object request) : base(message)
        {
            Severity = severity;
            ErrorType = errorType;
            _requestData = request;
        }

        public NSIException(string message): base(message)
        {
            Severity = Enums.Level.Error;
            ErrorType = Enums.ErrorType.Unknown;
        }
    }
}
