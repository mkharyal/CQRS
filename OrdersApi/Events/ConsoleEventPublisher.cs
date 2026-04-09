public class ConsoleEventPublisher : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event)
    {
        System.Console.WriteLine($"Event published: {@event}");
        await Task.CompletedTask;
    }
}