using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

// 10
namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;
        // 10 Dependency injection works because of the code in Bootstrapper.cs file 
        // 10 Setting up window and event managers and specifying to pass in matching class with "ViewModel" in name
        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            // 10 Caliburn Micro serves the matching view 
            ActivateItem(_loginVM);
        }

    }
}
