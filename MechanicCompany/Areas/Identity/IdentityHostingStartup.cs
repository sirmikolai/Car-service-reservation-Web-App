using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MechanicCompany.Areas.Identity.IdentityHostingStartup))]
namespace MechanicCompany.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}