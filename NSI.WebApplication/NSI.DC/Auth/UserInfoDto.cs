using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.Auth
{
    [DataContract]
    public class UserInfoDto
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string AvatarPath { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateModified { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public int CustomerId { get; set; }
    }
}
