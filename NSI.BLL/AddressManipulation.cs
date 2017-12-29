using System;
using System.Collections.Generic;
using System.Text;
using NSI.Repository;
using NSI.BLL.Interfaces;
using NSI.Repository.Interfaces;
using NSI.DC.AddressRepository;

namespace NSI.BLL
{
    public class AddressManipulation : IAddressManipulation
    {
        private readonly IAddressRepository _addressRepository;

        public AddressManipulation(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public bool DeleteAddressById(int addressId)
        {
            return _addressRepository.DeleteAddressById(addressId);
        }

        public bool EditAddress(int addressId, AddressDto address)
        {
            return _addressRepository.EditAddress(addressId, address);
        }

        public AddressDto GetAddressById(int addressId)
        {
            return _addressRepository.GetAddressById(addressId);
        }

        public ICollection<AddressDto> GetAddreses()
        {
            return _addressRepository.GetAddreses();
        }

        public AddressDto CreateAddress(AddressDto addressDto)
        {
            return _addressRepository.CreateAddress(addressDto);
        }
    }
}
