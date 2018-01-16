using IkarusEntities;
using NSI.DC.AddressRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public static class AddressRepository
    {
        public static Address MapToDbEntity(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "AddressDto argument is not provided!");
            }

            return new Address()
            {
                AddressId = addressDto.AddressId,
                Address1 = addressDto.Address1,
                Address2 = addressDto.Address2,
                City = addressDto.City,
                ZipCode = addressDto.ZipCode,
                AddressTypeId = addressDto.AddressTypeId,
                CreatedByUserId = addressDto.CreatedByUserId
            };
        }

        public static AddressDto MapToDto(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address argument is not provided!");
            }

            return new AddressDto()
            {
                AddressId = address.AddressId,
                Address1 = address.Address1,
                Address2 = address.Address2,
                City = address.City,
                ZipCode = address.ZipCode,
                AddressTypeId = address.AddressTypeId,
                CreatedByUserId = address.CreatedByUserId,
                IsDeleted = address.IsDeleted ?? false,
                DateModified = address.DateModified
            };
        }
    }
}
