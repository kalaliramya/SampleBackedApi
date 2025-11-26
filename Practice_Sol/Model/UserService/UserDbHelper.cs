using EF_API_Pg.Model;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samplebacked_api.Auth;
using Samplebacked_api.EFCore;
using Samplebacked_api.EFCore.UserEF;
using System.Data;

namespace Samplebacked_api.Model.UserService
{
    public class UserDbHelper
    {
        private patientDbContext _context;
        private readonly JwtService jwtdbhelper;

        private readonly IPasswordHasher<object> _hasher;

       public UserDbHelper(patientDbContext context, JwtService jwt, IPasswordHasher<object> hasher)
        {
            _context = context;
            jwtdbhelper = jwt;
            _hasher = hasher;
        }

        public async Task<ApiResponse> GetAllRoles(int? roleid)
        {
            ApiResponse response = new ApiResponse();
            var data = await _context.roles.AsNoTracking().ToListAsync();
            if (roleid != null)
            {
                data = data.Where(i => i.role_id == roleid).ToList();
            }
            response.ResponseData = data;
            return response;
        }

        public async Task<ApiResponse> CreateUser(UserModel model)
        {
            ApiResponse response = new ApiResponse();
            User user = new User();
            user.username = model.username;
            user.email = model.email;
            user.encrepted_password = _hasher.HashPassword(null, model.encrepted_password);
            //model.encrepted_password;
            user.role_id = model.role_id;
            user.is_active = model.is_active;
            user.created_at = model.created_at;
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return response;
        }
        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
        public async Task<ApiResponse> ValidateUser(string username, string pw)
        {
            ApiResponse response = new ApiResponse();

            var data = await _context.users.Where(i => i.username == username ).SingleOrDefaultAsync();
            if (data == null)
            {

                response.ResponseData = "invalied username";
                return response;
            }

            bool hashedPassword = VerifyPassword(data.encrepted_password, pw);
            
            if (data != null && hashedPassword == true )
            {

                var token = jwtdbhelper.GenerateToken(username, pw);
                var refreshToken = jwtdbhelper.GenerateRefreshToken();
                data.refresh_token = refreshToken;
                data.token_exp_date = DateTime.UtcNow.AddSeconds(10);
                _context.SaveChanges();

                response.ResponseData = token;
            }
            else
            {
                response.ResponseData = "invalied password";
            }
            return response;

        }

      

        public async Task<ApiResponse> RefreshTokenGen(string request)
        {
            ApiResponse response = new ApiResponse();

            // 1. Find user with matching refresh token
            var user = await _context.users
                .FirstOrDefaultAsync(x => x.refresh_token == request);

            if (user == null || user.token_exp_date >= DateTime.UtcNow)
            {
                response.Message = "Invalid or expired refresh token";
                return response;
            }

            // 2. Generate new access token  jwtdbhelper.GenerateToken(username, pw)
            var newAccessToken = jwtdbhelper.GenerateToken(user.username, user.encrepted_password);

            // 3. Generate new refresh token
            var newRefreshToken = jwtdbhelper.GenerateRefreshToken();

            // 4. Update user record
            user.refresh_token = newRefreshToken;
            user.token_exp_date = DateTime.UtcNow.AddSeconds(10);
            await _context.SaveChangesAsync();

            // 5. Return response
            response.Message = "Token refreshed successfully";
            response.ResponseData = new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return response;
        }


    }
}
