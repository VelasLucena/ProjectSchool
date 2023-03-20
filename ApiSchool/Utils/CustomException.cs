using System.IO;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Utils
{
    public class InvalidException :Exception
    {
        public InvalidException() { }

        public InvalidException(Exception exception) 
        {
            string createText = exception.ToString() + Environment.NewLine;
            File.WriteAllText(AppStartUp.GetSettingsApp(AppSettingsKeys.LocalPathLog), createText);
        }

    }
}
