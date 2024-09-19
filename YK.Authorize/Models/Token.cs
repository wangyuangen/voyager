namespace YK.Authorize.Models;

public class Token
{
    /// <summary>
    /// Token Value
    /// </summary>
    public string TokenValue { get; set; } = default!;
    /// <summary>
    /// Expires (unit second)
    /// </summary>
    public DateTime? Expires { get; set; } = default!;
    /// <summary>
    /// token type
    /// </summary>
    public string TokenType { get; set; } = default!;
}
