using System;

namespace PiringPeduliClass
{
    /// <summary>
    /// Represents an order in the system.
    /// </summary>
    public class Order
    {
        private int orderId;
        private string status;
        private string source;
        private string destination;

        /// <summary>
        /// Gets or sets the order's ID.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or sets the source location of the order.
        /// </summary>
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// Gets or sets the destination location of the order.
        /// </summary>
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        /// <summary>
        /// Creates a new order with the specified details.
        /// </summary>
        public void CreateOrder()
        {
            // Logic for creating an order
        }
    }
}
