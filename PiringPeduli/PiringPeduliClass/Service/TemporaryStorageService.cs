using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;

namespace PiringPeduliClass.Service
{
    public class TemporaryStorageService
    {
        private readonly TemporaryStorageRepository _temporaryStorageRepository;

        public TemporaryStorageService(TemporaryStorageRepository temporaryStorageRepository)
        {
            _temporaryStorageRepository = temporaryStorageRepository;
        }

        // Method to create a new temporary storage account
        public void CreateTemporaryStorage(int storageId, string storageAddress, int accountId)
        {
            var temporaryStorage = new TemporaryStorage
            {
                StorageId = storageId,
                StorageAddress = storageAddress,
                AccountId = accountId
            };

            _temporaryStorageRepository.makeAccount(temporaryStorage);
        }

        // Additional methods for temporary storage management can be added here, if needed
        // Example: Update, Delete, or Get storage by ID or Address
    }
}
