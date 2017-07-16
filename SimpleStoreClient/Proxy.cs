using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using Microsoft.ServiceFabric.Services.Client;

namespace SimpleStoreClient
{
    public class Proxy : ServicePartitionClient<WcfCommunicationClient<IShoppingCartService>>, IShoppingCartService
    {
        //public Proxy(WcfCommunicationClientFactory<IShoppingCartService> clientFactory, Uri serviceName) : base(clientFactory, serviceName, ServicePartitionKey.Singleton)
        public Proxy(WcfCommunicationClientFactory<IShoppingCartService> clientFactory, Uri serviceName, int partitionId) : base(clientFactory, serviceName, new ServicePartitionKey((long)partitionId))
        {

        }

        public Task AddItem(ShoppingCartItem item)
        {
            return InvokeWithRetryAsync(client => client.Channel.AddItem(item));
        }

        public Task DeleteItem(ShoppingCartItem item)
        {
            return InvokeWithRetryAsync(client => client.Channel.DeleteItem(item));
        }

        public Task<IList<ShoppingCartItem>> GetItems()
        {
            return InvokeWithRetryAsync(client => client.Channel.GetItems());
        }
    }
}
