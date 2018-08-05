using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApiExercise
{
    public class StudentSecurity
    {
        public static bool Login(string username, string password)
        {
            using(var db = new ApiDemoEntities())
            {
                return db.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
            }
        }
    }
}