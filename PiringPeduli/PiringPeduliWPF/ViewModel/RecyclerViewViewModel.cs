using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.ViewModel
{
    public class RecyclerViewViewModel: ViewModelBase
    {
        public string _quantity;

        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(Quantity);
            }
        }
    }
}
