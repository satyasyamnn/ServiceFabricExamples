using System;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System.Fabric.Description;
using System.Fabric;
using Microsoft.Owin.Hosting;

namespace WebCalculatorService
{
    public class OwinCommunicationListener : ICommunicationListener
    {
        private IOwinAppBuilder _startup;
        private IDisposable _serverHandler;
        private string _listeningAddress;
        private readonly ServiceContext _serviceInitializationParameters;

        public OwinCommunicationListener(IOwinAppBuilder startup, ServiceContext serviceInitializationParameters)
        {
            _startup = startup;
            _serviceInitializationParameters = serviceInitializationParameters;
        }

        public void Abort()
        {
            StopWebServer();
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            StopWebServer();
            return Task.FromResult(true);

        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            EndpointResourceDescription serviceEndpoint = _serviceInitializationParameters.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = serviceEndpoint.Port;
            _listeningAddress = string.Format("http://+:{0}", port);
            _serverHandler = WebApp.Start(_listeningAddress, appBuilder => _startup.Configuration(appBuilder));
            string resultAddress = _listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);
            ServiceEventSource.Current.Message("Listening on {0}", resultAddress);
            return Task.FromResult(resultAddress);
        }

        private void StopWebServer()
        {
            if(_serverHandler != null)
            {
                try
                {
                    _serverHandler.Dispose();
                }
                catch(ObjectDisposedException)
                {

                }
            }
        }
    }
}
