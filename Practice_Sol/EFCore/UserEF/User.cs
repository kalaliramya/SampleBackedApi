using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;

namespace Samplebacked_api.EFCore.UserEF
{
    [Table("users")]
    public class User
    {
        [Key,Required]
        public int user_id { get; set; }
        public string username { get; set; }
        public string? email { get; set; }
        public string? encrepted_password { get; set; }
        public int? role_id { get; set; }
        public bool? is_active { get; set; }
        public DateTime? created_at { get; set; }
        public string? refresh_token { get; set; }
        public DateTime? token_exp_date { get; set; }


    }
}
