using MomenticAPI.Models;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using System.Data.Entity;

namespace MomenticAPI.Controllers
{
    public class PersonForgotPasswordController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        public async Task<object> PostForgotPassword(PersonForgotPassword personForgotPassowrd)
        {
            dynamic cResponse = new ExpandoObject();
            try
            {
                // code uret mail gonder
                // person tablosunda yeni code'u kaydet
                // sifreyi degistirince code'u sifirla

                string email = "";
                string username = "";

                if (personForgotPassowrd.Username.IndexOf("@") == -1)
                {
                    username = personForgotPassowrd.Username;
                }
                else
                {
                    email = personForgotPassowrd.Username;
                }

                Person person;

                if (email != null)
                {
                    person = await db.Person.Where(x => x.Username == username).FirstOrDefaultAsync();
                }
                else if (email != null)
                {
                    person = await db.Person.Where(x => x.Email == email).FirstOrDefaultAsync();
                }
                else
                {
                    cResponse.Result = "-1";
                    cResponse.Descripton = "Bad request";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }

                if (person != null)
                {
                    if (personForgotPassowrd.NewPassword == null)
                    {

                        Random r = new Random();
                        int code = r.Next(10000, 99999);

                        person.Code = code;
                        await db.SaveChangesAsync();

                        SendEmail(person.Email, person.FirstName + " " + person.LastName, code);

                        cResponse.Result = "0";
                        cResponse.Descripton = "Email sent";
                        return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));

                    }
                    else
                    {
                        if (person.Code == personForgotPassowrd.Code)
                        {
                            person.Code = null;
                            person.Password = personForgotPassowrd.NewPassword;
                            person.LastLoginDate = DateTime.Now;
                            await db.SaveChangesAsync();

                            cResponse.Result = "0";
                            cResponse.Person = person;
                            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                        }
                        else
                        {
                            cResponse.Result = "-1";
                            cResponse.Descripton = "Code not matched";
                            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                        }
                    }
                }
                else
                {
                    cResponse.Result = "-1";
                    cResponse.Descripton = "Email or Username not matched";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Descripton = "Your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        private static void SendEmail(string email, string fullname, int code)
        {
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Port = 587;   //Smtp Portu (Sunucuya Göre Değişir)
            smtpclient.Host = "smtp.gmail.com";  //Smtp Hostu (Gmail smtp adresi bu şekilde)
            smtpclient.EnableSsl = true;   //Sunucunun SSL kullanıp kullanmadıgı
            smtpclient.Credentials = new NetworkCredential("noreplymomentic@gmail.com", "diablo2013"); //Gmail Adresiniz ve Şifreniz
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreplymomentic@gmail.com", "Momentic App"); //Gidecek Mail Adresi ve Görünüm Adınız
            mail.To.Add(email); //Kime Göndereceğiniz
            mail.Subject = "Forgot Password Request";    //Emailin Konusu
            mail.Body += "Dear " + fullname + ", ";
            mail.Body += " <br/><br/> Your Code: " + code;
            mail.Body += " <br/><br/> Cya ";
            //mail.Body += " <br/><br/> Siparişin detayı için: <br/> <a href=\"http://www.hayalimdekipasta.com/yonetici/giris.aspx\" target=\"_blank\">Şef Paneline Git</a>";
            mail.IsBodyHtml = true;           //Mesajınızın Gövdesinde HTML destegininin olup olmadıgı
            mail.BodyEncoding = Encoding.UTF8;
            smtpclient.Send(mail);
        }

    }
}
