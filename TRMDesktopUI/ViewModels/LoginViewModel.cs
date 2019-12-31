using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;

// 10 Handle the events of the LogInView
namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel: Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;

        // 11 Use dependency injection to create an apiHelper (HttpClient wrapper) to call the login service
        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #region properties

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        public bool CanLogIn
        {
            get
            {
                bool login = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                    login = true;
                return login;
            }
        } 
        #endregion

        public async Task LogIn()
        {
            try
            {
                var result = await _apiHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
