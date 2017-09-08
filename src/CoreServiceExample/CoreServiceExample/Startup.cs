using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IServiceProvider = System.IServiceProvider;

namespace CoreServiceExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            var bus = provider.GetService<IBus>();
            bus.Subscribe<string>("Message", Console.WriteLine);

            app.Run(_ => Task.CompletedTask);
        }
    }
}
