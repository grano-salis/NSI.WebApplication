using NSI.DC.AddressRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IAddressManipulation
    {
        AddressDto GetAddressById(int addressId);
        AddressDto CreateAddress(AddressDto addressDto);
        ICollection<AddressDto> GetAddreses();
        bool DeleteAddressById(int addressId);
        bool EditAddress(int addressId, AddressDto address);
    }
}
