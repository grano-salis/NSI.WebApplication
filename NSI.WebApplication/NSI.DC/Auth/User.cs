using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public ICollection<UserBranch> Branches { get; set; }
        public Company Company { get; set; }
    }

    public class UserBranch
    {
        public int BranchID { get; set; }
        public int RoleID { get; set; }
    }

    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
