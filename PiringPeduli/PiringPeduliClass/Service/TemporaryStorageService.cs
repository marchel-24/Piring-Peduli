using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Diagnostics;

namespace PiringPeduliClass.Service
{
    public class TemporaryStorageService:AccountService
    {
        private readonly TemporaryStorageRepository _temporaryStorageRepository;

        public TemporaryStorageService(TemporaryStorageRepository temporaryStorageRepository) : base(temporaryStorageRepository)
        {
            _temporaryStorageRepository = temporaryStorageRepository;
        }

        // Method to create a new temporary storage account
        public void CreateTemporaryStorage(string storageAddress, int accountId)
        {
            var temporaryStorage = new TemporaryStorage
            {
                //StorageId = storageId,
                StorageAddress = storageAddress,
                AccountId = accountId
            };

            _temporaryStorageRepository.makeAccount(temporaryStorage);
        }

        public async Task<bool> UpdateTempStorage(string oldUsername, TemporaryStorage updatedAccount)
        {
            try
            {
                var accountId = await _temporaryStorageRepository.GetIdFromUsernameAsync(oldUsername);

                if (accountId != -1)
                {
                    updatedAccount.AccountId = accountId;
                    bool isUpdatedAccount = await _temporaryStorageRepository.UpdateAccountAsync(updatedAccount);
                    bool isUpdatedTemporaryStorage = await _temporaryStorageRepository.UpdateTemporaryStorageAsync(updatedAccount);

                    Debug.Print(isUpdatedAccount.ToString());
                    Debug.Print(isUpdatedTemporaryStorage.ToString());
                    if (!isUpdatedAccount || !isUpdatedTemporaryStorage)
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

    }
}
