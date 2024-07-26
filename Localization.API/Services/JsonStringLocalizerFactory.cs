using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly IConfiguration _configuration;

    public JsonStringLocalizerFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        var baseName = resourceSource.Name;
        return new JsonStringLocalizer(_configuration, CultureInfo.CurrentUICulture, baseName);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(_configuration, CultureInfo.CurrentUICulture, baseName);
    }
}
