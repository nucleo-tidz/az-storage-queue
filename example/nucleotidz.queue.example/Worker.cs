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
        private readonly IQueueClient _queueClient;
        public Worker(ILogger<Worker> logger, IQueueClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Press 'p' to produce 'c' to consume from the queue");
            var input = Console.ReadLine();
            if (input.ToLower() == "p")
            {
                Payment payment = new Payment
                {
                    AccountNumber = "135637474748",
                    CreditAmmount = 1000
                };
               await _queueClient.SendAsync("payment-q", payment);
            }
            else if (input.ToLower() == "c")
            {

            }

        }
    }
}