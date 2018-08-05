using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace CrudApiExercise.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            using(var db = new ApiDemoEntities())
            {
                return db.Students.ToList();
            }
        }


        // Web Api query string parameter.
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (var entities = new ApiDemoEntities())
            {
                switch (username.ToLower())
                {
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Students.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Students.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            using(var db = new ApiDemoEntities())
            {
                var stdnt = db.Students.FirstOrDefault(std => std.ID == id);

                if(stdnt != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, stdnt);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Student with id= " + id.ToString() + " was not found.");
                }
            }
        }




        [HttpPost]
        public HttpResponseMessage Post([FromBody]Student stdnt)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {

                    db.Students.Add(stdnt);
                    db.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.Created, stdnt);
                    return msg;
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {
                    var stdnt = db.Students.FirstOrDefault(s => s.ID == id);

                    if (stdnt != null)
                    {
                        db.Students.Remove(stdnt);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id = " + id.ToString() + " was not found to be deleted.");
                    }
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Student stdnt)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {
                    var std = db.Students.FirstOrDefault(s => s.ID == id);

                    if (std == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id = " + id.ToString() + " was not found to be updated.");
                    }
                    else
                    {
                        std.ID = stdnt.ID;
                        std.Name = stdnt.Name;
                        std.Gender = stdnt.Gender;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
