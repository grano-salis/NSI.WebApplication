using IkarusEntities;
using NSI.DC.AddressTypeRepository;
using System;

namespace NSI.Repository.Mappers
{
    public partial class AddressTypeRepository
    {
        public static AddressType MapToDbEntity(AddressTypeDto addressTypeDto)
        {
            if (addressTypeDto == null)
            {
                throw new ArgumentNullException(nameof(addressTypeDto), "AddressTypeDto argument is not provided!");
            }

            return new AddressType()
            {
                AddressTypeId = addressTypeDto.AddressTypeId,
                AddressTypeName = addressTypeDto.AddressTypeName,
                IsDeleted = addressTypeDto.IsDeleted,
                CustomerId = addressTypeDto.CustomerId,
                ModifiedDate = addressTypeDto.ModifiedDate
            };
        }

        public static AddressTypeDto MapToDto(AddressType addressType)
        {
            if (addressType == null)
            {
                throw new ArgumentNullException(nameof(addressType), "AddressType argument is not provided!");
            }

            return new AddressTypeDto()
            {
                AddressTypeId = addressType.AddressTypeId,
                AddressTypeName = addressType.AddressTypeName,
                IsDeleted = addressType.IsDeleted,
                CustomerId = addressType.CustomerId,
                ModifiedDate = addressType.ModifiedDate,
                CreatedDate = addressType.CreatedDate
            };
        }
    }
}