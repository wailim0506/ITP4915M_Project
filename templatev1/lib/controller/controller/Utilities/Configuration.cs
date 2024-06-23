using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace controller.Utilities
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
            Setup();
        }

        private string GetApiKey()
        {
            return _configuration["GoogleMapsApiKey"];
        }

        private List<string> GetDataBaseConnectionStringsList()
        {
            return _configuration.GetSection("connectionStrings").GetChildren().Select(x => x.Value).ToList();
        }

        public string GoogleMapsApiKey => GetApiKey();

        public List<string> DataBaseConnectionStringsList => GetDataBaseConnectionStringsList();

        public void CreateAvatarFolder()
        {
            string path = Directory.GetCurrentDirectory() + "\\Upload\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void Setup()
        {
            CreateAvatarFolder();
        }
    }
}