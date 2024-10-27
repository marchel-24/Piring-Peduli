using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Collections.Generic;
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

        public void RegisterNewAccount(string username, string password, string phoneNumber, AccountType type)
        {
            int newAccountId = _accountRepository.GetNextAccountId();

            var account = new Account
            {
                AccountId = newAccountId,
                Username = username,
                Password = password,
                PhoneNumber = phoneNumber,
                Type = type
            };

            _accountRepository.CreateAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.UpdateAccount(account);
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
