using System;
using Microsoft.Extensions.Configuration;

namespace ReDoProject.Persistence
{
    public static class CConfigurations
    {
        public static string GetString(string key)
        {
            ConfigurationManager configurationManager = new();

            string path = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Week_6_4\\Infrastructure\\Week_6_4.Persistence";

            configurationManager.SetBasePath(path);
            configurationManager.AddJsonFile("Private_Informations.json");
            return configurationManager.GetSection(key).Value;
        }
    }
}

