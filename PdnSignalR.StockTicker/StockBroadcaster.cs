using Microsoft.AspNet.SignalR;
using SpaceShot.Samples.StockCore;

namespace SpaceShot.Samples.StockTicker
{
    public class StockBroadcaster
    {
        private IHubContext _hubContext;

        public StockBroadcaster(StockCore.StockTicker core, IHubContext hubContext)
        {
            core.StockPriceChangedEvent += StockPriceChanged;
            _hubContext = hubContext;
        }

        private void StockPriceChanged(object sender, StockPriceChangedArgs e)
        {
            if (_hubContext != null)
            {
                _hubContext.Clients.All.StockUpdated(e.Stock);
            }
        }
    }
}
