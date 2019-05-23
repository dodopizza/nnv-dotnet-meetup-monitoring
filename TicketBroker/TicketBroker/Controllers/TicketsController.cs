using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace TicketBroker.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private static bool _generateErrors = false;
        
        private readonly Random _random = new Random();
        private readonly string[] _paymentTypes = {"cash", "card"};
        private readonly string[] _ticketTypes = {"paper", "e-ticket"};
        
        private readonly Counter _viewsCounter = Metrics.CreateCounter(
            "views", "Total number of first page views");
        
        private readonly Counter _ordersCounter = Metrics.CreateCounter(
            "orders", "Total number of sold tickets", new[] {"paymentType", "ticketType"});
       
        private readonly Counter _paymentsCounter = Metrics.CreateCounter(
            "payments", "Total number of payments", new[] {"type"});

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (_generateErrors && ShouldFail(5))
            {
                return StatusCode(500);
            }
            
            _viewsCounter.Inc();
            return new string[]
            {
                "Билет на dotnet meetup", 
                "Билет на балет"
            };
        }
        
        [HttpPost]
        [Route("checkout")]
        public IActionResult Checkout()
        {
            if (_generateErrors && ShouldFail(5))
            {
                return StatusCode(500);
            }
            
            var ticketType = _ticketTypes[_random.Next(_ticketTypes.Length)];
            var paymentType = _paymentTypes[_random.Next(_paymentTypes.Length)];
            
            _ordersCounter.WithLabels(ticketType, paymentType).Inc();
            
            return Ok();
        }
        
        [HttpPost]
        [Route("pay")]
        public IActionResult Pay()
        {
            if (_generateErrors && ShouldFail(5))
            {
                return StatusCode(500);
            }
            
            var paymentType = _paymentTypes[_random.Next(_paymentTypes.Length)];
            _paymentsCounter.WithLabels(paymentType).Inc();
            return Ok();
        }
        
        [HttpGet]
        [Route("toggleFails")]
        public IActionResult ToggleFails()
        {
            _generateErrors = !_generateErrors;
            return Ok();
        }

        private bool ShouldFail(int probability)
        {
            return _random.Next(100) < probability;
        }
    }
}