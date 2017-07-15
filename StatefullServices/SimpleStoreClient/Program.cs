using System;
using System.Fabric;
using System.ServiceModel;

using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;

using Common;
using System.Collections.Generic;

namespace SimpleStoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri serviceName = new Uri("fabric:/SimpleStoreApplication/ShoppingCartService");
            ServicePartitionResolver serviceResolver = new ServicePartitionResolver(() => new FabricClient());
            NetTcpBinding binding = CreateClientConnectionBinding();
            WcfCommunicationClientFactory<IShoppingCartService> factory = new WcfCommunicationClientFactory<IShoppingCartService>(binding, null, serviceResolver, null, null);
            Proxy proxy = new Proxy(factory, serviceName, 1);
            proxy.AddItem(new ShoppingCartItem
            {
                ProductName = "XBox One",
                UnitPrice = 329.0,
                Amount = 2
            }).Wait();

            IList<ShoppingCartItem> items = proxy.GetItems().Result;
            Console.WriteLine(items.Count);

            Console.ReadKey();

        }

        private static NetTcpBinding CreateClientConnectionBinding()
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None)
            {
                SendTimeout = TimeSpan.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.FromSeconds(5),
                CloseTimeout = TimeSpan.FromSeconds(5),
                MaxReceivedMessageSize = 1024 * 1024
            };
            binding.MaxBufferSize = (int)binding.MaxReceivedMessageSize;
            binding.MaxBufferPoolSize = Environment.ProcessorCount * binding.MaxReceivedMessageSize;

            return binding;
        }
    }
}
