using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
