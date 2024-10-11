using System;

namespace PiringPeduliClass.Model
{
    /// <summary>
    /// Represents a courier responsible for delivering orders.
    /// Inherits from the Account class.
    /// </summary>
    public class Courier : Account
    {
        private int courierId;
        private string vehicleType;
        private Order assignedOrder;

        /// <summary>
        /// Gets or sets the courier's ID.
        /// </summary>
        public int CourierId
        {
            get { return courierId; }
            set { courierId = value; }
        }

        /// <summary>
        /// Gets or sets the type of vehicle used by the courier.
        /// </summary>
        public string VehicleType
        {
            get { return vehicleType; }
            set { vehicleType = value; }
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
        public Order GetComingOrder()
        {
            // Create and return a sample order with predefined details
            Order order = new Order
            {
                Status = "done",
                OrderId = 1,
                Source = "Rumah",
                Destination = "kantor"
            };

            return order;
        }
    }
}
