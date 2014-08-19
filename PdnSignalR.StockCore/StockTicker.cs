using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using Microsoft.AspNet.SignalR;

namespace SpaceShot.Samples.StockCore
{
    public class StockTicker
    {
        private static IHubContext _hubContext;
        public static IHubContext HubContext
        { 
            set {_hubContext = value;}
        }

        // Singleton instance
        private readonly static Lazy<StockTicker> _instance = new Lazy<StockTicker>(() => new StockTicker());

        private readonly ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change
        private readonly double _rangePercent = .002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
        private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;

        private StockTicker()
        {
            // Clear the list, create some "stocks" and add them to the list
            _stocks.Clear();
            var stocks = new List<Stock>
            {
                new Stock { Symbol = "MSFT", Price = 30.31m },
                new Stock { Symbol = "APPL", Price = 578.18m },
                new Stock { Symbol = "GOOG", Price = 570.30m }
            };
            stocks.ForEach(stock => _stocks.TryAdd(stock.Symbol, stock));

            // start the timer to "update stock prices"
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }

        public static StockTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            // simple "what are the stock values right now?" query
            return _stocks.Values;
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    foreach (var stock in _stocks.Values)
                    {
                        if (StockPriceChanged(stock))
                        {
                            if (_hubContext != null)
                            {
                                _hubContext.Clients.All.StockUpdated(stock);
                            }

                        }
                    }

                    _updatingStockPrices = false;
                }
            }
        }

        private bool StockPriceChanged(Stock stock)
        {
            // Randomly choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                // let's not change it
                return false;
            }

            // Let's change it!

            // Update the stock price by a random factor of the range percent
            var random = new Random((int)Math.Floor(stock.Price));
            var percentChange = random.NextDouble() * _rangePercent;
            var pos = random.NextDouble() > .51;
            var change = Math.Round(stock.Price * (decimal)percentChange, 2);
            change = pos ? change : -change;

            stock.Price += change;

            // Prove that this singleton runs once the public Instance property is accessed
            Debug.WriteLine("Stock Price Change: {0} {1} - {2}", stock.Symbol, stock.Price, stock.Change);

            return true;
        }
    }
}