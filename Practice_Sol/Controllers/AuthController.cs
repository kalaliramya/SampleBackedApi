using Microsoft.AspNetCore.Mvc;
using Samplebacked_api.Auth;
using Samplebacked_api.Model.UserService;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly UserDbHelper _userHelper;

    public AuthController(JwtService jwtService, UserDbHelper userHelper)
    {
        _jwtService = jwtService;
        _userHelper = userHelper;
    }

   
}
