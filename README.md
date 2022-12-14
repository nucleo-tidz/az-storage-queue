# Storage account client

A light weight nuget package to interact with azure storage account supports producing , consuming , peeking , getting properties wriiten on .NET 6.0
> Follow this to install nuget package in your project - https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "StorageConfiguration": {
    "ConnectionString": ""
  }
}
```

**Depenedency Injection**
```
services.AddQueueClient(hostContext.Configuration.GetSection("StoargeConfiguration"));

```
**Send**
```
Payment payment = new Payment
                    {
                        AccountNumber = DateTime.Now.ToLongDateString(),
                        CreditAmmount = 1000
                    };
                    var response = await _queueClient.SendAsync("payment-q", payment, TimeSpan.FromDays(100));
```
**Consume**
```
 List<Payment> payments = await _queueClient.ConsumeAsync<Payment>("payment-q");
```
**Peek**
```
 List<Payment> payments = await _queueClient.PeekAsync<Payment>("payment-q");
```
**Purge**
```
 await _queueClient.PurgeAsync("payment-q");
```
        
