using NSI.DC.AddressRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IAddressTypeManipulation
    {
        AddressTypeDto GetAddressTypeById(int addressTypeId);
        AddressTypeDto CreateAddressType(AddressTypeDto addressTypeDto);
        ICollection<AddressTypeDto> GetAddressTypes();
        bool DeleteAddressTypeById(int addressTypeId);
        bool EditAddressType(int addressTypeId, AddressTypeDto addressType);
    }
}
