using IkarusEntities;
using NSI.DC.CustomersRepository;
using System;
namespace NSI.Repository.Mappers
{
    public partial class CustomerRepository
    {
        public static Customer MapToDbEntity(CustomerDto customerDto)
        {
            return new Customer()
            {
                CustomerId = customerDto.CustomerId,
                CustomerName = customerDto.CustomerName,
                IsActive = customerDto.IsActive,
                DateCreated = customerDto.DateCreated,
                DateModified = customerDto.DateModified,
                IsDeleted = customerDto.IsDeleted,
                PricingPackageId = customerDto.PricingPackageId,
                AddressId = customerDto.AddressId
            };
        }

        public static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                IsActive = customer.IsActive,
                DateCreated = customer.DateCreated,
                DateModified = customer.DateModified,
                IsDeleted = customer.IsDeleted,
                PricingPackageId = customer.PricingPackageId,
                AddressId = customer.AddressId
            };
        }
    }
}
