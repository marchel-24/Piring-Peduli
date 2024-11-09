using System.Collections.Generic;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;

namespace PiringPeduliClass.Service
{
    public class RequiredWasteService
    {
        private readonly RequiredWasteRepository _repository;

        public RequiredWasteService(RequiredWasteRepository repository)
        {
            _repository = repository;
        }

        public void AddRequiredWaste(int recyclerId, int wasteId)
        {
            _repository.AddRequiredWaste(recyclerId, wasteId);
        }

        public void DeleteRequiredWaste(int recyclerId, int wasteId)
        {
            _repository.DeleteRequiredWaste(recyclerId, wasteId);
        }
    }
}
