using NSI.DC.ContactsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IContactsRepository
    {
        PaggedContactDto GetContacts(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder, int caseId);
        IEnumerable<ContactDto> GetContactsForCase(int caseId);
        ContactDto CreateContact(ContactDto contactDto,int caseId);
        bool DeleteContactById(int contactId);
        ContactDto GetContactById(int contactId);
        bool EditContactById(int contactId, ContactDto contact);
    }
}
