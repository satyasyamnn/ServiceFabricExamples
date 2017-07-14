using System;
using System.Threading.Tasks;
using CalculatorService;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;

namespace CalculatorClient
{
    public class Proxy : ServicePartitionClient<WcfCommunicationClient<ICalculatorService>>, ICalculatorService
    {
        public Proxy(WcfCommunicationClientFactory<ICalculatorService> clientFactory,
             Uri serviceName)
               : base(clientFactory, serviceName)
        {
        }
        public Task<int> Add(int a, int b)
        {
            return this.InvokeWithRetryAsync(client => client.Channel.Add(a, b));
        }

        public Task<int> Substract(int a, int b)
        {
            return this.InvokeWithRetryAsync(client => client.Channel.Substract(a, b));
        }
    }
}

