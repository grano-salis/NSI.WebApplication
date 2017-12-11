using IkarusEntities;
using NSI.DC.AddressRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSI.Repository
{
    public partial class AddressRepository : IAddressRepository
    {
        private readonly IkarusContext _dbContext;

        public AddressRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddressDto CreateAddress(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "AddresDto argument is not provided!");
            }

            try
            {
                var address = Mappers.AddressRepository.MapToDbEntity(addressDto);
                _dbContext.Add(address);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AddressRepository.MapToDto(address);
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }

            return null;
        }

        public AddressDto GetAddressById(int addressId)
        {
            try
            {
                var Address = _dbContext.Address.FirstOrDefault(x => x.AddressId == addressId);
                if (Address != null)
                {
                    return Mappers.AddressRepository.MapToDto(Address);
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
            return null;
        }

        public ICollection<AddressDto> GetAddreses()
        {
            try
            {
                var Addresss = _dbContext.Address;
                if (Addresss != null)
                {
                    ICollection<AddressDto> AddresssDto = new List<AddressDto>();
                    foreach (var item in Addresss)
                    {
                        AddresssDto.Add(Mappers.AddressRepository.MapToDto(item));
                    }
                    return AddresssDto;
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
            return null;
        }

        public bool DeleteAddressById(int AddressId)
        {
            try
            {
                var Address = _dbContext.Address.FirstOrDefault(x => x.AddressId == AddressId);
                if (Address != null)
                {
                    if (_dbContext.Address.Remove(Address) != null)
                    {
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
        }

        public ICollection<AddressDto> SearchAddreses(AddressDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria), "Argument SearchCriteria is not provided!");
            }

            return null;
        }

        public bool EditAddress(int addressId, AddressDto address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address argument is not provided!");
            }

            try
            {
                var AddressTmp = _dbContext.Address.FirstOrDefault(x => x.AddressId == addressId);
                if (AddressTmp != null)
                {
                    AddressTmp.Address1 = address.Address1 ?? AddressTmp.Address1;
                    AddressTmp.Address2 = address.Address2 ?? AddressTmp.Address2;
                    AddressTmp.City = address.City ?? AddressTmp.City;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
        }
    }
}