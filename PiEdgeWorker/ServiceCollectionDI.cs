using Microsoft.Extensions.DependencyInjection;
using static PiEdgeWorker.PureDependencyInjections;

namespace PiEdgeWorker
{
    internal class ServiceCollectionDI
    {
        public static void RegisterAll()
        {
            // 1. Create the DI container registry
            var services = new ServiceCollection();

            // 2. Register mappings: "Whenever someone asks for IEmailSender, give them SmtpEmailSender"
            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddTransient<OrderService>();
            services.AddTransient<CheckoutWorkflow>();
            services.AddTransient<Runner>();

            // 3. Build the ServiceProvider (the runtime factory)
            using ServiceProvider provider = services.BuildServiceProvider();

            // 3. Resolve ONLY the single root object (Runner)
            // The container automatically builds:
            // SmtpEmailSender -> OrderService -> CheckoutWorkflow -> Runner
            var runner = provider.GetRequiredService<Runner>();
            runner.Run();
        }
    }

    internal class Runner
    {
        ServiceProvider _provider;
        CheckoutWorkflow _checkoutService;

        public Runner(ServiceProvider provider)
        {
            _provider = provider;
            _checkoutService = _provider.GetRequiredService<CheckoutWorkflow>();
        }

        public void Run() 
        {
            _checkoutService.CompleteCheckout("anna@example.com");
        }
    }

    //some other class that wants to use OrderService
    internal class CheckoutWorkflow
    {
        private readonly OrderService _orderService;

        public CheckoutWorkflow(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void CompleteCheckout(string email)
        {
            _orderService.PlaceOrder(email);
        }
    }
}
