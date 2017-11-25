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
      

        public ICollection<DC.ContactsRepository.ContactDto> GetContacts()
        {
            return _contactsRepository.GetContacts();
        }
    }
}
