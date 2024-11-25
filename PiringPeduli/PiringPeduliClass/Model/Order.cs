using System;

namespace PiringPeduliClass.Model
{

    public enum StatusType
    {
        Confirmed,
        Processing,
        Delivered
    }

    /// <summary>
    /// Enumeration for types of accounts
    /// </summary>
    public enum Sizes
    {
        Small,
        Medium,
        Large
    }

    /// <summary>
    /// Represents an order in the system.
    /// </summary>
    public class Order
    {
        private int orderId;
        private StatusType status;
        private Sizes size;
        private int sourceAccountId;
        private int destinationAccountId;
        private int? courierAccountId;
        private string description;
        private string sourceAddress;
        private string sourceName;
        private string destinationAddress;
        private string destinationName;
        private double quantity;

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
        public StatusType Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or sets the source location of the order.
        /// </summary>
        public int Source
        {
            get { return sourceAccountId; }
            set { sourceAccountId = value; }
        }

        /// <summary>
        /// Gets or sets the destination location of the order.
        /// </summary>
        public int Destination
        {
            get { return destinationAccountId; }
            set { destinationAccountId = value; }
        }

        public int? Courier
        {
            get { return courierAccountId; }
            set { courierAccountId = value; }
        }

        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Sizes Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the source location of the order.
        /// </summary>
        public string SourceAddress
        {
            get { return sourceAddress; }
            set { sourceAddress = value; }
        }

        public string SourceName
        {
            get { return sourceName; }
            set { sourceName = value; }
        }

        /// <summary>
        /// Gets or sets the destination location of the order.
        /// </summary>
        public string DestinationAddress
        {
            get { return destinationAddress; }
            set { destinationAddress = value; }
        }

        /// <summary>
        /// Gets or sets the destination location of the order.
        /// </summary>
        public string DestinationName
        {
            get { return destinationName; }
            set { destinationName = value; }
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
