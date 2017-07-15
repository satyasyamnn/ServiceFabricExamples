﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Common;
using System.ServiceModel;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Microsoft.ServiceFabric.Data.Collections;

namespace ShoppingCartService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ShoppingCartService : StatefulService, IShoppingCartService
    {

        public ShoppingCartService(StatefulServiceContext context)
            : base(context)
        {

        }

        public async Task AddItem(ShoppingCartItem item)
        {
            var cart = await StateManager.GetOrAddAsync<IReliableDictionary<string, ShoppingCartItem>>("myCart");
            using(var tx = StateManager.CreateTransaction())
            {
                await cart.AddOrUpdateAsync(tx, item.ProductName, item, (k, v) => item);
                await tx.CommitAsync();
            }
        }

        public async Task DeleteItem(ShoppingCartItem item)
        {
            var cart = await StateManager.GetOrAddAsync<IReliableDictionary<string, ShoppingCartItem>>("myCart");
            using (var tx = StateManager.CreateTransaction())
            {
                var existing = await cart.TryGetValueAsync(tx, item.ProductName);
                if (existing.HasValue)
                    await cart.TryRemoveAsync(tx, item.ProductName);
                await tx.CommitAsync();
            }
        }

        public async Task<IList<ShoppingCartItem>> GetItems()
        {
            IList<ShoppingCartItem> items = new List<ShoppingCartItem>();
            var cart = await StateManager.GetOrAddAsync<IReliableDictionary<string, ShoppingCartItem>>("myCart");
            using (var tx = StateManager.CreateTransaction())
            {
              
            }
            return items;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(initParams=> new WcfCommunicationListener<IShoppingCartService>(initParams, this, CreateListenBinding(), "ServiceEndPoint"))
            };
        }

        private NetTcpBinding CreateListenBinding()
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None)
            {
                SendTimeout = TimeSpan.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.FromSeconds(5),
                CloseTimeout = TimeSpan.FromSeconds(5),
                MaxConnections = int.MaxValue,
                MaxReceivedMessageSize = 1024 * 1024
            };
            binding.MaxBufferSize = (int)binding.MaxReceivedMessageSize;
            binding.MaxBufferPoolSize = Environment.ProcessorCount * binding.MaxReceivedMessageSize;
            return binding;
        }
    }
}
