namespace PiringPeduliClass.Model
{

    public enum SortedType
    {
        Liquid,
        Leaves,
        Meat,
        Others
    }
    /// <summary>
    /// Represents a type of waste that has been sorted.
    /// </summary>
    public class SortedWaste
    {
        private int wasteid;
        private SortedType wasteType;
        private double quantity;

        public int Wasteid
        {
            get { return wasteid; }
            set { wasteid = value; }
        }

        /// <summary>
        /// Gets or sets the type of the sorted waste.
        /// </summary>
        public SortedType WasteType
        {
            get { return wasteType; }
            set { wasteType = value; }
        }

        /// <summary>
        /// Gets or sets the quantity of the sorted waste.
        /// </summary>
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Updates the quantity of the sorted waste.
        /// </summary>
        /// <param name="newQuantity">The new quantity to be set.</param>
        public void UpdateQuantity(double newQuantity)
        {
            // Logic for updating the quantity
            quantity = newQuantity;
        }
    }
}
