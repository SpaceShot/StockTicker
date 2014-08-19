using System;

namespace SpaceShot.Samples.StockCore
{
    public class StockPriceChangedArgs : EventArgs
    {
        public Stock Stock { get; set; }

        public StockPriceChangedArgs(Stock stock)
        {
            Stock = stock;
        }
    }
}
