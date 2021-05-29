using System;
using System.Configuration;
using EcoSendWeb.Controllers;
using EcoSendWeb.Services.Api;
using EcoSendWeb.Services.Impl;
using Microsoft.Practices.Unity;

namespace EcoSendWeb.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ParcelController>(
                new InjectionProperty(nameof(ParcelController.LiqPayPublicKey), ConfigurationManager.AppSettings["liqpay.public.key"]),
                new InjectionProperty(nameof(ParcelController.LiqPayPrivateKey), ConfigurationManager.AppSettings["liqpay.private.key"])
            );

            container.RegisterType<IHomeServ, HomeServ>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICacheServ, CacheServ>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountServ, AccountServ>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParcelServ, ParcelServ>(new ContainerControlledLifetimeManager());

        }
    }
}
