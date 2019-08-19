using System.IO;
using Microsoft.Extensions.Configuration;

namespace HangoutBotDeploy {

  public class ConfigSettings {

    private IConfiguration _config;

    public ConfigSettings() {

      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      this._config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

    }

    public IConfiguration GetConfig() {

      return this._config;

    }

  }

}
