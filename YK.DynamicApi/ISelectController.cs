using System.Reflection;
using YK.DynamicApi.Attributes;

namespace YK.DynamicApi;

public interface ISelectController
{
    bool IsController(Type type);
}

internal class DefaultSelectController : ISelectController
{
    public bool IsController(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        if (!typeof(IDynamicApi).IsAssignableFrom(type) ||
            !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
        {
            return false;
        }


        var attr = Reflections.GetSingleAttributeOrDefaultByFullSearch<DynamicApiAttribute>(typeInfo);

        if (attr == null)
        {
            return false;
        }

        if (Reflections.GetSingleAttributeOrDefaultByFullSearch<NonDynamicApiAttribute>(typeInfo) != null)
        {
            return false;
        }

        return true;
    }
}