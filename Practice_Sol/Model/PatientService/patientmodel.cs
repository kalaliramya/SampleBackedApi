using System.ComponentModel.DataAnnotations;

namespace Samplebacked_api.Model.Patient
{
    public class patientmodel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public decimal? age { get; set; }
        public string gender { get; set; }
        public int pin { get; set; }
    }
}
