using EF_API_Pg.Model;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samplebacked_api.Auth;
using Samplebacked_api.EFCore;
using Samplebacked_api.Model.Patient;
using Samplebacked_api.Model.UserService;



namespace Samplebacked_api.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserDbHelper userDbHelper;
        private readonly JwtService jwtdbhelper;
        public UserController(UserDbHelper user, JwtService jwtdb)
        {
            userDbHelper = user;
            jwtdbhelper = jwtdb;
        }

        // [HttpGet]
        //[Route("api/[controller]/GetAllRoles")]
        //public async Task<IActionResult> GetAllRoles(int? roleid)
        //{
        //    try
        //    {
        //        ResponseType type = ResponseType.Success;
        //        await userDbHelper.GetAllRoles(roleid);
        //        return Ok(ResponseHandler.GetAppResponse(type, DataMisalignedException.Res));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseHandler.GetExceptionResponse(ex));

        //    }
        //}



        [HttpGet]
        [Route("api/[controller]/GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(int? roleid)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ApiResponse data = await userDbHelper.GetAllRoles(roleid);
                return Ok(ResponseHandler.GetAppResponse(type, data.ResponseData));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        [HttpPost]
        [Route("api/[controller]/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await userDbHelper.CreateUser(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/ValidateUser")]

        public async Task<IActionResult> ValidateUser(string username, string pw)
        {
            try
            {
                ResponseType type = ResponseType.Success;

                ApiResponse data = await userDbHelper.ValidateUser(username, pw);
                return Ok(ResponseHandler.GetAppResponse(type, data.ResponseData));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/RefreshTokenGen")]
        public async Task<IActionResult> RefreshTokenGen(string request)
        {
            try
            {

                ResponseType type = ResponseType.Success;
                ApiResponse data = await userDbHelper.RefreshTokenGen(request);
                return Ok(ResponseHandler.GetAppResponse(type, data.ResponseData));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }



        }



}

