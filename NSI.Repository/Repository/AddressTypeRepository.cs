using IkarusEntities;
using NSI.DC.AddressTypeRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSI.Repository
{
    public partial class AddressTypeRepository : IAddressTypeRepository
    {
        private readonly IkarusContext _dbContext;

        public AddressTypeRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddressTypeDto CreateAddressType(AddressTypeDto addressTypeDto)
        {
            if (addressTypeDto == null)
            {
                throw new ArgumentNullException(nameof(addressTypeDto), "AddressTypeDto is not provided!");
            }

            try
            {
                var addressType = Mappers.AddressTypeRepository.MapToDbEntity(addressTypeDto);
                _dbContext.Add(addressType);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AddressTypeRepository.MapToDto(addressType);
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }

            return null;
        }

        public AddressTypeDto GetAddressTypeById(int addressTypeId)
        {
            try
            {
                var AddressType = _dbContext.AddressType.FirstOrDefault(x => x.AddressTypeId == addressTypeId);
                if (AddressType != null)
                {
                    return Mappers.AddressTypeRepository.MapToDto(AddressType);
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
            return null;
        }

        public ICollection<AddressTypeDto> GetAddressTypes()
        {
            try
            {
                var AddressType = _dbContext.AddressType;
                if (AddressType != null)
                {
                    ICollection<AddressTypeDto> AddressTypeDto = new List<AddressTypeDto>();
                    foreach (var item in AddressType)
                    {
                        AddressTypeDto.Add(Mappers.AddressTypeRepository.MapToDto(item));
                    }
                    return AddressTypeDto;
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
            return null;
        }

        public bool DeleteAddressTypeById(int AddressTypeId)
        {
            try
            {
                var AddressType = _dbContext.AddressType.FirstOrDefault(x => x.AddressTypeId == AddressTypeId);
                if (AddressType != null)
                {
                    if (_dbContext.AddressType.Remove(AddressType) != null)
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

        public ICollection<AddressTypeDto> SearchAddressTypes(AddressTypeDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria), "SearchCriteria argument is not provided!");
            }

            return null;
        }

        public bool EditAddressType(int AddressTypeId, AddressTypeDto AddressType)
        {
            if (AddressType == null)
            {
                throw new ArgumentNullException(nameof(AddressType), "AddressType argument is not provided!");
            }

            try
            {
                var AddressTypeTmp = _dbContext.AddressType.FirstOrDefault(x => x.AddressTypeId == AddressTypeId);
                if (AddressTypeTmp != null)
                {
                    AddressTypeTmp.AddressTypeName = AddressType.AddressTypeName ?? AddressTypeTmp.AddressTypeName;
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