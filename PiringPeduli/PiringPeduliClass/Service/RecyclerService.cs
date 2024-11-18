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
        public async Task<bool> CreateRecyclerAsync(Recycler accountmade, string username)
        {
            try
            {
                var accountid = await _recyclerRepository.GetIdFromUsernameAsync(username);
                var recycler = new Recycler
                {
                    RecyclerName = accountmade.RecyclerName,
                    RecyclerAddress = accountmade.RecyclerAddress,
                    AccountId = accountid
                };

                bool isCreated = await _recyclerRepository.AddRecyclerAsync(recycler);

                if (!isCreated)
                {
                    throw new Exception("Recycler creation failed");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public async Task<bool> DeleteRecycler(string username)
        {
            try
            {
                var accountid = await _recyclerRepository.GetIdFromUsernameAsync(username);

                bool isDeleteRecycler = await _recyclerRepository.DeleteRecyclerByAccountId(accountid);
                bool isDeleteAccount = await _recyclerRepository.DeleteAccountById(accountid);

                if (!isDeleteAccount || !isDeleteRecycler)
                {
                    throw new Exception("Account Delete Fail");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Method to retrieve a recycler by name
        public Recycler GetRecyclerByName(string recyclerName)
        {
            return _recyclerRepository.GetRecyclerByName(recyclerName);
        }
    }
}
