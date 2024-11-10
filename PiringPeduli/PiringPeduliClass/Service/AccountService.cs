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

        public Account GetUserByUsername(string username)
        {
            return _accountRepository.GetAccountByUsername(username);
        }

        public async Task<bool> RegisterNewAccountAsync(string username, string password, string phoneNumber, AccountType type)
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

                return isCreated;
            }
            catch (Exception ex)
            {
                // Log exception or handle it accordingly
                // Optionally, log or rethrow the exception
                return false;
            }
        }


        public void UpdateAccount(Account account)
        {
            var accountId = _accountRepository.GetIdFromUsername(account.Username);
            Debug.Print(accountId.ToString());
            if(accountId != -1)
            {
                account.AccountId = accountId;
                _accountRepository.UpdateAccount(account);
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

        public Account Login(string username, string password, out string errorMessage)
        {
            // Step 1: Validate the username and password
            bool isValid = _accountRepository.ValidateAccount(username, password);

            if (!isValid)
            {
                errorMessage = "Invalid username or password.";
                return null;
            }

            // Step 2: Retrieve the account details
            var account = _accountRepository.GetAccountByUsername(username);

            if (account == null)
            {
                errorMessage = "Account not found.";
                return null;
            }

            // Step 3: Return the account and set errorMessage to null since it's a successful login
            errorMessage = null;
            return account;
        }
    }
}
