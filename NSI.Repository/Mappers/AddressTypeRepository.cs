using IkarusEntities;
using NSI.DC.AddressTypeRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public partial class AddressTypeRepository
    {
        public static AddressType MapToDbEntity(AddressTypeDto addressTypeDto)
        {
            return new AddressType()
            {
                AddressTypeId = addressTypeDto.AddressTypeId,
                AddressTypeName = addressTypeDto.AddressTypeName,
                IsDeleted = addressTypeDto.IsDeleted,
                CustomerId = addressTypeDto.CustomerId
    };
        }

        public static AddressTypeDto MapToDto(AddressType addressType)
        {
            return new AddressTypeDto()
            {
                AddressTypeId = addressType.AddressTypeId,
                AddressTypeName = addressType.AddressTypeName,
                IsDeleted = addressType.IsDeleted,
                CustomerId = addressType.CustomerId
            };
        }
    }
}
