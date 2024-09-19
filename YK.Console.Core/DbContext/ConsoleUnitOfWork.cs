namespace YK.Console.Core.DbContext;

public interface IConsoleUnitOfWork : IUnitOfWork { }

public sealed class ConsoleUnitfOfWork(ConsoleDbContext dbContext) : UnitOfWork(dbContext), IConsoleUnitOfWork { }
