using EF_API_Pg.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samplebacked_api.EFCore;
using System;
using System.Drawing;
namespace Samplebacked_api.Model.Patient
{
    public class PatientDbHelper
    {
        private patientDbContext _context;
        //private readonly e
        public PatientDbHelper(patientDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>


        public async Task<ApiResponse> Get()
        {
            ApiResponse responce = new ApiResponse();

            var dataList = await _context.patients.AsNoTracking().OrderBy(i => i.patient_id).ToListAsync();
            responce.ResponseData = dataList;


            // worest practice case 2
            /*  List <patientmodel> response = new List<patientmodel>();
              dataList.ForEach(row => response.Add(new patientmodel()
             {
                 id = row.id,
                 name = row.name,
                 address = row.address,
                 age = row.age,
                 city = row.city,
                 gender = row.gender,
                 pin = row.pin

             }));   */
            return responce;
        }

        public async Task<patientmodel> GetPatientbyId(int id)
        {
            var row = await _context.patients.FindAsync(id); // FindAsnyc used only primary key column searching

            //var row = await _context.patients.Where(d => d.patient_id.Equals(id) && d.is_active == true).FirstOrDefaultAsync();
           if (row == null)
            {
                return null;
            }
                return new patientmodel()
                {
                    full_name = row.full_name,
                    phone_number = row.phone_number,
                    email = row.email,
                    dob = row.dob,
                    gender_id = row.gender_id,
                    address_line = row.address_line,
                    city = row.city,
                    state = row.state,
                    pin_code = row.pin_code,
                    created_by = row.created_by,
                    updated_by = row.updated_by,

                };
            
        }

        public async Task<ApiResponse> Savepatient(patientmodel patientmodel)
        {
            ApiResponse response = new ApiResponse();

            EFCore.PatientEF.Patient patient = new EFCore.PatientEF.Patient();

            patient.full_name = patientmodel.full_name;
            patient.phone_number = patientmodel.phone_number;
            patient.email = patientmodel.email;
            patient.dob = patientmodel.dob;
            patient.gender_id = patientmodel.gender_id;
            patient.address_line = patientmodel.address_line;
            patient.city = patientmodel.city;
            patient.state = patientmodel.state;
            patient.pin_code = patientmodel.pin_code;
            patient.is_active = patientmodel.is_active;
            patient.created_by = patientmodel.created_by;
            patient.created_date = DateTime.UtcNow;
            patient.updated_by = patientmodel.updated_by;
            patient.updation_date = DateTime.UtcNow;
             _context.patients.Add(patient);
            await _context.SaveChangesAsync();
            response.ResponseData = patientmodel;
            return response;
        }



        public async Task<ApiResponse> SavepatientList(List<EFCore.PatientEF.Patient> model)
        {
            ApiResponse response = new ApiResponse();
            List<EFCore.PatientEF.Patient> patient = new List<EFCore.PatientEF.Patient>();

           await _context.patients.AddRangeAsync(model);
            await _context.SaveChangesAsync();

            /* case 2 exicution
            foreach (patientmodel pt  in patientlist)
            {
                Patient dbtable = new Patient();
                dbtable.id = pt.id;
                dbtable.name = pt.name;
                dbtable.address = pt.address;
                dbtable.age = pt.age;
                dbtable.city = pt.city;
                dbtable.gender = pt.gender;
                dbtable.pin = pt.pin;
                _context.patients.Add(dbtable);
                _context.SaveChanges();
            } 
            */
            return response;

        }


        public async Task<ApiResponse> UpdatepatientList(List<EFCore.PatientEF.Patient> model)
        {
            ApiResponse response = new ApiResponse();
            List<EFCore.PatientEF.Patient> patients = new List<EFCore.PatientEF.Patient>();

            foreach (var item in model)
            {
                EFCore.PatientEF.Patient patient = new EFCore.PatientEF.Patient();
                patient = _context.patients.Where(i => i.patient_id == item.patient_id && i.is_active == true).SingleOrDefault();

                patient.full_name = item.full_name;
                patient.phone_number = item.phone_number;
                patient.email = item.email;
                patient.dob = item.dob;
                patient.gender_id = item.gender_id;
                patient.address_line = item.address_line;
                patient.city = item.city;
                patient.state = item.state;
                patient.pin_code = item.pin_code;
                patient.updated_by = item.updated_by;
                patient.updation_date = DateTime.UtcNow;

                patients.Add(patient);
            } 
            _context.patients.UpdateRange(patients);
            await _context.SaveChangesAsync();

            response.Message = "Patients updated successfully";


            return response;
        }

        public async Task<ApiResponse> Updatepatientcolumn(int id, string name)
        {
            ApiResponse response = new ApiResponse();

            List<EFCore.PatientEF.Patient> patients = new List<EFCore.PatientEF.Patient>();
            EFCore.PatientEF.Patient patient = new EFCore.PatientEF.Patient();
            patient = _context.patients.Where(i => i.patient_id == id).FirstOrDefault();
            patient.full_name = name;
            await _context.SaveChangesAsync();

            response.Message = "Patients updated successfully";


            return response;
        }


        public async Task<ApiResponse> DeletePatient(int id)
        {
            ApiResponse response = new ApiResponse();

            var patient = await _context.patients.FirstOrDefaultAsync(p => p.patient_id == id);

            _context.patients.Remove(patient);  // RemoveRange method used for list of delete records
           await _context.SaveChangesAsync();

            response.Message = "Patient deleted successfully";
            return response;
        }

        
    }
}
