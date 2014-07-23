using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PdnSignalR.StockTicker.Startup))]

namespace PdnSignalR.StockTicker
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapHubs();
        }
    }
}
