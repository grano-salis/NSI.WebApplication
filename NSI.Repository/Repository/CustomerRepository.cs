using IkarusEntities;
using NSI.DC.CustomersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NSI.Repository.Interfaces;
using NSI.DC.Exceptions;
using NSI.DC.Exceptions.Enums;
using Npgsql;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace NSI.Repository
{
    public partial class CustomerRepository : ICustomerRepository
    {
        private readonly IkarusContext _dbContext;

        public Exception NSIExcepiton { get; private set; }

        public CustomerRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = Mappers.CustomerRepository.MapToDbEntity(customerDto);
                customer.CustomerId = null;
                customer.DateCreated = DateTime.Now;
                customer.DateModified = DateTime.Now;

                _dbContext.Add(customer);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.CustomerRepository.MapToDto(customer);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        if (postgresException.SqlState == "23503")
                            throw new NSIException("Foreign key error: " + postgresException.Detail, Level.Error, ErrorType.InvalidParameter);
                        if (postgresException.SqlState == "23505")
                            throw new NSIException("Primary key error: " + postgresException.Detail, Level.Error, ErrorType.InvalidParameter);
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
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
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
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
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
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
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
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

        public bool EditCustomer(int id, CustomerDto customerDto)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == customerDto.CustomerId);
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
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
            }
        }

        public ICollection<CustomerDto> SearchCustomer(CustomerSearchDto searchCustomer)
        {
            try
            {
                var customer = _dbContext.Customer.Where(x => searchQuery(x, searchCustomer));
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
                if (ex.InnerException.GetType().ToString() == "Npgsql.PostgresException")
                {
                        PostgresException postgresException = (PostgresException)ex.InnerException;
                        throw new NSIException("Db error: " + postgresException.Detail, Level.Error, ErrorType.DBError);
                } else {
                    throw ex.InnerException;
                }
            }
            return null;
        }

        public bool searchQuery(Customer customerDto, CustomerSearchDto customerSearchDto)
        {
            return (customerDto.CustomerName.Contains(customerSearchDto.CustomerName) || String.IsNullOrWhiteSpace(customerSearchDto.CustomerName)) &&
                    (customerDto.DateCreated >= customerSearchDto.FromCreated || customerSearchDto.FromCreated.Equals(null)) &&
                    (customerDto.DateCreated <= customerSearchDto.ToCreated || customerSearchDto.ToCreated.Equals(null)) &&
                    (customerDto.DateModified >= customerSearchDto.FromModified || customerSearchDto.FromModified.Equals(null)) &&
                    (customerDto.DateModified <= customerSearchDto.ToModified || customerSearchDto.FromModified.Equals(null)) &&
                    (customerDto.AddressId == customerSearchDto.AddressId || customerSearchDto.AddressId.Equals(null)) &&
                    (customerDto.PricingPackageId == customerSearchDto.PricingPackageId || customerSearchDto.PricingPackageId.Equals(null));
        }

        public ICollection<CustomerReportDto> GetCustomerClients()
        {
            try
            {
                var customer = _dbContext.Customer;
                if (customer != null)
                {
                    ICollection<CustomerReportDto> customerDto = new List<CustomerReportDto>();
                    foreach (var item in customer)
                    {
                        var clientNumber = _dbContext.Client.Where(x => x.CustomerId == item.CustomerId).Count();
                        CustomerReportDto cust = new CustomerReportDto();
                        cust.CustomerId = item.CustomerId;
                        cust.CustomerName = item.CustomerName;
                        cust.NumberOfClient = clientNumber;

                        customerDto.Add(cust);
                    }
                    return customerDto;
                }
            }
            catch (Exception ex)
            {
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
            }
            return null;
        }

        public CustomerReportDto GetCustomerCasesYearly(int CustomerId)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x=>x.CustomerId == CustomerId);
                if (customer != null)
                {
                    CustomerReportDto customerDto = new CustomerReportDto();
                    var clientNumber = _dbContext.Client.Where(x => x.CustomerId == CustomerId).Count();
                    customerDto.CustomerId = customer.CustomerId;
                    customerDto.CustomerName = customer.CustomerName;
                    customerDto.NumberOfClient = clientNumber;

                    var cases = _dbContext.CaseInfo.Where(x => x.CustomerId == CustomerId).ToList();
                    IEnumerable<CaseInfo> filteredList = cases.GroupBy(CaseInfo => CaseInfo.DateCreated.Year)
                        .Select(group => group.First());
                    ICollection<CustomerCasesDto> customerCaseDto = new List<CustomerCasesDto>();
                    foreach(var ca in filteredList){
                        CustomerCasesDto custCase = new CustomerCasesDto();
                        custCase.YearOfCases = ca.DateCreated.Year;
                        custCase.NumberOfCases = _dbContext.CaseInfo.Where(x => x.CustomerId == CustomerId && x.DateCreated.Year == ca.DateCreated.Year).Count();

                        customerCaseDto.Add(custCase);
                    }
                    customerDto.Cases = customerCaseDto;

                    return customerDto;
                }
            }
            catch (Exception ex)
            {
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
            }
            return null;
        }

        public CustomerReportDto GetCustomerCasesMonthly(int CustomerId, int Year)
        {
            try
            {
                var customer = _dbContext.Customer.FirstOrDefault(x => x.CustomerId == CustomerId);
                if (customer != null && Year > 0 && Year <9999)
                {
                    CustomerReportDto customerDto = new CustomerReportDto();
                    var clientNumber = _dbContext.Client.Where(x => x.CustomerId == CustomerId).Count();
                    customerDto.CustomerId = customer.CustomerId;
                    customerDto.CustomerName = customer.CustomerName;
                    customerDto.NumberOfClient = clientNumber;

                    var cases = _dbContext.CaseInfo.Where(x => x.CustomerId == CustomerId && x.DateCreated.Year == Year).ToList();
                    IEnumerable<CaseInfo> filteredList = cases.GroupBy(CaseInfo => CaseInfo.DateCreated.Month)
                        .Select(group => group.First());
                    ICollection<CustomerCasesDto> customerCaseDto = new List<CustomerCasesDto>();
                    foreach (var ca in filteredList)
                    {
                        CustomerCasesDto custCase = new CustomerCasesDto();
                        custCase.MonthOfCases = ca.DateCreated.Month;
                        custCase.NumberOfCases = _dbContext.CaseInfo.Where(x => x.CustomerId == CustomerId && x.DateCreated.Year == Year && x.DateCreated.Month == ca.DateCreated.Month).Count();

                        customerCaseDto.Add(custCase);
                    }
                    customerDto.Cases = customerCaseDto;

                    return customerDto;
                }
            }
            catch (Exception ex)
            {
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter,HttpStatusCode.BadRequest);
            }
            return null;
        }
    }
}
