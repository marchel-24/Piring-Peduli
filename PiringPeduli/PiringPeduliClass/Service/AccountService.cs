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


        public async Task<bool> RegisterNewAccountAsync(string username, string password, AccountType type)
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
                    Type = type
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


        public void UpdateAccount(string oldUsername, Account updatedAccount)
        {
            var accountId = _accountRepository.GetIdFromUsername(oldUsername);
            if(accountId != -1)
            {
                updatedAccount.AccountId = accountId;
                _accountRepository.UpdateAccount(updatedAccount);
            }
        }

        public void RemoveAccountById(int accountId)
        {
            _accountRepository.DeleteAccountById(accountId);
        }

        public void RemoveAccountByUsername(string username)
        {
            _accountRepository.DeleteAccountByUsername(username);
        }

        public async Task<Account> Login(string username, string password)
        {
            try
            {
                // Step 1: Validate the username and password
                bool isValid = _accountRepository.ValidateAccount(username, password);

                Debug.Print(isValid.ToString());
                if (!isValid)
                {
                    Debug.Print("LSKJFD");
                    throw new Exception("Invalid username or password.");
                }

                Debug.Print("lsfjennsln");
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
