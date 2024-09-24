using System.Globalization;
using System.Resources;

namespace Application;

public static class ResourceManagerService
{
    private static readonly ResourceManager ResourceManager = new ResourceManager("Application.Resources.Language", typeof(ResourceManagerService).Assembly);

    // Método para obter a string de recursos com base na chave e cultura
    public static string GetString(string key, string culture)
    {
        var cultureInfo = new CultureInfo(culture);
        return ResourceManager.GetString(key, cultureInfo) ?? key;
    }
}