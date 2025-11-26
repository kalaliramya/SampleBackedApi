using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;

namespace Samplebacked_api.EFCore.UserEF
{
    [Table("roles")]
    public class Roles
    {
        [Key, Required]
        public int? role_id { get; set; }
        public string? role_name { get; set; }
        public string? description { get; set; }
        public bool? is_active { get; set; }
        public DateTime? created_at { get; set; }


    }
}
