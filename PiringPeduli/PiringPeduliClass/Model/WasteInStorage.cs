    using System;

    namespace PiringPeduliClass.Model
    {
        public class WasteInStorage
        {
            private int wasteid;
            private int storageid;
            private string wasteType;
            private string storageName;
            private double quantity;
            private double selectQuantity;

            public int Wasteid
            {
                get { return wasteid; }
                set { wasteid = value; }
            }

            public int Storageid
            {
                get { return storageid; }
                set { storageid = value; }
            }

            public double Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            public double SelectQuantity
            {
                get { return selectQuantity; }
                set { selectQuantity = value; }
            }

        public string WasteType
            {
                get { return wasteType; }
                set { wasteType = value; }
            }

            public string StorageName
            {
                get { return storageName; }
                set { storageName = value; }
            }

        // Constructor to easily set values
        public WasteInStorage(int wasteId, int storageId, double quantity)
            {
                this.wasteid = wasteId;
                this.storageid = storageId;
                this.quantity = quantity;
            }

            public WasteInStorage() { }
        }
    }
