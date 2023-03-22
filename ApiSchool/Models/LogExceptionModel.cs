using System.ComponentModel.DataAnnotations;

namespace ApiSchool.Models
{
    public class LogExceptionModel
    {
        [Key]
        public int? LogExceptionId { get; set; } = null;
        public string? Error { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CreateUserId { get; set; }
    }
}
