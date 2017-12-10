using IkarusEntities;
using NSI.DC.ContactsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    class ContactRepository
    {
        public static Contact MapToDbEntity(ContactDto contactDto)
        {
            return new Contact()
            {
                Contact1 = contactDto.Contact1,
                CreatedByUserId = contactDto.CreatedByUserId,
                CreatedDate = contactDto.CreatedDate,
                AddressId = contactDto.AddressId,
                FirsttName = contactDto.FirsttName,
                IsDeleted = contactDto.IsDeleted,
                LastName = contactDto.LastName,
                ModifiedDate = contactDto.ModifiedDate,
            };
        }

        public static ContactDto MapToDto(Contact contact)
        {
            return new ContactDto()
            {
                Contact1 = contact.Contact1,
                CreatedByUserId = contact.CreatedByUserId,
                CreatedDate = contact.CreatedDate,
                AddressId = contact.AddressId,
                FirsttName = contact.FirsttName,
                IsDeleted = contact.IsDeleted,
                LastName = contact.LastName,
                ModifiedDate = contact.ModifiedDate,
            };
        }

    }
}
