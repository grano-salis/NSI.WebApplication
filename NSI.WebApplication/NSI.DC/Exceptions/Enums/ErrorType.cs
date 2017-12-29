using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Exceptions.Enums
{
    public enum ErrorType
    {
        None,
        Validation,
        InvalidRequest,
        InvalidConfiguration,
        InvalidData,
        InvalidParameter,
        MissingData,
        DBError,
        Unknown = 99999,
    }
}
