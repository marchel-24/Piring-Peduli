using System;
using System.Collections.Generic;

namespace PiringPeduliClass.Model
{
    /// <summary>
    /// Represents a temporary storage facility for sorted waste.
    /// Inherits from the Account class.
    /// </summary>
    public class TemporaryStorage : Account
    {
        private int storageId;
        private List<SortedWaste> storedWasteType;
        private string storageAddress;

        /// <summary>
        /// Gets or sets the storage ID for the temporary storage.
        /// </summary>
        public int StorageId
        {
            get { return storageId; }
            set { storageId = value; }
        }

        public string StorageAddress
        {
            get { return storageAddress; }
            set { storageAddress = value; }
        }

       

        /// <summary>
        /// Gets or sets the list of sorted waste types stored in the facility.
        /// </summary>
        public List<SortedWaste> StoredWasteType
        {
            get { return storedWasteType; }
            set { storedWasteType = value; }
        }

        /// <summary>
        /// Gets or sets the address of the temporary storage facility.
        /// </summary>

        /// <summary>
        /// Manages the sorted waste in the storage.
        /// </summary>
        public void SortWaste()
        {
            // Logic for handling sorted waste
        }

        /// <summary>
        /// Updates the sorted waste information in the storage.
        /// </summary>
        public void UpdateSortedWaste()
        {
            // Logic for updating sorted waste
        }
    }
}
