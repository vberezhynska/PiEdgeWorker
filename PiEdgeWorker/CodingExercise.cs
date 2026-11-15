using System;

    namespace Coding.Exercise 
    {
        public class PriceDroppedArgs(decimal price, decimal threshold) : EventArgs
            {
                public decimal Price { get; } = price;
                public decimal Threshold { get; } = threshold;
            }

        // Define the Stock class which includes the event system
        public class Stock
        {
            private decimal _price;
            private decimal _threshold;
            private bool _isLowerThreshold = false;

            public event EventHandler<PriceDroppedArgs>? PriceDroped;
            // Property to get and set the stock price
            public decimal Price
            {
                get => _price; 
                set {
                    _price = value;
                    Console.WriteLine($"Stock price has changes to {_price}");
                    PriceDroppedArgs e = new(_price, _threshold);
                    if (_price < _threshold && !_isLowerThreshold)
                        {
                            _isLowerThreshold = true;

                            RaiseStockPriceChangedEvent(e);
                        }
                    
                    if (_price >= _threshold && _isLowerThreshold) //restore isLowerThreshold
                        _isLowerThreshold = false;
                    }
            }

            public decimal Threshold
            {
                get => _threshold;
                set { _threshold = value; }
            }

            protected virtual void RaiseStockPriceChangedEvent(PriceDroppedArgs e)
            {
                PriceDroped?.Invoke(this, e);
            }
        }

        public class StockAlert
        {
            public void TriggerAlertInConsole(object? publisher, PriceDroppedArgs args)
            {
                Console.WriteLine($"[ALERT]: Price droped. Price: {args.Price}, Threshold: {args.Threshold}");
            }
        }
}
