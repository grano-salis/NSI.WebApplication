using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Address = new HashSet<Address>();
            CaseInfo = new HashSet<CaseInfo>();
            Client = new HashSet<Client>();
            Contact = new HashSet<Contact>();
            Conversation = new HashSet<Conversation>();
            Document = new HashSet<Document>();
            Hearing = new HashSet<Hearing>();
            Meeting = new HashSet<Meeting>();
            Message = new HashSet<Message>();
            Note = new HashSet<Note>();
            Participant = new HashSet<Participant>();
            Task = new HashSet<Task>();
            UserCase = new HashSet<UserCase>();
            UserHearing = new HashSet<UserHearing>();
            UserMeeting = new HashSet<UserMeeting>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string AvatarPath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<CaseInfo> CaseInfo { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Conversation> Conversation { get; set; }
        public ICollection<Document> Document { get; set; }
        public ICollection<Hearing> Hearing { get; set; }
        public ICollection<Meeting> Meeting { get; set; }
        public ICollection<Message> Message { get; set; }
        public ICollection<Note> Note { get; set; }
        public ICollection<Participant> Participant { get; set; }
        public ICollection<Task> Task { get; set; }
        public ICollection<UserCase> UserCase { get; set; }
        public ICollection<UserHearing> UserHearing { get; set; }
        public ICollection<UserMeeting> UserMeeting { get; set; }
    }
}
