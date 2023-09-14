using StockBrokarageChallenge.Application.Shared.Abstractions;

namespace StockBrokarageChallenge.Application.Shared.Models
{
    public class StocksWallet
    {
        public int Id { get;  private set; }

        public int WalletId { get;  private set; }

        public int StockId { get;  private set; }

        public double AveragePrice { get; private set; }

        public int StockQuantity { get;  private set; }

        public double TotalInvestedStock { get;  private set; }

        public double CurrentInvestedStock { get; private set; }
        public Stock Stock { get; private set; }
        public StocksWallet()
        {
            StockQuantity = 0;
            TotalInvestedStock = 0.0;
            CurrentInvestedStock = 0.0;
        }

        private void CalculateAveragePrice() 
        {
            AveragePrice = (TotalInvestedStock / StockQuantity);
        }

        private void CalculateCurrentInvestedStock(double currentStockPrice) 
        { 
            CurrentInvestedStock = currentStockPrice * StockQuantity;
        }

        public void AddStock(Stock stock, int quantity) 
        {
            TotalInvestedStock = (stock.Price * quantity);
            StockQuantity = quantity;
            CalculateAveragePrice();
            CalculateCurrentInvestedStock(stock.Price);

            Stock = stock; 
        }

        public void BuyStocks(int quantity)
        {
            DomainExceptionValidation.When(quantity < 0, "Quantity must be greater than 0");
            StockQuantity += quantity;
            TotalInvestedStock += (Stock.Price * quantity);

            CalculateAveragePrice();
            CalculateCurrentInvestedStock(Stock.Price);
        }

        public void SellStocks(int quantity) 
        {
            DomainExceptionValidation.When(quantity > StockQuantity, "Quantity must be lower than StockQuantity");
            if(quantity < StockQuantity)
            {
                var proportionalValue = (double)quantity / StockQuantity;
                StockQuantity -= quantity;

                TotalInvestedStock *= proportionalValue;
                CalculateAveragePrice();
                CalculateCurrentInvestedStock(Stock.Price);
            } else if(quantity == StockQuantity)
            {
                StockQuantity = 0;
                TotalInvestedStock = 0.0;
                AveragePrice = 0.0;
                CurrentInvestedStock = 0.0;
            }
        }
    
        public void UpdateCurrentValue() 
        {
            CalculateCurrentInvestedStock(Stock.Price);
        }
    }
}
