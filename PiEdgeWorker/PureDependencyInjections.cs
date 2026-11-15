using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PiEdgeWorker.PureDependencyInjections;

namespace PiEdgeWorker
{
    internal class PureDependencyInjections
    {
        // Step 1: Define the abstraction (contract)
        public interface IEmailSender
        {
            void Send(string to, string message);
        }

        // Step 2: Create concrete implementations
        public class SmtpEmailSender : IEmailSender
        {
            public void Send(string to, string message)
                => Console.WriteLine($"[SMTP] To {to}: {message}");
        }

        public class FakeEmailSender : IEmailSender
        {
            public void Send(string to, string message)
                => Console.WriteLine($"[FAKE TEST] To {to}: {message}");
        }

        // Step 3: OrderService asks for IEmailSender in its constructor (NO 'new'!)
        public class OrderService
        {
            private readonly IEmailSender _emailSender;

            public OrderService(IEmailSender emailSender)
            {
                _emailSender = emailSender;
            }

            public void PlaceOrder(string userEmail)
            {
                _emailSender.Send(userEmail, "Order confirmed!");
            }
        }
    }
}

//// In Production:
//OrderService prodOrderService = new OrderService(new SmtpEmailSender());
//prodOrderService.PlaceOrder("anna@example.com");

//// In a Unit Test (zero code changes to OrderService!):
//OrderService testOrderService = new OrderService(new FakeEmailSender());
//testOrderService.PlaceOrder("test@example.com");