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
        public async Task<bool> CreateCustomerAsync(string username, Customer accountmade)
        {
            try
            {
                var accountid = await _customerRepository.GetIdFromUsernameAsync(username);

                var customer = new Customer
                {
                    CustomerName = accountmade.CustomerName,
                    CustomerInstance = accountmade.CustomerInstance,
                    CustomerAddress = accountmade.CustomerAddress,
                    AccountId = accountid
                };

                var iscreated = await _customerRepository.AddCustomerAsync(customer);

                if (!iscreated)
                {
                    throw new Exception("Courier creation Failed");
                }

                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
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
        public async Task<bool> DeleteCustomer(string username)
        {
            try
            {
                var accountid = await _customerRepository.GetIdFromUsernameAsync(username);

                bool isDeleteCustomer = await _customerRepository.DeleteCustomerByAccountID(accountid);
                bool isDeleteAccount = await _customerRepository.DeleteAccountById(accountid);

                if (!isDeleteAccount || !isDeleteCustomer)
                {
                    throw new Exception("Delete Account Failed");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Method to retrieve a customer by name
        public Customer GetCustomerByName(string customerName)
        {
            return _customerRepository.GetCustomerByName(customerName);
        }
    }
}
