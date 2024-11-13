using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;

namespace PiringPeduliClass.Service
{
    public class CustomerService : AccountService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository) : base(customerRepository)
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
        public async Task <bool>  UpdateCustomerAsync(string oldUsername, Customer updatedAccount)
        {
            try
            {
                // Attempt to get the account ID using the provided username
                var accountId = await _customerRepository.GetIdFromUsernameAsync(oldUsername);

                // Check if the account exists
                if (accountId != -1)
                {
                    updatedAccount.AccountId = accountId;
                    // Update the account with the new details
                    bool isUpdatedAccount = await _customerRepository.UpdateAccountAsync(updatedAccount);
                    bool isUpdatedCourier = await _customerRepository.UpdateCustomerAsync(updatedAccount);
                    if (!isUpdatedAccount || !isUpdatedCourier)
                    {
                        throw new Exception("Account update failed");
                    }
                    return true;
                }
                else
                {
                    throw new Exception($"Account with username '{oldUsername}' not found.");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
