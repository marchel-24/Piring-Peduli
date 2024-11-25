using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Service
{
    public class WasteService
    {
        private readonly SortedWasteRepository _sortedWasteRepository;
        private readonly WasteInStorageRepository _wasteInStorageRepository;

        public WasteService (SortedWasteRepository sortedWasteRepository, WasteInStorageRepository wasteInStorageRepository)
        {
            _sortedWasteRepository = sortedWasteRepository;
            _wasteInStorageRepository = wasteInStorageRepository;
        }

        public void AddWaste(int storageId, string wasteType, double quantity)
        {
            try
            {
                SortedWaste? sortedWaste = _sortedWasteRepository.GetSortedByType(wasteType);
                // Add new sorted waste if new
                if (sortedWaste == null)
                {
                    var newSortedWaste = new SortedWaste
                    {
                        WasteType = wasteType
                    };

                    _sortedWasteRepository.CreateSortedWaste(newSortedWaste);
                }
                sortedWaste = _sortedWasteRepository.GetSortedByType(wasteType);
                WasteInStorage? wasteInStorage = _wasteInStorageRepository.GetWasteByStorageAndWaste(storageId, sortedWaste.Wasteid);

                var waste = new WasteInStorage
                {
                    Storageid = storageId,
                    Wasteid = sortedWaste.Wasteid,
                    Quantity = quantity
                };

                // update existing quantity if found
                if (wasteInStorage != null)
                {
                    throw new Exception("Please update, not create new");
                }
                else
                {
                    _wasteInStorageRepository.AddWasteToStorage(waste);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<WasteInStorage> GetWaste(int storageId)
        {
            try
            {
                List<WasteInStorage> wastes = _wasteInStorageRepository.GetWasteByStorage(storageId);
                return wastes;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WasteInStorage> GetAllWaste()
        {
            try
            {
                List<WasteInStorage> wastes = _wasteInStorageRepository.GetAllWaste();
                return wastes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateWaste(int storageid, string type, double quantity)
        {
            int wasteId = _sortedWasteRepository.GetSortedByType(type).Wasteid;
            WasteInStorage newWaste = new WasteInStorage
            {
                Storageid = storageid,
                Wasteid = wasteId,
                Quantity = quantity
            };
            _wasteInStorageRepository.UpdateWasteInStorage(newWaste);
        }

        public void DeleteWaste(int storageId, string type)
        {
            int wasteId = _sortedWasteRepository.GetSortedByType(type).Wasteid;
            _wasteInStorageRepository.DeleteWasteByStorageAndWaste(storageId, wasteId);
        }

        public List<string> GetAllWasteType()
        {
            List<string>  wasteTypes = _sortedWasteRepository.GetAllWasteType();
            return wasteTypes;
        }
    }
}
