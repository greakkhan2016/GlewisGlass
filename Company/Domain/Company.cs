using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Exchange { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ticker { get; set; }

        [Required]
        [MaxLength(100)]
        public string Isin { get; set; }

        public string Website { get; set; }
    }
}
