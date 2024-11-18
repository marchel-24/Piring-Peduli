using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Service
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository userRepository)
        {
            _accountRepository = userRepository;
        }

        public Account GetUserById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public async Task<Account> GetUserByUsernameAsync(string username)
        {
            return await _accountRepository.GetAccountByUsernameAsync(username);
        }


        public async Task<bool> RegisterNewAccountAsync(string username, string password, AccountType type, double lat, double lon)
        {
            try
            {

                // Get the next Account ID asynchronously
                int newAccountId = await _accountRepository.GetNextAccountIdAsync();

                var account = new Account
                {
                    AccountId = newAccountId,
                    Username = username,
                    Password = password,
                    Type = type,
                    Lat = lat,
                    Lon = lon
                };

                // Create the account asynchronously
                bool isCreated = await _accountRepository.CreateAccountAsync(account);

                if (!isCreated)
                {
                    throw new Exception("Account creation failed.");
                }

                return true; // Successfully created the account
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                // Optionally, rethrow the exception or return false
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> UpdateAccountAsync(string oldUsername, Account updatedAccount)
        {
            try
            {
                // Attempt to get the account ID using the provided username
                var accountId = await _accountRepository.GetIdFromUsernameAsync(oldUsername);

                // Check if the account exists
                if (accountId != -1)
                {
                    updatedAccount.AccountId = accountId;
                    // Update the account with the new details
                    bool isUpdated = await _accountRepository.UpdateAccountAsync(updatedAccount);
                    if (!isUpdated)
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


        public void RemoveAccountById(int accountId)
        {
            _accountRepository.DeleteAccountById(accountId);
        }

        public async Task<bool> RemoveAccountByUsernameAsync(string username)
        {
            try
            {
                bool isDeleted = await _accountRepository.DeleteAccountByUsernameAsync(username);

                if ((!isDeleted))
                {
                    throw new Exception("Delete account failed");
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> Login(string username, string password)
        {
            try
            {
                // Step 1: Validate the username and password
                bool isValid = _accountRepository.ValidateAccount(username, password);

                if (!isValid)
                {
                    throw new Exception("Invalid username or password.");
                }
                // Step 2: Retrieve the account details asynchronously
                var account = await _accountRepository.GetAccountByUsernameAsync(username);

                if (account == null)
                {
                    throw new Exception("Account not found.");
                }

                // Step 3: Return the account for successful login
                return account;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception(ex.Message); // Rethrow as a custom exception if desired
            }
        }


    }
}
