using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;

namespace PiringPeduliClass.Service
{
    public class CourierService : AccountService
    {
        private readonly CourierRepository _courierRepository;

        public CourierService(CourierRepository courierRepository) : base(courierRepository)
        { 
            _courierRepository = courierRepository;
        }

        public void CreateCourier(string name, VehicleType vehicleType, int AccountID)
        {
            var courier = new Courier
            {
                //CourierId = courierId,
                Name = name,
                Vehicle = vehicleType,
                AccountId = AccountID
            };

            _courierRepository.AddCourier(courier);
        }

        public Courier GetCourierById(int courierId)
        {
            return _courierRepository.GetCourierById(courierId);
        }

        public async Task<bool> UpdateCourier(string oldUsername, Courier updatedAccount)
        {
            try
            {
                // Attempt to get the account ID using the provided username
                var accountId = await _courierRepository.GetIdFromUsernameAsync(oldUsername);

                // Check if the account exists
                if (accountId != -1)
                {
                    updatedAccount.AccountId = accountId;
                    // Update the account with the new details
                    bool isUpdatedAccount = await _courierRepository.UpdateAccountAsync(updatedAccount);
                    bool isUpdatedCourier = await _courierRepository.UpdateCourierAsync(updatedAccount);
                    if (!isUpdatedAccount || !isUpdatedCourier)
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

        public void DeleteCourier(int courierId)
        {
            _courierRepository.DeleteCourier(courierId);
        }

        public void AssignOrderToCourier(string username, Courier courier, Order order)
        {
            courier.AcceptOrder(order);
            UpdateCourier(username, courier); // Update courier after assigning order
        }

        public Courier GetCourierByName(string courierName)
        {
            return _courierRepository.GetCourierByName(courierName);
        }
    }
}
