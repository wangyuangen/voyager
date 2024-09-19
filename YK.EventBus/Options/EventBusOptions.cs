namespace YK.EventBus.Options;

public class EventBusOptions
{
    public bool UseRabbitMQ { get; set; }
    public string HostName { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ExchangeName { get; set; } = string.Empty;
    public int Port { get; set; }
}
