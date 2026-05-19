using Client;
using Common;
using Demo;
using Server;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Entry
{
    public class CoreLifetimeScope : LifetimeScope
    {
        [SerializeField] private bool _useGuiLogger;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<NetworkManagerWrapper>();

            builder
               .Register<SubscriptionsRepository>(Lifetime.Singleton)
               .AsImplementedInterfaces();

            builder
               .Register<NetworkMessageSender>(Lifetime.Singleton)
               .AsImplementedInterfaces();

            builder
               .Register<HelloMessageProcessor>(Lifetime.Singleton)
               .AsImplementedInterfaces();

            if (_useGuiLogger)
            {
                builder
                   .RegisterComponentInHierarchy<GuiLogger>()
                   .AsImplementedInterfaces();
            }
            else
            {
                builder
                   .Register<UnityDebugLogger>(Lifetime.Singleton)
                   .AsImplementedInterfaces();
            }

            builder
               .Register<HelloMessageRegistrator>(Lifetime.Singleton)
               .AsImplementedInterfaces();

            builder
               .Register<HandlerRegistry>(Lifetime.Singleton)
               .AsImplementedInterfaces();

            builder.RegisterComponentInHierarchy<SendMessageExample>();

            builder
               .RegisterEntryPoint<Server.Server>()
               .AsSelf();

            builder
               .RegisterEntryPoint<Client.Client>()
               .AsSelf();
        }
    }
}
