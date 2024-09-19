using System.ComponentModel.DataAnnotations;

namespace YK.ORM.Options;

/// <summary>
/// 数据库配置项抽象类
/// </summary>
public abstract class DatabaseOptions :IValidatableObject
{
    public string Provider { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            yield return new ValidationResult("connection string cannot be empty.", new[] { nameof(ConnectionString) });
        }
    }
}
