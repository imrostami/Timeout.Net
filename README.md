# Timeout.Net
This class is very easy to use
Just add it to your project
And then



```csharp
   using Timeout.Net
```




1.Create an instance of the TimeoutContext class

```csharp
    TimeoutContext<string, string> ctx = new TimeoutContext<string, string>();
```


2.Add Timeout Context Event Handllers


```csharp
timeout.OnItemScheduled += timeout_OnKeyAdded;
timeout.OnScheduledItemExpired += timeout_OnKeyExpier;





void timeout_OnKeyExpier(string key)
{
    Console.WriteLine($"Key {key} Expierd");
}

void timeout_OnKeyAdded(string key)
{
    Console.WriteLine($"Key {key} Added");
}
```
    
    


<h2>Scedule a Key Value</h2>

```csharp
  //this code scedule a string key for 10 seccend
   timeout.SetTimeout("key" , new TimeSpan(hours: 0, 0, 10))
```
    
    

<h3>Scedule a Action or Code Example</h3>



```csharp
   TimeoutContext<Action, object> timeout = new TimeoutContext<Action, object>();


timeout.SetTimeout(() =>
{
    Console.WriteLine("10 seccend task timeouted");
}, null, new TimeSpan(hours: 0, 0, 10));

timeout.SetTimeout(() =>
{
    Console.WriteLine("15 seccend task timeouted");
}, null, new TimeSpan(hours: 0, 0, 15));



```
    
    






