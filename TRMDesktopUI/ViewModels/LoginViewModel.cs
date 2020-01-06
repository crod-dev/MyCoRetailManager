using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;
using MRMDesktopUI.Library;
using MRMDesktopUI.Library.Api;
using TRMDesktopUI.EventModels;

// 10 Handle the events of the LogInView
namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel: Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events; // 16 private field EventAggregator from Bootstrapper class

        // 11 Use dependency injection to create an apiHelper (HttpClient wrapper) to call the login service
        // 16 Pass in EventAggregator from Bootstrapper class
        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        #region properties

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                // NotifyOfPropertyChange is provided by Caliburn.Micro anounces state change
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
        // 12
        public bool IsErrorVisible 
        {
            get 
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                    output = true;
                return output;
                    
            }
        }

        // 12
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value; // 12 The order is important, change the value first then notify, caused bug
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        #endregion
        // 12 async so application doesn't lock up when calling api
        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);
                // 14 If authentication works, retreive information about the user via apiHelper
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
                // 16 PublichOnUIThread causes the event to be heard on the UI thread in case the 
                // processing falls on a background thread, everyone will heard the event (yes, we logged on event)
                _events.PublishOnUIThread(new LogOnEvent());

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
