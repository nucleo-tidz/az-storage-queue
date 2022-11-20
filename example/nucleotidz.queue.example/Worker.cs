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
                Console.WriteLine("Press '1' to produce '2' to consume from the queue,'3' to peek , '4' to get properties , '5' to purge");
                var input = Console.ReadLine();
                if (input.ToLower() == "2")
                {
                    Payment payment = new Payment
                    {
                        AccountNumber = DateTime.Now.ToLongDateString(),
                        CreditAmmount = 1000
                    };
                    var response = await _queueClient.SendAsync("payment-q", payment, TimeSpan.FromDays(100));
                }
                else if (input.ToLower() == "2")
                {
                    List<Payment> payments = await _queueClient.ConsumeAsync<Payment>("payment-q");
                }
                else if (input.ToLower() == "3")
                {
                    List<Payment> payments = await _queueClient.PeekAsync<Payment>("payment-q");
                }
                else if (input.ToLower() == "4")
                {
                    var properties = await _queueClient.GetPropertiesAsync("payment-q");
                }
                else if (input.ToLower() == "5")
                {
                    var properties = await _queueClient.PurgeAsync("payment-q");
                }
            }
        }
    }
}