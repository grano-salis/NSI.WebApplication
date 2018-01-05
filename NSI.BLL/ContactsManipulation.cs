using NSI.DC.ContactsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL
{
    public class ContactsManipulation : Interfaces.IContactsManipulation
    {
        IContactsRepository _contactsRepository;


        public ContactsManipulation(IContactsRepository contactRepository)
        {
            _contactsRepository = contactRepository;
        }

           

        public PaggedContactDto GetContacts(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder, int caseId)
        {
            return _contactsRepository.GetContacts(pageSize, pageNumber, searchString, searchColumn, sortOrder, caseId);
        }

        public IEnumerable<ContactDto> GetContactsForCase(int caseId)
        {
            return _contactsRepository.GetContactsForCase(caseId);
        }

        public ContactDto CreateContact(ContactDto contactDto, int caseId)
        {
            return _contactsRepository.CreateContact(contactDto, caseId);
        }

        public bool DeleteContactById(int contactId)
        {
            return _contactsRepository.DeleteContactById(contactId);
        }

        public ContactDto GetContactById(int contactId)
        {
            return _contactsRepository.GetContactById(contactId);
        }

        public bool EditContact(int contactId, ContactDto contact)
        {
            return _contactsRepository.EditContactById(contactId, contact);
        }

     
    }
}
