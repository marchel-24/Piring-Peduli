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

        public async Task<bool> CreateCourierAsync(Courier accountmade, string username)
        {
            try
            {
                var accountID = await _courierRepository.GetIdFromUsernameAsync(username);
                var courier = new Courier
                {
                    Name = accountmade.Name,
                    Vehicle = accountmade.Vehicle,
                    AccountId = accountID
                };

                bool isCreated = await _courierRepository.AddCourierAsync(courier);

                if (!isCreated)
                {
                    throw new Exception("Courier creation Failed");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
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

        public async Task<bool> DeleteCourier(string username)
        {
            try
            {
                var accountid = await _courierRepository.GetIdFromUsernameAsync(username);

                bool isDeleteCourier = await _courierRepository.DeleteCourierByAccountID(accountid);
                bool isDeleteAccount = await _courierRepository.DeleteAccountById(accountid);

                if (!isDeleteAccount || !isDeleteCourier)
                {
                    throw new Exception("Delete Account Failed");
                }
                return true;
            }catch (Exception ex) { throw new Exception( ex.Message); }
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
