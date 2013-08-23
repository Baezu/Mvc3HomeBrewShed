using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3HomeBrewShed.Utils
{
    public static class General
    {
       //-- added from asus-ltop

        /// <summary>
        /// GenerateHumanFriendlyPassword
        /// </summary>
        /// <returns>New Password</returns>
        public static string GenerateHumanFriendlyPassword()
        {
            string testPassword = "Password1";

            string[] passwordRoots = { "Pennsylvania", "Missouri", "Kansas", "Washington", "Alabama",
        "California", "Portland", "Texas", "Nebraska", "Wisconsin" };

            Random r = new Random(DateTime.Now.Millisecond);
            int random = r.Next(10, 1000);
            int root = random % 10;
            // return passwordRoots[root] + random.ToString();

            return testPassword;
        }
    }   
}