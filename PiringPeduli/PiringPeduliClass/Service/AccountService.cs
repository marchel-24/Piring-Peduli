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
            // Add any business logic here if needed
            return _accountRepository.GetAccountById(id);
        }
    }
}
