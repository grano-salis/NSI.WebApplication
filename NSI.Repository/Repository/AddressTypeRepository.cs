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
                addressType.CreatedDate = DateTime.Now;

                _dbContext.Add(addressType);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AddressTypeRepository.MapToDto(addressType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
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
                Console.WriteLine(ex.InnerException);
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
                Console.WriteLine(ex.InnerException);
                throw;
            }
            return new List<AddressTypeDto>();
        }

        public bool DeleteAddressTypeById(int addressTypeId)
        {
            try
            {
                var AddressType = _dbContext.AddressType.FirstOrDefault(x => x.AddressTypeId == addressTypeId);
                if (AddressType != null && _dbContext.AddressType.Remove(AddressType) != null)
                {
                        _dbContext.SaveChanges();
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
        }

        public ICollection<AddressTypeDto> SearchAddressTypes(AddressTypeDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria), "SearchCriteria argument is not provided!");
            }

            return new List<AddressTypeDto>();
        }

        public bool EditAddressType(int addressTypeId, AddressTypeDto addressType)
        {
            if (addressType == null)
            {
                throw new ArgumentNullException(nameof(addressType), "AddressType argument is not provided!");
            }

            try
            {
                var AddressTypeTmp = _dbContext.AddressType.FirstOrDefault(x => x.AddressTypeId == addressTypeId);
                if (AddressTypeTmp != null)
                {
                    AddressTypeTmp.AddressTypeName = addressType.AddressTypeName ?? AddressTypeTmp.AddressTypeName;
                    AddressTypeTmp.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
        }
    }
}