using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;

namespace PiringPeduliClass.Service
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Method to create a new customer
        public void CreateCustomer(int customerId, string customerName, string customerInstance, string customerAddress, int accountId)
        {
            var customer = new Customer
            {
                CustomerId = customerId,
                CustomerName = customerName,
                CustomerInstance = customerInstance,
                CustomerAddress = customerAddress,
                AccountId = accountId
            };

            _customerRepository.AddCustomer(customer);
        }

        // Method to update an existing customer by name
        public void UpdateCustomerByName(string customerName, string customerInstance, string customerAddress, int accountId)
        {
            var updatedCustomer = new Customer
            {
                CustomerName = customerName,
                CustomerInstance = customerInstance,
                CustomerAddress = customerAddress,
                AccountId = accountId
            };

            _customerRepository.UpdateCustomerByName(customerName, updatedCustomer);
        }

        // Method to delete a customer by name
        public void DeleteCustomerByName(string customerName)
        {
            _customerRepository.DeleteCustomerByName(customerName);
        }

        // Method to retrieve a customer by name
        public Customer GetCustomerByName(string customerName)
        {
            return _customerRepository.GetCustomerByName(customerName);
        }
    }
}
