using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System.Collections.Generic;

namespace PiringPeduliClass.Service
{
    public class RecyclerService:AccountService
    {
        private readonly RecyclerRepository _recyclerRepository;

        public RecyclerService(RecyclerRepository recyclerRepository) : base(recyclerRepository) 
        {
            _recyclerRepository = recyclerRepository;
        }

        // Method to create a new recycler
        public void CreateRecycler(int recyclerId, string recyclerName, string recyclerAddress, int accountId)
        {
            var recycler = new Recycler
            {
                RecyclerId = recyclerId,
                RecyclerName = recyclerName,
                RecyclerAddress = recyclerAddress,
                AccountId = accountId
            };

            _recyclerRepository.AddRecycler(recycler);
        }

        // Method to update an existing recycler by name
        public async Task<bool> UpdateRecycler(string oldUsername, Recycler updatedAccount)
        {
            try
            {
                var accountId = await _recyclerRepository.GetIdFromUsernameAsync(oldUsername);

                if (accountId != -1)
                {
                    updatedAccount.AccountId = accountId;
                    bool isUpdatedAccount = await _recyclerRepository.UpdateAccountAsync(updatedAccount);
                    bool isUpdatedRecycler = await _recyclerRepository.UpdateRecyclerAsync(updatedAccount);

                    if (!isUpdatedAccount || !isUpdatedRecycler)
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

        // Method to delete a recycler by name
        public void DeleteRecyclerByName(string recyclerName)
        {
            _recyclerRepository.DeleteRecyclerByName(recyclerName);
        }

        // Method to retrieve a recycler by name
        public Recycler GetRecyclerByName(string recyclerName)
        {
            return _recyclerRepository.GetRecyclerByName(recyclerName);
        }
    }
}
