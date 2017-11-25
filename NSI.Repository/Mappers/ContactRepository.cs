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
                Email = contactDto.Email,
                FirsttName = contactDto.FirsttName,
                IsDeleted = contactDto.IsDeleted,
                LastName = contactDto.LastName,
                Mobile = contactDto.Mobile,
                ModifiedDate = contactDto.ModifiedDate,
                Phone = contactDto.Phone
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
                Email = contact.Email,
                FirsttName = contact.FirsttName,
                IsDeleted = contact.IsDeleted,
                LastName = contact.LastName,
                Mobile = contact.Mobile,
                ModifiedDate = contact.ModifiedDate,
                Phone = contact.Phone
            };
        }

    }
}
