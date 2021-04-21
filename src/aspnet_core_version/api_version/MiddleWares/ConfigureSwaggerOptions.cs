using System.Collections.Specialized;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api_version.MiddleWares
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
      readonly IApiVersionDescriptionProvider provider;

      public ConfigureSwaggerOptions( IApiVersionDescriptionProvider provider ) =>
        this.provider = provider;

      public void Configure( SwaggerGenOptions options )
      {
        foreach ( var description in provider.ApiVersionDescriptions )
        {
          options.SwaggerDoc(
            description.GroupName,
              new OpenApiInfo()
              {
                Title = $"Sample API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description=description.IsDeprecated?"This Api version has been deprecated":string.Empty
              }
              );
        }
      }
    }
}
