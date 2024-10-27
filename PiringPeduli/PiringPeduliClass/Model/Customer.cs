using System;

namespace PiringPeduliClass.Model
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

        /// <summary>
        /// Overloaded method to update profile including customer name, isntance, and address
        /// </summary>
        /// <param name="newUsername">The new username to be set.</param>
        /// <param name="newPassword">The new password to be set.</param>
        /// <param name="newCustomerName">The new customer name to be set.</param>
        /// <param name="newCustomerInstance">The new customer instance to be set.</param>
        /// <param name="newCustomerAddress">The new customer address to be set.</param>
        public void UpdateProfile(string newUsername, string newPassword, string newCustomerName, string newCustomerInstance, string newCustomerAddress)
        {
            base.UpdateProfile(newUsername, newPassword);
            customerAddress = newCustomerAddress;
            customerName = newCustomerName;
            customerInstance = newCustomerInstance;
        }
    }
}
