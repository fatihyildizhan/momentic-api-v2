using MomenticAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI
{
    public static class BasicHelper
    {
        public static string TokenCreate(int personID)
        {
            MomenticEntities db = new MomenticEntities();
            try
            {
                string temp = Guid.NewGuid().ToString();

                PersonToken pt = new PersonToken();
                pt.DateCreated = DateTime.Now;
                pt.DateLastLogin = DateTime.Now;
                pt.PersonID = personID;
                pt.Token = temp;

                db.PersonToken.Add(pt);
                db.SaveChanges();

                return temp;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string TokenCheck(string token)
        {
            MomenticEntities db = new MomenticEntities();
            try
            {
                PersonToken pt = db.PersonToken.Where(x => x.Token == token).SingleOrDefault();
                if (pt != null)
                {
                    pt.DateLastLogin = DateTime.Now;
                    db.SaveChanges();
                    return "0";
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
    }
}