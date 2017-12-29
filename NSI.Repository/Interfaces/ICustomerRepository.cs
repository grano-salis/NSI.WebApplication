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
        bool EditCustomer(int customerId, CustomerDto customerDto);
    }
}
