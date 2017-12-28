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

                address.DateCreated = DateTime.Now;
                address.DateModified = DateTime.Now;

                _dbContext.Add(address);

                if (_dbContext.SaveChanges() != 0)
                {
                    return Mappers.AddressRepository.MapToDto(address);
                }
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
                var address = _dbContext.Address.FirstOrDefault(x => x.AddressId == addressId);

                if (address != null)
                {
                    return Mappers.AddressRepository.MapToDto(address);
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
                var addresss = _dbContext.Address;
                if (addresss != null)
                {
                    ICollection<AddressDto> addresssDto = new List<AddressDto>();
                    foreach (var item in addresss)
                    {
                        addresssDto.Add(Mappers.AddressRepository.MapToDto(item));
                    }
                    return addresssDto;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public bool DeleteAddressById(int addressId)
        {
            try
            {
                var address = _dbContext.Address.FirstOrDefault(x => x.AddressId == addressId);
                if (address != null)
                {
                    address.IsDeleted = true;
                    address.DateModified = DateTime.Now;

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
                var addressTmp = _dbContext.Address.FirstOrDefault(x => x.AddressId == addressId);

                if (addressTmp != null)
                {
                    addressTmp.Address1 = address.Address1 ?? addressTmp.Address1;
                    addressTmp.Address2 = address.Address2 ?? addressTmp.Address2;
                    addressTmp.City = address.City ?? addressTmp.City;
                    addressTmp.DateModified = DateTime.Now;
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