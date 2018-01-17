using NSI.DC.CustomersRepository;
using System;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerDto CreateCustomer(CustomerDto customerDto);
        ICollection<CustomerDto> SearchCustomer(CustomerDto searchCriteria);
        CustomerDto GetCustomerById(int customerId);
        ICollection<CustomerDto> GetCustomers();
        bool DeleteCustomerById(int customerId);
        bool EditCustomer(int id, CustomerDto customerDto);
        ICollection<CustomerDto> SearchCustomer(CustomerSearchDto searchCustomer);
        ICollection<CustomerReportDto> GetCustomerClients();
        CustomerReportDto GetCustomerCasesYearly(int CustomerId);
        CustomerReportDto GetCustomerCasesMonthly(int CustomerId, int Year);
    }
}
