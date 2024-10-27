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

        public void RegisterNewAccount(string username, string password, AccountType type)
        {
            int newAccountId = _accountRepository.GetNextAccountId();

            var account = new Account
            {
                AccountId = newAccountId,
                Username = username,
                Password = password,
                Type = type
            };

            _accountRepository.CreateAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.UpdateAccount(account);
        }

        public void RemoveAccount(int accountId)
        {
            _accountRepository.DeleteAccount(accountId);
        }
    }
}
