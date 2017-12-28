using IkarusEntities;
using NSI.DC.ContactsRepository;
using NSI.Repository.Interfaces;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSI.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IkarusContext _dbContext;

        public ContactsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PaggedContactDto GetContacts(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder)
        {
            try
            {
                IQueryable<Contact> contacts = _dbContext.Contact.Where(a => (bool)!a.IsDeleted);
                if (!String.IsNullOrEmpty((string)searchString))
                {
                    switch ((string)searchColumn)
                    {
                        case "email":
                            contacts = contacts.Where(s => s.Email.Any(email => email.EmailAddress.ToUpper().Contains((string)searchString.ToUpper())));
                            break;
                        case "phone":
                            contacts = contacts.Where(s => s.Phone.Any(phone => phone.PhoneNumber.ToUpper().Contains((string)searchString.ToUpper())));
                            break;
                        default:
                            contacts = contacts.Where(s => (s.FirsttName + " "+  s.LastName).ToUpper().Contains((string)searchString.ToUpper())); ;
                            break;
                    }
                }
                var sortingOrder = String.IsNullOrEmpty((string)sortOrder) ? "date_modified" : (string)sortOrder; // date_modified will be our default sort order
                switch (sortingOrder)
                {
                    case "name_asc":
                        contacts = contacts.OrderBy(a => a.FirsttName + " " + a.LastName);
                        break;
                    case "name_desc":
                        contacts = contacts.OrderByDescending(a => a.FirsttName + " " + a.LastName);
                        break;
                    case "phone_asc":
                        contacts = contacts.OrderBy(a => a.Phone.FirstOrDefault().PhoneNumber);
                        break;
                    case "phone_desc":
                        contacts = contacts.OrderByDescending(a => a.Phone.FirstOrDefault().PhoneNumber);
                        break;
                    case "email_asc":
                        contacts = contacts.OrderBy(a => a.Email.FirstOrDefault().EmailAddress);
                        break;
                    case "email_desc":
                        contacts = contacts.OrderByDescending(a => a.Email.FirstOrDefault().EmailAddress);
                        break;
                    default:
                        contacts = contacts.OrderByDescending(a => a.ModifiedDate);
                        break;
                }
                if (contacts != null)
                {
                    var total = contacts.Count();
                    var contactsList = contacts.ToPagedList(pageNumber, pageSize).ToList();
                    ICollection<ContactDto> contactDto = new List<ContactDto>();
                    foreach (var item in contactsList)
                    {
                        _dbContext.Entry(item).Collection(p => p.Phone).Load();
                        _dbContext.Entry(item).Collection(p => p.Email).Load();
                        _dbContext.Entry(item).Reference(p => p.Address).Load();
                        contactDto.Add(Mappers.ContactRepository.MapToDto(item));
                    }
                    var paggedContactsDto = new PaggedContactDto() { Contacts = contactDto, Total = total };
                    return paggedContactsDto;
                }

            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception(ex.Message);
            }
            return null;
        }

        public ContactDto CreateContact(ContactDto contactDto)
        {
            try
            {
                var contact = Mappers.ContactRepository.MapToDbEntity(contactDto);
                contact.ModifiedDate = contact.CreatedDate = DateTime.Now;
                contact.IsDeleted = false;
                _dbContext.Add(contact);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.ContactRepository.MapToDto(contact);
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception(ex.Message);
            }
            return null;
        }

        public bool DeleteContactById(int contactId)
        {
            try
            {
                var contact = _dbContext.Contact.FirstOrDefault(x => x.Contact1 == contactId);
                if (contact != null)
                {
                    contact.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception(ex.Message);
            }
        }

        public ContactDto GetContactById(int contactId)
        {
            try
            {
                var contact = _dbContext.Contact.FirstOrDefault(x => x.Contact1 == contactId && x.IsDeleted == false);   
                if (contact != null)
                {
                    _dbContext.Entry(contact).Collection(p => p.Phone).Load();
                    _dbContext.Entry(contact).Collection(p => p.Email).Load();
                    _dbContext.Entry(contact).Reference(p => p.Address).Load();
                    return Mappers.ContactRepository.MapToDto(contact);
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception(ex.Message);
            }
            return null;
        }

        public bool EditContactById(int contactId, ContactDto contact)
        {
            try
            {
                var contactTmp = _dbContext.Contact.FirstOrDefault(x => x.Contact1 == contactId);
                if (contactTmp != null)
                {
                    contactTmp.FirsttName = contact.FirsttName;
                    contactTmp.LastName = contact.LastName;
                    contactTmp.AddressId = contact.AddressId;
                    contactTmp.ModifiedDate = DateTime.Now;
                    contactTmp.AddressId = contact.AddressId;
                    //delete all phones and emails
                    _dbContext.RemoveRange(_dbContext.Phone.Where(x => x.ContactId == contactTmp.Contact1));
                    _dbContext.RemoveRange(_dbContext.Email.Where(x => x.ContactId == contactTmp.Contact1));
                    //create new phones and emails
                    foreach (var emailDto in contact.Emails)
                    {
                        _dbContext.Add(new Email() { ContactId = contactTmp.Contact1, EmailAddress = emailDto.EmailAddress });
                    }
                    foreach (var phoneDto in contact.Phones)
                    {
                        _dbContext.Add(new Phone() { ContactId = contactTmp.Contact1, PhoneNumber = phoneDto.PhoneNumber });
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception(ex.Message);
            }
        }
    }
}
