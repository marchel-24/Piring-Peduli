using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System.Collections.Generic;

namespace PiringPeduliClass.Service
{
    public class RecyclerService
    {
        private readonly RecyclerRepository _recyclerRepository;

        public RecyclerService(RecyclerRepository recyclerRepository)
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
        public void UpdateRecyclerByName(string recyclerName, string recyclerAddress, int accountId)
        {
            var updatedRecycler = new Recycler
            {
                RecyclerName = recyclerName,
                RecyclerAddress = recyclerAddress,
                AccountId = accountId
            };

            _recyclerRepository.UpdateRecyclerByName(recyclerName, updatedRecycler);
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
