using Okra.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.Lifetime
{
    internal class UniversalAppLaunchContext : AppLaunchContext
    {
        public UniversalAppLaunchContext(IServiceProvider services, IAppLaunchRequest launchRequest)
        {
            this.LaunchRequest = launchRequest;
            this.Services = services;
        }

        public override IAppLaunchRequest LaunchRequest
        {
            get;
        }

        public override IServiceProvider Services
        {
            get;
            set;
        }
    }
}
