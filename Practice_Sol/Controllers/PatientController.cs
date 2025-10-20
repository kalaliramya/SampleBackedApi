using EF_API_Pg.Model;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samplebacked_api.EFCore;
using Samplebacked_api.Model.Patient;
using System.Drawing;

namespace Practice_Sol.Controllers
{
    [ApiController]  // This controller handles REST API requests — please auto-handle validation, binding, and responses
    public class PatientController : Controller
    {
        private readonly PatientDbHelper _db;
        public PatientController(patientDbContext eF_DataContext)
        {
            _db = new PatientDbHelper(eF_DataContext);
        }

        [HttpGet]
        [Route("api/[controller]/Get")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ApiResponse data = _db.Get();
                return Ok(ResponseHandler.GetAppResponse(type, data.ResponseData));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }





        [HttpGet]
        [Route("api/[controller]/GetPatientbyId/{id}")]  // here id call it as [FromRoute]
        public IActionResult GetPatientbyId(string id)   //here id call it as [FromQuery]
        {
            ResponseType type = ResponseType.Success;
            try
            {
                patientmodel data = _db.GetPatientbyId(id);
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
        [Route("api/[controller]/Savepatient")]
        public IActionResult Post([FromBody] patientmodel patientmodel)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.Savepatient(patientmodel);
                return Ok(ResponseHandler.GetAppResponse(type, patientmodel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPost]
        [Route("api/[controller]/SavepatientList")]
        public IActionResult SavepatientList([FromBody] List<Patient> model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SavepatientList(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdatepatientList")]
        public IActionResult UpdatepatientList([FromBody] List<Patient> model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdatepatientList(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpPatch]
        [Route("api/[controller]/Updatepatientcolumn")]
        public IActionResult Updatepatientcolumn(int id, int age)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.Updatepatientcolumn(id,age);
                return Ok(ResponseHandler.GetAppResponse(type, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeletePatient")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeletePatient(id);
                return Ok(ResponseHandler.GetAppResponse(type, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }


    }
}
