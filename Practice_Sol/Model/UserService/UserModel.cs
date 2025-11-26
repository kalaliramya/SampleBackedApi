namespace Samplebacked_api.Model.UserService
{
    public class UserModel
    {
        public string username { get; set; }
        public string? email { get; set; }
        public string? encrepted_password { get; set; }
        public int? role_id { get; set; }
        public bool? is_active { get; set; }
        public DateTime? created_at { get; set; }
    }
}
