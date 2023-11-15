using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskAssignment.Controllers
{
    public class InternController : ApiController
    {
        [HttpPost ]
        public string PostData(Intern batch)
        {
            try
            {
                using (var db = new TaskassignmentEntities())
                {
                    var Idparam = new SqlParameter("Id", batch.Id);
                    var FirstNameparam = new SqlParameter("FirstName", batch.FirstName);
                    var LastNameparam = new SqlParameter("LastName", batch.lastName);
                    var Emailparam = new SqlParameter("Email", batch.Email);
                    var Genderparam = new SqlParameter("Gender", batch.Gender);
                    var Cityparam = new SqlParameter("City", batch.City);
                    var MobileNoparam = new SqlParameter("MobileNo", batch.MobileNo);

                    db.Database.ExecuteSqlCommand("exec InsertInternData @Id,@FirstName,@LastName,@Email,@Gender,@City,@MobileNo",
                                                                                                            Idparam,
                                                                                                            FirstNameparam,
                                                                                                            LastNameparam,
                                                                                                            Emailparam,
                                                                                                            Genderparam,
                                                                                                            Cityparam,
                                                                                                            MobileNoparam
                                                                                                       );
                    db.SaveChanges();
                    return "data inserted";
                }
            }
            catch(Exception e)
            {
                return "error" + e.Message;
            }```
        }

        [HttpGet]
        public IHttpActionResult GetById(int Id)
        {
            using(var dsb = new TaskassignmentEntities())
            {

                var interns = dsb.Interns.FirstOrDefault(i => i.Id == Id);
                return Ok(interns);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            using (var db = new TaskassignmentEntities())
            {
                //IList<Intern> interns = null;
                var interns = db.Database.SqlQuery<Intern>("exec GetAll").ToList();
                return Ok(interns);
            }
        }

        [HttpPut]
        public string UpdateData(Intern interns)
        {
            using (var db = new TaskassignmentEntities())
            {
                var data = db.Interns.Where(i => i.Id == interns.Id).FirstOrDefault();
                if(data != null)
                {
                    data.Id = interns.Id;
                    data.FirstName = interns.FirstName;
                    data.lastName = interns.lastName;
                    data.Email = interns.Email;
                    data.Gender = interns.Gender;
                    data.City = interns.City;
                    data.MobileNo = interns.MobileNo;

                    db.SaveChanges();
                    return "data updated";
                }
                else
                {
                    return "data not updated";
                }
            }
        }


        [HttpDelete]
        public string deleteStudent(int id)
        {
            using (var db = new TaskassignmentEntities())
            {
                var stu = db.Interns.FirstOrDefault(s => s.Id == id);
                if (stu != null)
                {
                    db.Interns.Remove(stu);
                    db.SaveChanges();
                    return "deleted";
                }

                return "not deleted";
            }
        }
    }
}
