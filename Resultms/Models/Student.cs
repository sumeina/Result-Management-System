using System.ComponentModel.DataAnnotations;

namespace Resultms.Models
{
    public class Student
    {

        [Key]
        public int id { get; set; }

        [Required]
        public int Roll { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Math { get; set; }

        [Required]
        public string Science { get; set; }

        [Required]
        public string Total { get; set; }
    }
}
