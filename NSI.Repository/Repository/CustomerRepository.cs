﻿using IkarusEntities;
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
                customerDto.DateCreated=DateTime.Now;
                var customer = Mappers.CustomerRepository.MapToDbEntity(customerDto);
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

        public bool EditCustomer(CustomerDto customer)
        {
            try
            {
                var customerTmp = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (customerTmp != null)
                {
                    customerTmp.CustomerName = customer.CustomerName ?? customerTmp.CustomerName;
                    customerTmp.DateModified = DateTime.Now;
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

        public ICollection<CustomerDto> SearchCustomer(CustomerSearchDto searchCustomer){
            try
            {
                var customer = _dbContext.Customer.Where(x => searchQuery(x,searchCustomer));
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

        public bool searchQuery(Customer customerDto,CustomerSearchDto customerSearchDto){
            return (customerDto.CustomerName.Contains(customerSearchDto.CustomerName) || String.IsNullOrWhiteSpace(customerSearchDto.CustomerName)) &&
                    (customerDto.DateCreated >= customerSearchDto.FromCreated || customerSearchDto.FromCreated.Equals(null)) &&
                    (customerDto.DateCreated <= customerSearchDto.ToCreated || customerSearchDto.ToCreated.Equals(null)) &&
                    (customerDto.DateModified >= customerSearchDto.FromModified || customerSearchDto.FromModified.Equals(null)) &&
                    (customerDto.DateModified <= customerSearchDto.ToModified || customerSearchDto.FromModified.Equals(null)) &&
                    (customerDto.AddressId == customerSearchDto.AddressId || customerSearchDto.AddressId.Equals(null)) &&
                    (customerDto.PricingPackageId == customerSearchDto.PricingPackageId || customerSearchDto.PricingPackageId.Equals(null));
        }
    }
}
