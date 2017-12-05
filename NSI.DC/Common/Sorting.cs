using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.Common
{
    [DataContract]
    public class Sort
    {
        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public SortOrder Order { get; set; }

    }
}
