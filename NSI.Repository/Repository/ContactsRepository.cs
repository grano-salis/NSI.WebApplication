using IkarusEntities;
using NSI.DC.ContactsRepository;
using NSI.Repository.Interfaces;
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

        public ICollection<ContactDto> GetContacts()
        {
            try
            {
                var contacts = _dbContext.Contact.Where(x => x.IsDeleted == false);
                if (contacts != null)
                {
                    ICollection<ContactDto> contactDto = new List<ContactDto>();
                    foreach (var item in contacts)
                    {
                        contactDto.Add(Mappers.ContactRepository.MapToDto(item));
                    }
                    return contactDto;
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
                    contactTmp.Email = contact.Email;
                    contactTmp.FirsttName = contact.FirsttName;
                    contactTmp.LastName = contact.LastName;
                    contactTmp.Mobile = contact.Mobile;
                    contactTmp.AddressId = contact.AddressId;
                    contactTmp.ModifiedDate = DateTime.Now;
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
