using NSI.Repository;
using NSI.BLL.Interfaces;
using NSI.Repository.Interfaces;
using NSI.DC.CustomersRepository;
using System.Collections.Generic;
using System;

namespace NSI.BLL
{
    public class CustomerManipulation : ICustomerManipulation
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManipulation(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool DeleteCustomerById(int addressTypeId)
        {
            return _customerRepository.DeleteCustomerById(addressTypeId);
        }

        public bool EditCustomer(int customerId, CustomerDto customer)
        {
            return _customerRepository.EditCustomer(customerId, customer);
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            return _customerRepository.GetCustomerById(customerId);
        }

        public ICollection<CustomerDto> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            return _customerRepository.CreateCustomer(customerDto);
        }
    }
}
