using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IContactsManipulation
    {
        ICollection<DC.ContactsRepository.ContactDto> GetContacts();
    }
}
