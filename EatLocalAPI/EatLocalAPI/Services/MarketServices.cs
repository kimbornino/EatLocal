using EatLocalAPI.Models;
using MarketServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketServices.Services
{
   
    public class MarketServices : IMarketServices
    {
        private readonly Dictionary<string, Markets> _marketsDictionary;
        public MarketServices()
        {
            _marketsDictionary = new Dictionary<string, Markets>();
        }
        public Markets AddMarkets(Markets marketItems)
        {
            _marketsDictionary.Add(marketItems.name, marketItems);

            return marketItems;
        }
       
    }    
}
