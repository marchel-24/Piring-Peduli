using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass
{
    public class Recycler: Account
    {
        int recyclerId;
        string recyclerName;
        string recyclerAddress;
        List<SortedWaste> requiredWasteType;
    }
}
