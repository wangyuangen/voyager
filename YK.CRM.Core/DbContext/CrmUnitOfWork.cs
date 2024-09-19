namespace YK.CRM.Core.DbContext;

public interface ICrmUnitOfWork : IUnitOfWork { }

public class CrmUnitOfWork(CrmDbContext dbContext) : UnitOfWork(dbContext), ICrmUnitOfWork { }
