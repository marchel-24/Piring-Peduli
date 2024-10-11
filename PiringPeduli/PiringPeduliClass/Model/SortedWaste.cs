namespace PiringPeduliClass.Model
{
    /// <summary>
    /// Represents a type of waste that has been sorted.
    /// </summary>
    public class SortedWaste
    {
        private string wasteType;
        private float quantity;

        /// <summary>
        /// Gets or sets the type of the sorted waste.
        /// </summary>
        public string WasteType
        {
            get { return wasteType; }
            set { wasteType = value; }
        }

        /// <summary>
        /// Gets or sets the quantity of the sorted waste.
        /// </summary>
        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Updates the quantity of the sorted waste.
        /// </summary>
        /// <param name="newQuantity">The new quantity to be set.</param>
        public void UpdateQuantity(float newQuantity)
        {
            // Logic for updating the quantity
            quantity = newQuantity;
        }
    }
}
