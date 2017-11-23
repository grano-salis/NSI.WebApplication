using NSI.DC.ContactRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IContactManipulation
    {
        ContactDto GetContactById(int contactId);
        ContactDto CreateContact(ContactDto contactDto);
        ICollection<ContactDto> GetContacts();
        bool DeleteContactById(int contactId);
    }
}
