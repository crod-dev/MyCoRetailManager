using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        // 07 Caliburn Micro (nuget package previously installed for MVVC) contains a SimpleContainer class to help
        // with dependency injection; Instantiate SimpleContainer object to handle dependency injection 
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }
        // 07 Where the actual instantiation happens; tells the container what to connect to what
        protected override void Configure()
        {
            _container.Instance(_container);
            // 07 Some Caliburn Micro features, WindowManager handles bringing windows in and out
            // EventAggregator ties application together, passes event messaging throughout app. One section can raise an event
            // and another can listen and handle it; ties interface to implementation
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();
            // 07 Use reflection to tie ViewModels to Views, this code only runs once on startup to performance impact is minimal
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }
        // 05 Specifies ShellViewModel to launch on startup as base view; VewModel launches the View
        // 05 Remove start up from App.xaml file
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        // 07 Pass in a type and name and will return a container
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        // 07 Retern all instances of objects of certain type using the container
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        // 07 This is where the container builds the objects
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
