using IkarusEntities;
using NSI.DC.ContactsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository
{
    public class ContactsRepository: IContactsRepository
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
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("Database error!");
            }
            return null;
        }
    }
}
