using System;

namespace PiringPeduliClass.Model
{
    public enum VehicleType
    {
        Motorcycle,
        Car,
        Truck
    }
    /// <summary>
    /// Represents a courier responsible for delivering orders.
    /// Inherits from the Account class.
    /// </summary>
    public class Courier : Account
    {
        private int courierId;
        private string name;
        private VehicleType vehicleType;
        private int accountID;
        private Order assignedOrder;

        /// <summary>
        /// Gets or sets the courier's ID.
        /// </summary>
        public int CourierId
        {
            get { return courierId; }
            set { courierId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the type of vehicle used by the courier.
        /// </summary>
        public VehicleType Vehicle
        {
            get { return vehicleType; }
            set { vehicleType = value; }
        }

        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        /// <summary>
        /// Gets or sets the currently assigned order for the courier.
        /// </summary>
        public Order AssignedOrder
        {
            get { return assignedOrder; }
            set { assignedOrder = value; }
        }

        /// <summary>
        /// Accepts an order and assigns it to the courier.
        /// </summary>
        /// <param name="order">The order to be accepted.</param>
        public void AcceptOrder(Order order)
        {
            // Logic for accepting the order
            assignedOrder = order;
        }

        /// <summary>
        /// Retrieves the details of the currently assigned order.
        /// </summary>
        /// <returns>The current assigned order with predefined details.</returns>
        //public Order GetComingOrder()
        //{
        //    // Create and return a sample order with predefined details
        //    Order order = new Order
        //    {
        //        Status = "done",
        //        OrderId = 1,
        //        Source = "Rumah",
        //        Destination = "kantor"
        //    };

        //    return order;
        //}

        /// <summary>
        /// Overrides the UpdateProfile method to update the courier's profile,
        /// including the vehicle type.
        /// </summary>
        /// <param name="newUsername">The new username to be set.</param>
        /// <param name="newPassword">The new password to be set.</param>
        /// <param name="newVehicleType">The new vehicle type for the courier.</param>
        //public override void UpdateProfile(string newUsername, string newPassword)
        //{
        //    // Logic for updating courier's profile
        //    base.UpdateProfile(newUsername, newPassword);
        //    vehicleType = "New Vehicle Type";
        //}

        /// <summary>
        /// Overloaded method to update profile including vehicle type.
        /// </summary>
        /// <param name="newUsername">The new username to be set.</param>
        /// <param name="newPassword">The new password to be set.</param>
        /// <param name="newVehicleType">The new vehicle type to be set.</param>
        //public void UpdateProfile(string newUsername, string newPassword, string newVehicleType)
        //{
        //    base.UpdateProfile(newUsername, newPassword);
        //    vehicleType = newVehicleType;
        //}
    }
}
