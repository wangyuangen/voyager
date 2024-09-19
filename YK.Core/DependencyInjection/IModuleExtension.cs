namespace YK.Core.DependencyInjection;

public interface IModuleExtension
{
    void Register(ICoreServiceBuider serviceBuider);
}
