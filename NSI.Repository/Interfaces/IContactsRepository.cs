using NSI.DC.ContactsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IContactsRepository
    {
        ICollection<ContactDto> GetContacts(int? pageSize, int? pageNumber);
        ContactDto CreateContact(ContactDto contactDto);
        bool DeleteContactById(int contactId);
        ContactDto GetContactById(int contactId);
        bool EditContactById(int contactId, ContactDto contact);
    }
}
