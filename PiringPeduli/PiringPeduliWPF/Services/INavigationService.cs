using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.Services
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);
    }
}
