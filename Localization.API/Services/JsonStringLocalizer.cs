using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly Dictionary<string, string> _localizationStrings;
    private readonly string _baseName;
    private readonly IConfiguration _configuration;

    public JsonStringLocalizer(IConfiguration configuration, CultureInfo culture, string baseName)
    {
        _configuration = configuration;
        _baseName = baseName;

        var jsonFileName = $"{baseName}.{culture.Name}.json";
        var jsonFilePath = Path.Combine("Resources", jsonFileName);

        if (File.Exists(jsonFilePath))
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            _localizationStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }
        else
        {
            _localizationStrings = new Dictionary<string, string>();
        }
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = _localizationStrings.ContainsKey(name) ? _localizationStrings[name] : name;
            return new LocalizedString(name, value);
        }
    }

    public LocalizedString this[string name, params object[] arguments] => this[name];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _localizationStrings.Select(kv => new LocalizedString(kv.Key, kv.Value));
    }

    public IStringLocalizer WithCulture(CultureInfo culture)
    {
        return new JsonStringLocalizer(_configuration, culture, _baseName);
    }
}
