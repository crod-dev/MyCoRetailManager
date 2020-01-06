using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;

// 10
namespace TRMDesktopUI.ViewModels
{
    // 16 Add IHandle interface inheritance; steps for handling events
    // 1. Specify which type of event will be handled (LogOnEvent)
    // 2. Implement how the event will be handled
    // 3. Add Event Aggregator property to subscribe as a listener
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        // 16 private event property to subscribe as listener
        private IEventAggregator _events;
        // 16
        private SalesViewModel _salesVM;
        // 16
        private SimpleContainer _container;
        // 10 Dependency injection works because of the code in Bootstrapper.cs file 
        // 10 Setting up window and event managers and specifying to pass in matching class with "ViewModel" in name
        // 16 Pass in IEventAggregator parameter to register as a listener
        // 16 Adding SimpleContainer type to be able to request new instances whenever we want
        // 16 Removed loginVM so nothing is stored, instead a new instance is created with every request
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
        {
            // 16
            _events = events;
            // 16
            _salesVM = salesVM;
            _container = container;
            // 16 subscribe as listener
            _events.Subscribe(this);
            // 10 Caliburn Micro serves the matching view 
            // 16 Changed from ActivateItem(_loginVM); to the following so login view is blank everytime
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }
        // 16 Implement the newly inherited IHandle interface method
        // 16 Must register as an event listener before this will work
        public void Handle(LogOnEvent message)
        {
            // 16 See mousehover message on Conductor class that is being inherited
            // Activating new form closes out the previous form (does not destroy it, instance still alive with all data still in tact)
            ActivateItem(_salesVM);
            // 16 Wipe out old LoginVM with a new blank instantiated one
            // 16 Removed this line and everything related to _loginVM
            // 16 _loginVM = _container.GetInstance<LoginViewModel>();
        }
    }
}
