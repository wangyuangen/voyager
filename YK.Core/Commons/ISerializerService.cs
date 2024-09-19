using YK.Core.Contract;

namespace YK.Core.Commons;

public interface ISerializerService: ITransientService
{
    string Serialize<T>(T obj);

    string Serialize<T>(T obj, Type type);

    T Deserialize<T>(string text);
}
