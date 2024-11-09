using System;

namespace PiringPeduliClass.Model
{
    public class RequiredWaste
    {
        private int recyclerid;
        private int wasteid;

        public int RecyclerId
        {
            get { return recyclerid; }
            set { recyclerid = value; }
        }

        public int WasteId
        {
            get { return wasteid; }
            set { wasteid = value; }
        }
    }
}
