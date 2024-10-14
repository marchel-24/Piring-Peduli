using System;

namespace PiringPeduliClass.Model
{
    /// <summary>
    /// Enumeration for types of accounts
    /// </summary>
    public enum AccountType
    {
        Customer,
        Courier,
        Recycler,
        TemporaryStorage
    }

    /// <summary>
    /// Represents a user account with basic authentication details.
    /// </summary>
    /// 
    public class Account
    {
        private int accountId;
        private string username;
        private string password;
        private AccountType type;

        /// <summary>
        /// Gets or sets the account's ID.
        /// </summary>
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        /// <summary>
        /// Gets or sets the username for the account.
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Gets or sets the password for the account.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets for account type
        /// </summary>
        public AccountType Type
        {
            get { return type; }
            set { type = value;  }
        }

        /// <summary>
        /// Updates the profile with a new username and password.
        /// </summary>
        /// <param name="newUsername">The new username to be set.</param>
        /// <param name="newPassword">The new password to be set.</param>
        public void UpdateProfile(string newUsername, string newPassword)
        {
            // Logic for updating profile
            username = newUsername;
            password = newPassword;
        }
    }
}
