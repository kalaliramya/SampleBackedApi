using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samplebacked_api.EFCore
{
    [Table("patients")]
    public class Patient
    {
        [Key,Required]
        public int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        [Range(0,100)]
        public decimal? age { get; set; }
        public string? gender { get; set; }
        [StringLength(6)]
        public int? pin { get; set; }
    }
}
