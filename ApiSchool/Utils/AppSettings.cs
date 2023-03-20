using ApiSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Utils
{
    public class AppStartUp
    {
        public static string GetSettingsApp(AppSettingsKeys key)
        {
            string? result = string.Empty;
            ConfigurationModel configurationModel = new ConfigurationModel();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            config.GetSection("AppSettings").Bind(configurationModel);

            foreach(PropertyInfo propertyInfo in configurationModel.GetType().GetProperties())
            {
                if (propertyInfo.Name == Enum.GetName(key.GetType(), key))
                    return propertyInfo.GetValue(configurationModel.LocalPathLog).ToString();
            }

            if (result == null)
                return string.Empty;

            return result;
        }
    }
}
