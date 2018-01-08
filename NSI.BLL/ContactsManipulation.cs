using NSI.DC.ContactsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.ContactsRepository;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;
using NSI.DC.AddressRepository;
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
            
                if (this.ValidationContact(contactDto) != "")
                {
                    throw new Exception(this.ValidationContact(contactDto));
                }
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
            if (this.ValidationContact(contact) != "")
            {
                throw new Exception(this.ValidationContact(contact));
            }
            return _contactsRepository.EditContactById(contactId, contact);
        }

        public string ValidationContact(ContactDto contact)
        {
            String validationMessage = "";

            if (string.IsNullOrEmpty(contact.FirsttName)) validationMessage += " First name is required.";
            if (string.IsNullOrEmpty(contact.LastName)) validationMessage += " Last name is required.";
            if (contact.Phones.Any(x => String.IsNullOrEmpty(x.PhoneNumber))) validationMessage += " All phone number fields should have a value.";
            if (contact.Phones.Select(x => x.PhoneNumber).Distinct().Count() != contact.Phones.Count) validationMessage += " Phone number already exists or the same phone number value is entered several times.";
            if (contact.Emails.Any(x => String.IsNullOrEmpty(x.EmailAddress))) validationMessage += " All email fields should have a value.";
            if (contact.Emails.Select(x => x.EmailAddress).Distinct().Count() != contact.Emails.Count) validationMessage += " Email already exists or the same email address is enetered several times.";
            if (!contact.Emails.All(x => Regex.IsMatch(x.EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))) validationMessage += "\n Email is not in correct format. Example: someone@domain.com.";
            if (!Regex.IsMatch(contact.FirsttName, @"^[a-zA-Z]+$")) validationMessage += " First name should contain letters only.";
            if (!Regex.IsMatch(contact.LastName, @"^[a-zA-Z]+$")) validationMessage += " Last name should contain letters only.";

            foreach (string p in contact.Phones.Select(x => x.PhoneNumber))
            {
                Int64 result;
                if (!Int64.TryParse(p, out result)) validationMessage += " Phone number " + p + " should contain numbers only.";
            }

            return validationMessage;
        }


    }
}
