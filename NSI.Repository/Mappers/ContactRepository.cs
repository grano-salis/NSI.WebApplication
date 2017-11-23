using IkarusEntities;
using NSI.DC.ContactRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public partial class ContactRepository
    {
        public static Contact MapToDbEntity(ContactDto contactDto)
        {
            return new Contact()
            {
                Contact1 = contactDto.Contact1,
                FirsttName = contactDto.FirsttName,
                LastName = contactDto.LastName,
                AddressId = contactDto.AddressId,
                CreatedDate = contactDto.CreatedDate,
                ModifiedDate = contactDto.ModifiedDate,
                IsDeleted = contactDto.IsDeleted,
                CreatedByUserId = contactDto.CreatedByUserId,
                Address = contactDto.Address,
                CreatedByUser = contactDto.CreatedByUser,
                CaseContact = contactDto.CaseContact,
                ClientContact = contactDto.ClientContact,
                Email = contactDto.Email,
                Phone = contactDto.Phone
            };
        }

        public static ContactDto MapToDto(Contact contact)
        {
            return new ContactDto()
            {
                Contact1 = contact.Contact1,
                FirsttName = contact.FirsttName,
                LastName = contact.LastName,
                AddressId = contact.AddressId,
                CreatedDate = contact.CreatedDate,
                ModifiedDate = contact.ModifiedDate,
                IsDeleted = contact.IsDeleted,
                CreatedByUserId = contact.CreatedByUserId,
                Address = contact.Address,
                CreatedByUser = contact.CreatedByUser,
                CaseContact = contact.CaseContact,
                ClientContact = contact.ClientContact,
                Email = contact.Email,
                Phone = contact.Phone,
            };
        }
    }
}
