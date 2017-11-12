using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Response
{
    public class NSIResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
