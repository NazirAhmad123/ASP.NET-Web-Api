using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudApiExercise.Controllers
{
    public class DeansListController : ApiController
    {
        [HttpGet]
        public IEnumerable<Deanslist> Get()
        {
            using(var db = new ApiDemoEntities())
            {
                return db.Deanslists.ToList();
            }
        }


        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {
                    var std = db.Deanslists.FirstOrDefault(s => s.ID == id);

                    return Request.CreateResponse(HttpStatusCode.OK, std);

                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Deanslist std)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {
                    db.Deanslists.Add(std);
                    db.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.Created);
                    return msg;
                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var db = new ApiDemoEntities())
                {
                    var std = db.Deanslists.FirstOrDefault(s => s.ID == id);

                    if (std != null)
                    {
                        db.Deanslists.Remove(std);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id=" + id.ToString() + " was not found to be deleted.");
                    }
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Deanslist std)
        {

            try
            {
                using (var db = new ApiDemoEntities())
                {
                    var stdnt = db.Deanslists.FirstOrDefault(s => s.ID == id);

                    if (stdnt != null)
                    {
                        stdnt.ID = std.ID;
                        stdnt.Name = std.Name;
                        stdnt.Grade = std.Grade;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id = " + id.ToString() + " wasn't found.");
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

