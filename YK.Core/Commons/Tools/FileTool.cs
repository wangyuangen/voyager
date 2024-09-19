using Microsoft.AspNetCore.StaticFiles;

namespace YK.Core.Commons.Tools;

public class FileTool
{
    public static FileExtensionContentTypeProvider GetFileExtensionContentTypeProvider()
    {
        FileExtensionContentTypeProvider fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
        fileExtensionContentTypeProvider.Mappings[".iec"] = "application/octet-stream";
        fileExtensionContentTypeProvider.Mappings[".patch"] = "application/octet-stream";
        fileExtensionContentTypeProvider.Mappings[".apk"] = "application/vnd.android.package-archive";
        fileExtensionContentTypeProvider.Mappings[".pem"] = "application/x-x509-user-cert";
        fileExtensionContentTypeProvider.Mappings[".gzip"] = "application/x-gzip";
        fileExtensionContentTypeProvider.Mappings[".7zip"] = "application/zip";
        fileExtensionContentTypeProvider.Mappings[".jpg2"] = "image/jp2";
        fileExtensionContentTypeProvider.Mappings[".et"] = "application/kset";
        fileExtensionContentTypeProvider.Mappings[".dps"] = "application/ksdps";
        fileExtensionContentTypeProvider.Mappings[".cdr"] = "application/x-coreldraw";
        fileExtensionContentTypeProvider.Mappings[".shtml"] = "text/html";
        fileExtensionContentTypeProvider.Mappings[".php"] = "application/x-httpd-php";
        fileExtensionContentTypeProvider.Mappings[".php3"] = "application/x-httpd-php";
        fileExtensionContentTypeProvider.Mappings[".php4"] = "application/x-httpd-php";
        fileExtensionContentTypeProvider.Mappings[".phtml"] = "application/x-httpd-php";
        fileExtensionContentTypeProvider.Mappings[".pcd"] = "image/x-photo-cd";
        fileExtensionContentTypeProvider.Mappings[".bcmap"] = "application/octet-stream";
        fileExtensionContentTypeProvider.Mappings[".properties"] = "application/octet-stream";
        fileExtensionContentTypeProvider.Mappings[".m3u8"] = "application/x-mpegURL";
        return fileExtensionContentTypeProvider;
    }
}
