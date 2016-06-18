using Microsoft.Extensions.DependencyInjection;
using Okra.Builder;
using Okra.DependencyInjection;
using Okra.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Okra
{
    public class OkraApplication : Application
    {
        // *** Fields ***

        private IAppContainer _rootAppContainer;
        private AppLaunchDelegate _appLaunch;

        // *** Constructors ***

        public OkraApplication(IOkraBootstrapper bootstrapper)
        {
            if (bootstrapper == null)
                throw new ArgumentNullException(nameof(bootstrapper));

            this.Resuming += OnResuming;
            this.Suspending += OnSuspending;
            
            bootstrapper.Initialize();

            var appContainerFactory = bootstrapper.ApplicationServices.GetRequiredService<IAppContainerFactory>();
            _rootAppContainer = appContainerFactory.CreateAppContainer();

            var appBuilder = bootstrapper.ApplicationServices.GetRequiredService<IOkraAppBuilder>();
            _appLaunch = appBuilder.Build();
        }

        // *** Overriden Base Methods ***

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            Activate(args);
        }

        protected override void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args)
        {
            base.OnCachedFileUpdaterActivated(args);
            Activate(args);
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            base.OnFileActivated(args);
            Activate(args);
        }

        protected override void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args)
        {
            base.OnFileOpenPickerActivated(args);
            Activate(args);
        }

        protected override void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args)
        {
            base.OnFileSavePickerActivated(args);
            Activate(args);
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            base.OnShareTargetActivated(args);
            Activate(args);
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            base.OnSearchActivated(args);
            Activate(args);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            Activate(args);
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferal = e.SuspendingOperation.GetDeferral();
            await _rootAppContainer.Deactivate();
            deferal.Complete();
        }

        private async void OnResuming(object sender, object e)
        {
            await _rootAppContainer.Activate();
        }

        // *** Private Methods ***

        private async void Activate(IActivatedEventArgs args)
        {
            var appLaunchRequest = new UniversalAppLaunchRequest(args);
            var appLaunchContext = new UniversalAppLaunchContext(_rootAppContainer.Services, appLaunchRequest);
            await _appLaunch(appLaunchContext);
        }
    }
}