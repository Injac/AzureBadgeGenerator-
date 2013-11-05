using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Practices.Unity;
using BadgeService.Ioc;
using Microsoft.Owin.Hosting;



namespace BadgeService
{
    public class WorkerRole : RoleEntryPoint
    {
        private IDisposable _app = null;

        public override void Run()
        {

            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Http"];
            string baseUri = string.Format("{0}://{1}",
                endpoint.Protocol, endpoint.IPEndpoint);

            _app = WebApp.Start<Startup>(new StartOptions(url: baseUri));
            
            Trace.TraceInformation(baseUri, "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        public override void OnStop()
        {
            if (_app != null)
            {
                _app.Dispose();
            }

               base.OnStop();
        }
    }
}
