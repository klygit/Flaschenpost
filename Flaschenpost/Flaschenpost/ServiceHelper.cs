using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Flaschenpost
{
    internal static class ServiceHelper
    {
        internal static TService Get<TService>() => ServiceProvider.GetService<TService>();

        private static IServiceProvider ServiceProvider { get; set; }

        internal static void Init(App app, Func<Page> createRootPageFunc)
        {
            // init DI container
            ServiceProvider = new ServiceCollection()
                // managers
                .AddSingleton<IPageManager, PageManager>()
                .AddSingleton<IDataManager, DataManager>()
                .AddSingleton<IPrefManager, PrefManager>()
                // repository
                .AddSingleton<IRemoteProductsRepository, RemoteProductsRepository>()
                //.AddSingleton<IRemoteProductsSource, DemoRemoteProductsSource>()
                .AddSingleton<IRemoteProductsSource, RemoteProductsSource>()
                // viewModels
                .AddTransient<SideMenuViewModel>()
                .AddTransient<ItemsViewModel>()
                .AddTransient<ItemDetailViewModel>()
                .AddTransient<SelectSortViewModel>()
                .AddTransient<AboutViewModel>()
                // build
                .BuildServiceProvider();

            Get<IPageManager>().Init(app, createRootPageFunc.Invoke());
        }
    }
}
