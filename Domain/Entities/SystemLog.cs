using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SystemLog
    {
        [Key]
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Exception { get; set; }
    }
}