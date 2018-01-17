﻿using NSI.DC.ContactsRepository;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IContactsManipulation
    {
        PaggedContactDto GetContacts(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder, int caseId);
        IEnumerable<ContactDto> GetContactsForCase(int caseId);
        ContactDto CreateContact(ContactDto model, int caseId);
        ContactDto GetContactById(int contactId);
        bool DeleteContactById(int contactId);
        bool EditContact(int contactId, ContactDto contact);
        string ValidationContact(ContactDto contact);
    
    }
}
