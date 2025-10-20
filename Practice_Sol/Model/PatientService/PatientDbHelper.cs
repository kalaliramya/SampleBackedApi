using EF_API_Pg.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using Microsoft.AspNetCore.Mvc;
using Samplebacked_api.EFCore;
using System;
using System.Drawing;
namespace Samplebacked_api.Model.Patient
{
    public class PatientDbHelper
    {
        private patientDbContext _context;
        public PatientDbHelper(patientDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>


        public ApiResponse Get()
        {
            ApiResponse responce = new ApiResponse();

            var dataList = _context.patients.OrderBy(i => i.id).ToList();
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

        public patientmodel GetPatientbyId(string id)
        {
            patientmodel response = new patientmodel();
            var row = _context.patients.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (row != null) { }
            return new patientmodel()
            {
                // id = row.id,
                name = row.name,
                address = row.address,
                age = row.age,
                city = row.city,

            };
        }

        public ApiResponse Savepatient(patientmodel patientmodel)
        {
            ApiResponse response = new ApiResponse();

            EFCore.Patient dbtable = new EFCore.Patient();

            dbtable.name = patientmodel.name;
            dbtable.address = patientmodel.address;
            dbtable.age = patientmodel.age;
            dbtable.city = patientmodel.city;
            dbtable.gender = patientmodel.gender;
            dbtable.pin = patientmodel.pin;
            _context.patients.Add(dbtable);
            _context.SaveChanges();
            response.ResponseData = patientmodel;
            return response;
        }



        public ApiResponse SavepatientList(List<EFCore.Patient> model)
        {
            ApiResponse response = new ApiResponse();
            List<EFCore.Patient> patient = new List<EFCore.Patient>();

            _context.patients.AddRange(model);
            _context.SaveChanges();

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


        public ApiResponse UpdatepatientList(List<EFCore.Patient> model)
        {
            ApiResponse response = new ApiResponse();
            List<EFCore.Patient> patients = new List<EFCore.Patient>();

            foreach (var item in model)
            {
                EFCore.Patient patient = new EFCore.Patient();
                patient = _context.patients.Where(i => i.id == item.id).FirstOrDefault();
                patient.name = item.name;
                patient.address = item.address;
                patient.age = item.age;
                patient.city = item.city;
                patient.gender = item.gender;
                patient.pin = item.pin;

                patients.Add(patient);
            }
            _context.patients.UpdateRange(patients);
            _context.SaveChanges();

            response.Message = "Patients updated successfully";


            return response;
        }

        public ApiResponse Updatepatientcolumn(int id, int age)
        {
            ApiResponse response = new ApiResponse();

            List<EFCore.Patient> patients = new List<EFCore.Patient>();
            EFCore.Patient patient = new EFCore.Patient();
            patient = _context.patients.Where(i => i.id == id).FirstOrDefault();
            patient.age = age;
            _context.SaveChanges();

            response.Message = "Patients updated successfully";


            return response;
        }


        public ApiResponse DeletePatient(int id)
        {
            ApiResponse response = new ApiResponse();

            var patient = _context.patients.FirstOrDefault(p => p.id == id);

            _context.patients.Remove(patient);  // RemoveRange method used for list of delete records
            _context.SaveChanges();

            response.Message = "Patient deleted successfully";
            return response;
        }



        public void Generation()
        {
            GcPdfDocument document = new GcPdfDocument();
            GcPdfGraphics g = document.NewPage().Graphics;
            TextFormat tf = new TextFormat();
            tf.Font = StandardFonts.Times;
            tf.FontSize = 12;
            g.DrawString("Hello ram", tf, new PointF(72, 72));
            document.Save("ram.pdf");


        }
    }
}
