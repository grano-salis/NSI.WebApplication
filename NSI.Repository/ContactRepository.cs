using IkarusEntities;
using NSI.DC.ContactRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSI.Repository
{
    public partial class ContactRepository
    {
        private readonly IkarusContext _dbContext;

        public ContactRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ContactDto CreateContact(ContactDto contactDto)
        {
            var contact = Mappers.ContactRepository.MapToDbEntity(contactDto);
            _dbContext.Add(contact);
            if (_dbContext.SaveChanges() != 0)
                return Mappers.ContactRepository.MapToDto(contact);
            return null;

        }
        //Not tested
        public ContactDto GetContactById(int contactId)
        {
            var contact = _dbContext.Contact.FirstOrDefault(x => x.Contact1 == contactId);
            if (contact != null)
            {
                return Mappers.ContactRepository.MapToDto(contact);
            }
            return null;
        }
        //Not tested
        public ICollection<ContactDto> GetContacts()
        {
            var contacts = _dbContext.Contact;
            if (contacts != null)
            {
                ICollection<ContactDto> contactDto = new List<ContactDto>();
                foreach (var item in contacts)
                {
                    contactDto.Add(Mappers.ContactRepository.MapToDto(item));
                }
                return contactDto;
            }
            return null;
        }
        //Not tested
        public bool DeleteContactById(int contactId)
        {
            var contact = _dbContext.Contact.FirstOrDefault(x => x.Contact1 == contactId);
            if (contact != null)
            {
                if (_dbContext.Contact.Remove(contact) != null)
                {
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public ICollection<ContactDto> SearchContacts(ContactDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }


            return null;

        }
    }
}
