using System.Collections.Generic;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;

namespace PiringPeduliClass.Service
{
    public class WasteInStorageService
    {
        private readonly WasteInStorageRepository _repository;

        public WasteInStorageService(WasteInStorageRepository repository)
        {
            _repository = repository;
        }

        public WasteInStorage GetWasteByStorage(int storageId)
        {
            return _repository.GetWasteByStorage(storageId);
        }

        public void AddWasteToStorage(int wasteId, int storageId, double quantity)
        {
            var wasteInStorage = new WasteInStorage(wasteId, storageId, quantity);
            _repository.AddWasteToStorage(wasteInStorage);
        }

        public void DeleteWasteByStorage(int storageId)
        {
            _repository.DeleteWasteByStorage(storageId);
        }
    }
}
