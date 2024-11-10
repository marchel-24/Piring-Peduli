using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Service
{
    public class SortedWasteService
    {
        private readonly SortedWasteRepository _sortedWasteRepository;

        public SortedWasteService (SortedWasteRepository sortedWasteRepository)
        {
            _sortedWasteRepository = sortedWasteRepository;
        }

        public void PublishSortedWaste(SortedType wasteType)
        {
            var SortedWaste = new SortedWaste
            {
                //Wasteid = wastedID,
                WasteType = wasteType
                //Quantity = quantity
            };

            _sortedWasteRepository.CreateSortedWaste(SortedWaste);
        }

        public SortedWaste GetWaste(int id)
        {
            return _sortedWasteRepository.GetSortedbyID(id);
        }

        public void UpdateSortedWaste(int wastedID, SortedType wasteType)
        {
            var SortedWaste = new SortedWaste
            {
                Wasteid = wastedID,
                WasteType = wasteType
                //Quantity = quantity
            };

            _sortedWasteRepository.UpdateSortedWaste(SortedWaste);
        }

        public void DeleteSortedWaste (int wasteID)
        {
            _sortedWasteRepository.DeleteSortedWaste(wasteID);
        }
    }
}
