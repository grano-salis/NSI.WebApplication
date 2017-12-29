using NSI.DC.AddressTypeRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IAddressTypeRepository
    {
        AddressTypeDto CreateAddressType(AddressTypeDto addressTypeDto);
        ICollection<AddressTypeDto> SearchAddressTypes(AddressTypeDto searchCriteria);
        AddressTypeDto GetAddressTypeById(int addressTypeId);
        ICollection<AddressTypeDto> GetAddressTypes();
        bool DeleteAddressTypeById(int addressTypeId);
        bool EditAddressType(int addressTypeId, AddressTypeDto addressType);
    }
}
