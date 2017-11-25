using NSI.DC.ContactsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IContactsRepository
    {
        ICollection<ContactDto> GetContacts();
    }
}
