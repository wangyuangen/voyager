namespace YK.Notice.Options;

public class SignalROptions
{
    public bool UseBackplane { get; set; }

    public Backplane Backplane { get; set; }
}

public class Backplane
{
    public string? Provider { get; set; }
    public string? StringConnection { get; set; }
    public string? Auth { get; set; }
}