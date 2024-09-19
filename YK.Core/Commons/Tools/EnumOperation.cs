using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml;
using YK.Core.Extensions;

namespace YK.Core.Commons.Tools;

/// <summary>
/// 枚举操作
/// </summary>
public static class EnumOperation
{
    /// <summary>
    /// 获取实例
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
    public static TEnum Parse<TEnum>(object member)
    {
        string value = member.SafeString();
        if (string.IsNullOrWhiteSpace(value))
        {
            if (typeof(TEnum).IsGenericType)
                return default;
            throw new ArgumentNullException(nameof(member));
        }
        return (TEnum)Enum.Parse(Reflections.GetType<TEnum>(), value, true);
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,则传入Enum1.A或0,获取成员名"A"</param>
    public static string GetName<TEnum>(object member)
    {
        return GetName(Reflections.GetType<TEnum>(), member);
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetName(Type type, object member)
    {
        if (type == null)
            return string.Empty;
        if (member == null)
            return string.Empty;
        if (member is string)
            return member.SafeString();
        if (type.GetTypeInfo().IsEnum == false)
            return string.Empty;
        return Enum.GetName(type, member) ?? "";
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可，范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
    public static int GetValue<TEnum>(object member)
    {
        return GetValue(Reflections.GetType<TEnum>(), member);
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static int GetValue(Type type, object member)
    {
        string value = member.SafeString();
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(member));
        return (int)Enum.Parse(type, member.SafeString(), true);
    }

    /// <summary>
    /// 获取描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetDescription<TEnum>(object member)
    {
        return Reflections.GetDescription<TEnum>(GetName<TEnum>(member));
    }

    /// <summary>
    /// 获取描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetDescription(Type type, object member)
    {
        return Reflections.GetDescription(type, GetName(type, member));
    }

    private static int _CodeBaseNotSupportedException = 0;

    /// <summary>
    /// 获得枚举类型说明列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetEnumSummaryList(Type type)
    {
        return LocalGetComment(type, 0);

        Dictionary<string, string> LocalGetComment(Type localType, int level)
        {
            if (localType.Assembly.IsDynamic) return null;
            //动态生成的程序集，访问不了 Assembly.Location/Assembly.CodeBase
            var regex = new Regex(@"\.(dll|exe)", RegexOptions.IgnoreCase);
            var xmlPath = regex.Replace(localType.Assembly.Location, ".xml");
            if (File.Exists(xmlPath) == false)
            {
                if (_CodeBaseNotSupportedException == 1) return null;
                try
                {
                    if (string.IsNullOrEmpty(localType.Assembly.Location)) return null;
                }
                catch (NotSupportedException) //NotSupportedException: CodeBase is not supported on assemblies loaded from a single-file bundle.
                {
                    Interlocked.Exchange(ref _CodeBaseNotSupportedException, 1);
                    return null;
                }

                xmlPath = regex.Replace(localType.Assembly.Location, ".xml");
                if (xmlPath.StartsWith("file:///") && Uri.TryCreate(xmlPath, UriKind.Absolute, out var tryuri))
                    xmlPath = tryuri.LocalPath;
                if (File.Exists(xmlPath) == false) return null;
            }

            var dic = new Dictionary<string, string>();
            StringReader sReader = null;
            try
            {
                sReader = new StringReader(File.ReadAllText(xmlPath));
            }
            catch
            {
                return dic;
            }
            using (var xmlReader = XmlReader.Create(sReader))
            {
                XPathDocument xpath = null;
                try
                {
                    xpath = new XPathDocument(xmlReader);
                }
                catch
                {
                    return null;
                }
                var xmlNav = xpath.CreateNavigator();

                var className = (localType.IsNested ? $"{localType.Namespace}.{localType.DeclaringType.Name}.{localType.Name}" : $"{localType.Namespace}.{localType.Name}").Trim('.');
                var node = xmlNav.SelectSingleNode($"/doc/members/member[@name='T:{className}']/summary");
                if (node != null)
                {
                    var comment = node.InnerXml.Trim(' ', '\r', '\n', '\t');
                    if (string.IsNullOrEmpty(comment) == false) dic.Add("", comment); //class注释
                }

                if (localType.IsEnum)
                {
                    var fields = Enum.GetValues(localType).Cast<Enum>().Select(x => x.ToString()).ToList();
                    foreach (var field in fields)
                    {
                        node = xmlNav.SelectSingleNode($"/doc/members/member[@name='F:{className}.{field}']/summary");
                        if (node != null)
                        {
                            var comment = node.InnerXml.Trim(' ', '\r', '\n', '\t');
                            if (string.IsNullOrEmpty(comment) == false) dic.Add(field, comment); //field注释
                        }
                    }
                }
            }
            return dic;
        }
    }
}
