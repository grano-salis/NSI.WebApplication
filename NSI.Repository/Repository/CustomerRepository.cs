using IkarusEntities;
using NSI.DC.CustomersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NSI.Repository.Interfaces;
using NSI.DC.Exceptions;
using NSI.DC.Exceptions.Enums;
using System.Net;

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
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
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
                throw new NSIException(ex.Message, Level.Error, ErrorType.InvalidParameter);
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
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
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
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
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
                throw new NSIException(ex.InnerException.Message, Level.Error, ErrorType.InvalidParameter, HttpStatusCode.BadRequest);
            }
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
