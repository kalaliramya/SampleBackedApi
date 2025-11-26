using EF_API_Pg.Model;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samplebacked_api.EFCore;
using Samplebacked_api.EFCore.PatientEF;
using Samplebacked_api.Model.Patient;
using System.Drawing;

namespace Practice_Sol.Controllers
{
    [ApiController]  // This controller handles REST API requests — please auto-handle validation, binding, and responses
    [Authorize]
    public class PatientController : Controller
    {
        private readonly PatientDbHelper _db;
        public PatientController(patientDbContext eF_DataContext,PatientDbHelper patientDbHelper)
        {
            _db = patientDbHelper;  
                
        }

        [HttpGet]
        [Route("api/[controller]/GetPatientList")]
        public async Task<IActionResult> Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ApiResponse data = await _db.Get();
                return Ok(ResponseHandler.GetAppResponse(type, data.ResponseData));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }





        [HttpGet]
        [Route("api/[controller]/GetPatientbyId/{id}")]  // here id call it as [FromRoute]
        public async Task<IActionResult> GetPatientbyId(int id)   //here id call it as [FromQuery]
        {
            ResponseType type = ResponseType.Success;
            try
            {
                patientmodel data = await _db.GetPatientbyId(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/SavePatient")]
        public async Task<IActionResult> SavePatient([FromBody] patientmodel patientmodel)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _db.Savepatient(patientmodel);
                return Ok(ResponseHandler.GetAppResponse(type, patientmodel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPost]
        [Route("api/[controller]/SavePatientList")]
        public async Task<IActionResult> SavePatientList([FromBody] List<Patient> model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
               await _db.SavepatientList(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdatePatientList")]
        public async Task<IActionResult> UpdatePatientList([FromBody] List<Patient> model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                 await _db.UpdatepatientList(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPatch]
        [Route("api/[controller]/UpdatePatientColumn")]
        public IActionResult UpdatePatientColumn(int id, string name)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.Updatepatientcolumn(id,name);
                return Ok(ResponseHandler.GetAppResponse(type, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeletePatient")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _db.DeletePatient(id);
                return Ok(ResponseHandler.GetAppResponse(type, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }


    }
}
