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
        private readonly IAdminQClient _adminClient;
        public Worker(ILogger<Worker> logger, IQClient queueClient, IAdminQClient adminClient)
        {
            _logger = logger;
            _queueClient = queueClient;
            _adminClient = adminClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Press 'a' for admin operation,'i' for input output operation");
            var action = Console.ReadLine();
            if (action.ToLower() == "i")
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Press '1' to produce '2' to consume from the queue,'3' to peek , '4' to get properties , '5' to purge");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "1")
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
            else if (action.ToLower() == "a")
            {
                Console.WriteLine("Press '1' to create '2' to delete a topic,'3' to set metadata");
                var input = Console.ReadLine();
                if (input.ToLower() == "1")
                {
                    await _adminClient.CreateAsync("q-test");
                }
                else if (input.ToLower() == "2")
                {
                    await _adminClient.DeleteAsync("q-test");
                }
                else if (input.ToLower() == "3")
                {
                    Dictionary<string, string> metaData = new Dictionary<string, string>();
                    metaData.TryAdd("qtype", "payment");
                    await _adminClient.SetMetaDataAsync(metaData, "q-test");
                }
            }
        }
    }
}