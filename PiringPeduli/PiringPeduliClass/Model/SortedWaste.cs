namespace PiringPeduliClass.Model
{
    /// <summary>
    /// Represents a type of waste that has been sorted.
    /// </summary>
    public class SortedWaste
    {
        private int wasteid;
        private string wasteType;

        public int Wasteid
        {
            get { return wasteid; }
            set { wasteid = value; }
        }

        /// <summary>
        /// Gets or sets the type of the sorted waste.
        /// </summary>
        public string WasteType
        {
            get { return wasteType; }
            set { wasteType = value; }
        }
    }
}
