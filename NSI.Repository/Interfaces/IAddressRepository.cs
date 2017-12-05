using NSI.DC.AddressRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IAddressRepository
    {
        AddressDto CreateAddress(AddressDto addressDto);
        ICollection<AddressDto> SearchAddreses(AddressDto searchCriteria);
        AddressDto GetAddressById(int addressId);
        ICollection<AddressDto> GetAddreses();
        bool DeleteAddressById(int addressId);
        bool EditAddress(int addressId, AddressDto address);
    }
}
