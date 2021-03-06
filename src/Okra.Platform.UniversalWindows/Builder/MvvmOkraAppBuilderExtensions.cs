﻿using Okra.DependencyInjection;
using Okra.Lifetime;
using Okra.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Windows.ApplicationModel.Activation;
using Okra.Navigation;

namespace Okra.Builder
{
    public static class MvvmOkraAppBuilderExtensions
    {
        public static IOkraAppBuilder UseStartPage(this IOkraAppBuilder app, string pageName, object arguments, Type shellType)
        {
            return app.Use((context, next) =>
            {
                var request = context.LaunchRequest as UniversalAppLaunchRequest;

                if (request != null && request.EventArgs.Kind == ActivationKind.Launch)
                {
                    // TODO : Handle lifetime of app container?
                    var appContainerFactory = context.Services.GetRequiredService<IAppContainerFactory>();
                    var shellAppContainer = appContainerFactory.CreateAppContainer();
                    // TODO : Also restore navigation stack (or whole root app container?)
                    var appHost = shellAppContainer.Services.GetRequiredService<WindowAppHost>();
                    var appShell = shellAppContainer.Services.GetRequiredService(shellType);
                    appHost.SetShell(appShell);

                    var navigationManager = shellAppContainer.Services.GetRequiredService<INavigationManager>();
                    navigationManager.NavigateTo(new PageInfo(pageName, arguments));

                    return Task.FromResult<bool>(true);
                }
                else
                {
                    return next();
                }
            });
        }
    }
}
