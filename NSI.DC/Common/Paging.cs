using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.Common
{
    [DataContract]
    public class Paging
    {
        [DataMember]
        public int Page { get; set; }

        [DataMember]
        public int RecordsPerPage { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public int Pages
        {
            get
            {
                int pages = 1;
                if (RecordsPerPage > 0)
                {
                    pages = (TotalRecords / RecordsPerPage) + (TotalRecords % RecordsPerPage > 0 ? 1 : 0);
                }

                return pages;
            }
        }
    }
}
