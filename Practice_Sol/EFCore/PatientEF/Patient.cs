using GrapeCity.Documents.Pdf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samplebacked_api.EFCore.PatientEF
{
    [Table("patients")]
    public class Patient
    {

        [Key, Required]
        public int patient_id { get; set; }
        public string? full_name { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public DateTime? dob { get; set; }
        public int? gender_id { get; set; }
        //[Range(0, 100)]
        public string? address_line { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        [StringLength(6)]
        public string? pin_code { get; set; }
        public bool? is_active { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updation_date { get; set; }


    }

}
