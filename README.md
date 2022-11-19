# Storage account client

A light weight nuget package to interact with azure storage account supports producing , consuming , peeking , getting properties wriiten on .NET 6.0

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "StoargeConfiguration": {
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
        
