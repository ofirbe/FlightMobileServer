using FlightMobileApp.Manager;
using FlightMobileApp.Model;
using FlightMobileApp.Model.Manager;
using FlightMobileApp.Model.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightMobileApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IConnectionManager, ConnectionManager>();

            // create the singeltons
            string IP = Configuration.GetValue<string>("SimulatorInfo:IP");
            int telnetPort = Configuration.GetValue<int>("SimulatorInfo:TelnetPort");
            int httpPort = Configuration.GetValue<int>("SimulatorInfo:HttpPort");
            var commandManager = new CommandManager(IP, telnetPort);
            services.AddSingleton(commandManager);
            var screenshotManager = new ScreenshotManager(IP, httpPort);
            services.AddSingleton(screenshotManager);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}