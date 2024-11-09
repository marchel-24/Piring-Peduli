using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;

namespace PiringPeduliClass.Service
{
    public class CourierService
    {
        private readonly CourierRepository _courierRepository;

        public CourierService(CourierRepository courierRepository)
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

        public void UpdateCourier(int courierId, string name, VehicleType vehicleType)
        {
            var courier = new Courier
            {
                CourierId = courierId,
                Name = name,
                Vehicle = vehicleType
            };

            _courierRepository.UpdateCourier(courier);
        }

        public void DeleteCourier(int courierId)
        {
            _courierRepository.DeleteCourier(courierId);
        }

        public void AssignOrderToCourier(Courier courier, Order order)
        {
            courier.AcceptOrder(order);
            UpdateCourier(courier.CourierId, courier.Name, courier.Vehicle); // Update courier after assigning order
        }

        public Courier GetCourierByName(string courierName)
        {
            return _courierRepository.GetCourierByName(courierName);
        }
    }
}
