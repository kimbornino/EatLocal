using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatLocalAPI.Models;
using MarketServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatLocalAPI.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketServices _services;

        public MarketController(IMarketServices services)
        {
            _services = services;
        }
        [HttpPost]
        public ActionResult<Markets> AddMarkets(Markets marketItem)
        {
            var markets = _services.AddMarket(marketItem);
            
            if(markets == null)
            {
                return marketItems;
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult<Dictionary<string, Markets>> GetMarkets()
        {
            var markets = _services.GetMarkets();

            if (markets.Count == 0)
            {
                return markets;
            }
        }

    }
}