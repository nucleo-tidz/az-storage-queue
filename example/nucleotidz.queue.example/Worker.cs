using nucelotidz.storage.queue;

namespace nucleotidz.queue.example
{
    public class Payment
    {
        public string AccountNumber { get; set; }
        public double CreditAmmount { get; set; }
    }
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQClient _queueClient;
        public Worker(ILogger<Worker> logger, IQClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Press 'p' to produce 'c' to consume from the queue,'k' to peek , 'r' to get properties");
                var input = Console.ReadLine();
                if (input.ToLower() == "p")
                {
                    Payment payment = new Payment
                    {
                        AccountNumber = DateTime.Now.ToLongDateString(),
                        CreditAmmount = 1000
                    };
                    var response = await _queueClient.SendAsync("payment-q", payment, TimeSpan.FromDays(100));
                }
                else if (input.ToLower() == "c")
                {
                    List<Payment> payments = await _queueClient.ConsumeAsync<Payment>("payment-q");
                }
                else if (input.ToLower() == "k")
                {
                    List<Payment> payments = await _queueClient.PeekAsync<Payment>("payment-q");
                }
                else if (input.ToLower() == "r")
                {
                    var properties = await _queueClient.GetProperties("payment-q");
                }
            }
        }
    }
}