using Coding.Exercise;
using PiEdgeWorker;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    public delegate void MyDeligate(string s);
    
    public class NotifyArgs : EventArgs 
    {
        public string Message { get; }
        public NotifyArgs(string message)
        {
            Message = message;
        }
    }
    

    public class EventPublisher
    {  
        public event EventHandler<NotifyArgs> Notify;
        public void RaiseEvent(NotifyArgs e)
        {
            Notify?.Invoke(this, e);
        }
    }

    public class EventSubscriber
    {
        public void OnEventRaised(object? publisher, NotifyArgs e)
        {
            Console.WriteLine($"[Subscriber] Got event from {publisher}. Message: {e.Message}");
        }

        public void OneMethod(string message)
        { 
        
        }

        public void AnotherOneMethod(string message)
        {

        }
    }

    struct Data
    {
        public int age;
        public Data(int age) => this.age = age;
    }

    static async Task Main(string[] args)
    {
        // Create instances of Stock and StockAlert
        var stockPublisher = new Stock();
        var stockAlert = new StockAlert();

        stockPublisher.PriceDroped += stockAlert.TriggerAlertInConsole;

        // Set the alert threshold
        stockPublisher.Threshold = 50.1m;

        // Simulate stock price changes
        stockPublisher.Price = 100;
        Thread.Sleep(1000);
        stockPublisher.Price = 80;
        Thread.Sleep(1000);
        stockPublisher.Price = 60;
        Thread.Sleep(1000);
        stockPublisher.Price = 50.05m;
        Thread.Sleep(1000);
        stockPublisher.Price = 48;
        Thread.Sleep(1000);
        stockPublisher.Price = 52;
        Thread.Sleep(1000);
        stockPublisher.Price = 55;
        Thread.Sleep(1000);
        stockPublisher.Price = 49;
        Thread.Sleep(1000);
        stockPublisher.Price = 30;
        Thread.Sleep(1000);


         Console.ReadLine();
         
         EventPublisher publisher = new EventPublisher();
         EventSubscriber subscriber = new EventSubscriber();
         
         publisher.Notify += subscriber.OnEventRaised;
        
         publisher.RaiseEvent(new NotifyArgs("Here is my message!"));

        var sub = new EventSubscriber();
        MyDeligate d = sub.AnotherOneMethod;
        d += sub.OneMethod;
        d?.Invoke("Here is my message!");

        int[] arr = { 1, 2, 3 };
        Array.Resize(ref arr, arr.Length + 10);
        arr[6] = 5;

        IEnumerable<Data> datas = Enumerable.Range(0, 1000).Select(d => new Data(d));
        var myData = datas.Where(d => d.age % 2 == 0);

        MethodInfo? secretMethod = typeof(EventPublisher).GetMethod("RaiseEvent"); //type.GetMethod("Secret", BindingFlags.NonPublic | BindingFlags.Instance);
        var data = new Data(25);
        PropertyInfo? secretProperty = typeof(Data).GetProperty("age");
        secretProperty?.SetValue(data, 39);

        var validPerson = new User { Age = 34 };
        bool isValidPersonValid = UserModelValidator.TryValidate(validPerson, out var validPersonErrors);
        Console.WriteLine(isValidPersonValid);

        var wrongPerson = new User { Age = 34 };
        bool isWrongPersonValid = UserModelValidator.TryValidate(validPerson, out var wrongPersonErrors);
        Console.WriteLine($"{isWrongPersonValid} {string.Join('\n', wrongPersonErrors)}");
    }
}