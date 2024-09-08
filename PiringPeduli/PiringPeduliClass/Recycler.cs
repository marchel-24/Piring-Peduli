using System;
using System.Collections.Generic;

namespace PiringPeduliClass
{
    /// <summary>
    /// Represents a recycler that can order and manage sorted waste.
    /// Inherits from the Account class.
    /// </summary>
    public class Recycler : Account
    {
        private int recyclerId;
        private string recyclerName;
        private string recyclerAddress;
        private List<SortedWaste> requiredWasteType;

        /// <summary>
        /// Gets or sets the recycler's ID.
        /// </summary>
        public int RecyclerId
        {
            get { return recyclerId; }
            set { recyclerId = value; }
        }

        /// <summary>
        /// Gets or sets the recycler's name.
        /// </summary>
        public string RecyclerName
        {
            get { return recyclerName; }
            set { recyclerName = value; }
        }

        /// <summary>
        /// Gets or sets the recycler's address.
        /// </summary>
        public string RecyclerAddress
        {
            get { return recyclerAddress; }
            set { recyclerAddress = value; }
        }

        /// <summary>
        /// Gets or sets the list of required sorted waste types.
        /// </summary>
        public List<SortedWaste> RequiredWasteType
        {
            get { return requiredWasteType; }
            set { requiredWasteType = value; }
        }

        /// <summary>
        /// Places an order for sorted waste.
        /// </summary>
        /// <param name="wasteList">The list of sorted waste to be ordered.</param>
        public void OrderSortedWaste(List<SortedWaste> wasteList)
        {
            // Logic for ordering sorted waste
        }

        /// <summary>
        /// Retrieves the available sorted waste from a temporary storage.
        /// </summary>
        /// <param name="temporaryStorage">The temporary storage from which sorted waste is retrieved.</param>
        /// <returns>A list of available sorted waste.</returns>
        public List<SortedWaste> GetAvailableWaste(TemporaryStorage temporaryStorage)
        {
            return temporaryStorage.StoredWasteType;
        }
    }
}
