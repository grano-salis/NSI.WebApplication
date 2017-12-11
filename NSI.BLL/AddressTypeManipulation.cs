using System;
using System.Collections.Generic;
using System.Text;
using NSI.Repository;
using NSI.BLL.Interfaces;
using NSI.Repository.Interfaces;
using NSI.DC.AddressRepository;

namespace NSI.BLL
{
    public class AddressTypeManipulation : IAddressTypeManipulation
    {
        private readonly IAddressTypeRepository _addressTypeRepository;

        public AddressTypeManipulation(IAddressTypeRepository addressTypeRepository)
        {
            _addressTypeRepository = addressTypeRepository;
        }

        public bool DeleteAddressTypeById(int addressTypeId)
        {
            return _addressTypeRepository.DeleteAddressTypeById(addressTypeId);
        }

        public bool EditAddressType(int addressTypeId, AddressTypeDto addressType)
        {
            return _addressTypeRepository.EditAddressType(addressTypeId, addressType);
        }

        public AddressTypeDto GetAddressTypeById(int addressTypeId)
        {
            return _addressTypeRepository.GetAddressTypeById(addressTypeId);
        }

        public ICollection<AddressTypeDto> GetAddressTypes()
        {
            return _addressTypeRepository.GetAddressTypes();
        }

        public AddressTypeDto CreateAddressType(AddressTypeDto addressTypeDto)
        {
            return _addressTypeRepository.CreateAddressType(addressTypeDto);
        }
    }
}
