using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class PersonForgotPassword
    {
     //   public string Email { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public int Code { get; set; }
    }

    public class PersonEmail
    {
        //   public string Email { get; set; }
        public string Email { get; set; }
    }

    public class PersonUsername
    {
        //   public string Email { get; set; }
        public string Username { get; set; }
    }
}