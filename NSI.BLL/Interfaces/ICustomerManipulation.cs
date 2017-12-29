using NSI.DC.CustomersRepository;
using System;
using System.Collections.Generic;

namespace NSI.BLL.Interfaces
{
    public interface ICustomerManipulation
    {
        CustomerDto GetCustomerById(int customerId);
        CustomerDto CreateCustomer(CustomerDto customer);
        ICollection<CustomerDto> GetCustomers();
        bool DeleteCustomerById(int customerId);
        bool EditCustomer(int customerId, CustomerDto customer);
    }
}
