using Microsoft.AspNetCore.Mvc;

namespace ApiSchool.Models
{

    public class AppSettings
    {
        public string? DefaultUserId { get; set; }
        public string? LocalPathLog { get; set; }
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }

        public static string? GetDefaultUserId()
        {
            return DefaultUserId;
        }

        public static string? LocalPathLog { get; set; }
        public static string? Key { get; set; }
        public static string? Issuer { get; set; }
        public static string? Audience { get; set; }

    }
}
