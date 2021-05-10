using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using PVcase.Services;
using PVcase.ViewModels;

namespace PVcase
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();

            _container.PerRequest<ShellViewModel, ShellViewModel>();
            _container.PerRequest<FileReader>();
            _container.PerRequest<PanelCalculations>();
            _container.PerRequest<ZoneCalculations>();

            base.Configure();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
    }
}
