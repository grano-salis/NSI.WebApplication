using IkarusEntities;
using NSI.DC.ContactsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public class ContactRepository
    {
        public static Contact MapToDbEntity(ContactDto contactDto)
        {
            return new Contact()
            {
                Contact1 = contactDto.Contact1,
                CreatedByUserId = contactDto.CreatedByUserId,
                CreatedDate = contactDto.CreatedDate,
                AddressId = contactDto.AddressId,
                Address = contactDto.Address != null ? AddressRepository.MapToDbEntity(contactDto.Address) : null,
                FirsttName = contactDto.FirsttName,
                IsDeleted = contactDto.IsDeleted,
                LastName = contactDto.LastName,
                ModifiedDate = contactDto.ModifiedDate,
                Phone = MapToPhonesDbEntity(contactDto.Phones),
                Email = MapToEmailsDbEntity(contactDto.Emails)
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
                Address = contact.Address != null ? AddressRepository.MapToDto(contact.Address) : null,
                FirsttName = contact.FirsttName,
                IsDeleted = contact.IsDeleted,
                LastName = contact.LastName,
                ModifiedDate = contact.ModifiedDate,
                Phones = MapToPhonesDto(contact.Phone),
                Emails = MapToEmailsDto(contact.Email),
            };
        }

        public static ICollection<PhoneDto> MapToPhonesDto(ICollection<Phone> phones)
        {
            var phonesDto = new List<PhoneDto>();
            foreach (var phone in phones)
            {
                var phoneDto = new PhoneDto()
                {
                    PhoneId = phone.PhoneId,
                    PhoneNumber = phone.PhoneNumber,
                    ContactId = phone.ContactId
                };
                phonesDto.Add(phoneDto);
            }
            return phonesDto;
        }

        public static ICollection<Phone> MapToPhonesDbEntity(ICollection<PhoneDto> phonesDto)
        {
            var phones = new List<Phone>();
            foreach (var phoneDto in phonesDto)
            {
                var phone = new Phone()
                {
                    PhoneId = phoneDto.PhoneId,
                    PhoneNumber = phoneDto.PhoneNumber,
                    ContactId = phoneDto.ContactId,
                    IsDeleted = false,
                };
                phones.Add(phone);
            }
            return phones;
        }

        public static ICollection<EmailDto> MapToEmailsDto(ICollection<Email> emails)
        {
            var emailsDto = new List<EmailDto>();
            foreach (var email in emails)
            {
                var emailDto = new EmailDto()
                {
                    EmailId = email.EmailId,
                    EmailAddress = email.EmailAddress,
                    ContactId = email.ContactId
                };
                emailsDto.Add(emailDto);
            }
            return emailsDto;
        }

        public static ICollection<Email> MapToEmailsDbEntity(ICollection<EmailDto> emailsDto)
        {
            var emails = new List<Email>();
            foreach (var emailDto in emailsDto)
            {
                var email = new Email()
                {
                    EmailId = emailDto.EmailId,
                    EmailAddress = emailDto.EmailAddress,
                    ContactId = emailDto.ContactId,
                    IsDeleted = false
                };
                emails.Add(email);
            }
            return emails;
        }

    }
}
