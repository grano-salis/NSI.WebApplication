using IkarusEntities;
using NSI.DC.CustomersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NSI.Repository.Interfaces;

namespace NSI.Repository
{
    public partial class CustomerRepository : ICustomerRepository
    {
        private readonly IkarusContext _dbContext;

        public CustomerRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = Mappers.CustomerRepository.MapToDbEntity(customerDto);
                customer.DateCreated = DateTime.Now;
                customer.DateModified = null;
                _dbContext.Add(customer);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.CustomerRepository.MapToDto(customer);
            }
            catch (Exception ex)
            {
                //log ex
                throw ex.InnerException;
                //throw new Exception("Database error!");
            }
            return null;

        }

        public CustomerDto GetCustomerById(int customerId)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                if (customer != null)
                {
                    return Mappers.CustomerRepository.MapToDto(customer);
                }
            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new Exception("Database error!");
            }
            return null;
        }

        public ICollection<CustomerDto> GetCustomers()
        {
            try
            {
                var customer = _dbContext.Customer;
                if (customer != null)
                {
                    ICollection<CustomerDto> customerDto = new List<CustomerDto>();
                    foreach (var item in customer)
                    {
                        customerDto.Add(Mappers.CustomerRepository.MapToDto(item));
                    }
                    return customerDto;
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("Database error!");
            }
            return null;
        }

        public bool DeleteCustomerById(int customerId)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                if (customer != null)
                {
                    customer.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("Database error!");
            }
        }

        public ICollection<CustomerDto> SearchCustomer(CustomerDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }


            return null;

        }

        public bool EditCustomer(int customerId, CustomerDto customerDto)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                if (customer != null)
                {
                    customer.CustomerName = customerDto.CustomerName ?? customer.CustomerName;
                    customer.IsActive = customerDto.IsActive ?? customer.IsActive;
                    customer.IsDeleted = customerDto.IsDeleted ?? customer.IsDeleted;
                    customer.DateModified = customerDto.DateModified ?? DateTime.Now;
                    customer.PricingPackageId = customerDto.PricingPackageId ?? customer.PricingPackageId;
                    customer.AddressId = customerDto.AddressId ?? customer.AddressId;
                    _dbContext.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("Database error!");
            }
        }
    }
}
