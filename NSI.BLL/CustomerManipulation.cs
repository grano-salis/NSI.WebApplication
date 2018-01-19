using NSI.Repository;
using NSI.BLL.Interfaces;
using NSI.Repository.Interfaces;
using NSI.DC.CustomersRepository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NSI.BLL
{
    public class CustomerManipulation : ICustomerManipulation
    {
        private readonly ICustomerRepository _customerRepository;

        // private readonly IAddressManipulation _addressManipulation;
        // private readonly IPricingPackageManipulation _packageManipulation;

        public CustomerManipulation(
            ICustomerRepository customerRepository)
           // IAddressManipulation addressManipulation,
           // IPricingPackageManipulation packageManipulation
           
        {
            _customerRepository = customerRepository;
            //_addressManipulation = addressManipulation;
            //_packageManipulation = packageManipulation;
        }

        public bool DeleteCustomerById(int addressTypeId)
        {
            return _customerRepository.DeleteCustomerById(addressTypeId);
        }


        public bool EditCustomer(int id, CustomerDto customer)
        {
            return _customerRepository.EditCustomer(id, customer);
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            CustomerDto customerDto = _customerRepository.GetCustomerById(customerId);
            //return FillCustomerDtoWithOtherDtos(customerDto);
            return _customerRepository.GetCustomerById(customerId);
        }

        public ICollection<CustomerDto> GetCustomers()
        {

            ICollection<CustomerDto> customerDtoCollection = _customerRepository.GetCustomers().Where(x => x.IsDeleted == false).ToList();
            // customerDtoCollection.ToList().ForEach(x => {
            //     FillCustomerDtoWithOtherDtos(x);
            // });
            return customerDtoCollection;
        }

        public ICollection<CustomerDto> GetAllCustomers()
        {   
            ICollection<CustomerDto> customerDtoCollection = _customerRepository.GetCustomers();
            // customerDtoCollection.ToList().ForEach(x => {
            //     FillCustomerDtoWithOtherDtos(x);
            // });
            return customerDtoCollection;
        }

        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            return _customerRepository.CreateCustomer(customerDto);
        }

        public ICollection<CustomerDto> SearchCustomer(CustomerSearchDto customerSearch){
            
            return _customerRepository.SearchCustomer(customerSearch);
        }

        public ICollection<CustomerReportDto> GetCustomerClients()
        {
            return _customerRepository.GetCustomerClients();
        }

        public CustomerReportDto GetCustomerCasesYearly(int CustomerId)
        {
            return _customerRepository.GetCustomerCasesYearly(CustomerId);
        }

        public CustomerReportDto GetCustomerCasesMonthly(int CustomerId, int Year)
        {
            return _customerRepository.GetCustomerCasesMonthly(CustomerId,Year);
        }
    }
}
