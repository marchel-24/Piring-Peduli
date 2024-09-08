using System;

namespace PiringPeduliClass
{
    /// <summary>
    /// Represents a customer who can request waste pick up.
    /// Inherits from the Account class.
    /// </summary>
    public class Customer : Account
    {
        private int customerId;
        private string customerName;
        private string customerInstance;
        private string customerAddress;

        /// <summary>
        /// Gets or sets the customer's ID.
        /// </summary>
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        /// <summary>
        /// Gets or sets the customer's instance (could be a unique identifier or type).
        /// </summary>
        public string CustomerInstance
        {
            get { return customerInstance; }
            set { customerInstance = value; }
        }

        /// <summary>
        /// Gets or sets the customer's address.
        /// </summary>
        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        /// <summary>
        /// Requests a waste pick up for the customer.
        /// </summary>
        public void RequestWastePickUp()
        {
            // Logic for requesting waste pick up
        }
    }
}
