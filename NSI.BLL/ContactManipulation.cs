using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.ContactRepository;
using NSI.Repository.Interfaces;
using NSI.Repository;
using NSI.BLL.Interfaces;

namespace NSI.BLL
{
    public class ContactManipulation : IContactManipulation
    {
        ContactRepository _contactRepository;

        public ContactManipulation()
        {
            _contactRepository = new ContactRepository(new IkarusEntities.IkarusContext());
        }

        public ContactManipulation (ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        //
        public ContactDto CreateContact(ContactDto contactDto)
        {
            return _contactRepository.CreateContact(contactDto);
        }

        public bool DeleteContactById(int contactId)
        {
            return _contactRepository.DeleteContactById(contactId);
        }

        public ContactDto GetContactById(int contactId)
        {
            return _contactRepository.GetContactById(contactId);
        }

        public ICollection<ContactDto> GetContacts()
        {
            return _contactRepository.GetContacts();
        }

    }
}
