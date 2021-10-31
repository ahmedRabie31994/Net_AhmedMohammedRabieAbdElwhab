using Antlr.Runtime;
using Net_AhmedMohammedRabieAbdElwhab.BL.Managers.User.Interface;
using Net_AhmedMohammedRabieAbdElwhab.BL.Managers.User.UserManager;
using Net_AhmedMohammedRabieAbdElwhab.BL.ViewModels;
using Net_AhmedMohammedRabieAbdElwhab.BL.ViewModels.User;
using Net_AhmedMohammedRabieAbdElwhab.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Net_AhmedMohammedRabieAbdElwhab.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
       private readonly  IUser manager;
        private readonly SendEmailHelper _sendEmailHelper;
        public UserController()
        {
            manager = new UserManager();
            _sendEmailHelper = new SendEmailHelper();
        }
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var list = manager.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetById/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var user = manager.GetById(id);
                if (user != null)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Creat(UserDTO Param)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bool query= manager.Create(Param);
                if (query)
                {
                    _sendEmailHelper.sendEmailConfirmation(Param.Email);
                    return Ok("created successfully ");
                }
                    
                else
                    return BadRequest("error occured userName or Email is already used");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(UserDTO Param)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var query = manager.Update(Param);
                if (query)
                    return Ok("Updated successfully");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("ConfirmEmail")]
        [HttpGet]
        public IHttpActionResult ConfirmEmail(string Email)
        {
            try
            {
               
                var query = manager.ConfirmEmail(Email);
                if (query)
                    //return Url("")
                    return Ok("Confirmed successfully");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("sendMessage")]
        [HttpPost]
        public IHttpActionResult sendMessage(UserDTO user)
        {
            try
            {
                _sendEmailHelper.sendEmailConfirmation(user.Email);
               
                
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [Route("JoinDamenForm")]
        [HttpPost]
        public  IHttpActionResult JoinDamenForm ()
        {
            try
            {
                JoinDamenFormViewModel model = new JoinDamenFormViewModel();
                model.Area=HttpContext.Current.Request.Params["Area"];
                model.Governorate = HttpContext.Current.Request.Params["Governorate"];
                model.describe = HttpContext.Current.Request.Params["describe"];
                model.Email = HttpContext.Current.Request.Params["Email"];
                model.firstName = HttpContext.Current.Request.Params["firstName"];
                model.lastName = HttpContext.Current.Request.Params["lastName"];
                model.phone = HttpContext.Current.Request.Params["phone"];
         
                /*
                 	ports
                    25, 587	(for unencrypted/TLS connections)
                    465	(for SSL connections)
                 
                 
                 */
                if (model == null)
                {
                    return BadRequest("model Cann't Be null");
                }
                var httpRequest = HttpContext.Current.Request;
                var docfiles = new List<string>();
                if (httpRequest.Files.Count > 0)
                {

                    foreach (string file in httpRequest.Files)
                    {
                        Guid generatedId = new Guid();
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadedImages/" + generatedId + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        filePath = "UploadedImages/" + generatedId + postedFile.FileName;
                        docfiles.Add(filePath);

                    }
                }

                string Email = "amrabie6767@gmail.com";
                MailMessage ms = new MailMessage(Email, Email);
                ms.Subject = "Join Damen";
                ms.Body = "<span> Hey Sir I am " + model.firstName + " " + model.lastName + "  </span>" + " <br /><span> My PhoneNumber : " + model.phone +"</span><br />"
                    + " <span> | My Mail Address  : "  + model.Email + "</span ><br />"
                     + " <span>| my Full Address is " + model.Area    + " "+
                     
                     model.Governorate + " | And this is about  my self " +
                     "</br> <span>" +
                     model.describe + "</span>";

              
                //foreach (var docfil in docfiles)
                //{
                //    ms.Attachments.Add(new System.Net.Mail.Attachment("http://localhost:54816/UploadedImages/" + docfil)); ;

                //}
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
                ms.IsBodyHtml = true;
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "apikey",
                    Password = "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",
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(ms);
                return Ok("you message sent succssesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Route("ContactUsForm")]
        [HttpPost]
        public IHttpActionResult ContactUsForm(ContactUsForm model)
        {
            try
            {

                /*
                 	ports
                    25, 587	(for unencrypted/TLS connections)
                    465	(for SSL connections)
                 
                 
                 */
                if (model == null)
                {
                    return BadRequest("model Cann't Be null");
                }
             

                string Email = "amrabie6767@gmail.com";
                MailMessage ms = new MailMessage(Email, Email);
                ms.Subject = "Contact Us Messagge";
                ms.Body = "Hey Sir I am " + model.Name + " " + " My PhoneNumber : " + model.Phone
                    + " | My Mail Address  : " + model.Email
                     + " my message: " + model.Message;

                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "apikey",
                    Password = "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",
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(ms);
                return Ok("you message sent succssesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Route("ApplyToJobForm")]
        [HttpPost]
        public IHttpActionResult ApplyToJobForm()
        {
            try
            {
                ApplyForJobForm model = new ApplyForJobForm();
                model.Address = HttpContext.Current.Request.Params["Address"];
                model.ContactNumber = HttpContext.Current.Request.Params["ContactNumber"];
                model.EmailAddress = HttpContext.Current.Request.Params["EmailAddress"];
                model.ExperienceYearsNumber = HttpContext.Current.Request.Params["ExperienceYearsNumber"];
                model.FullName = HttpContext.Current.Request.Params["FullName"];
                model.Gender = HttpContext.Current.Request.Params["Gender"];
                model.MaritalStatus = HttpContext.Current.Request.Params["MaritalStatus"];
                model.Title = HttpContext.Current.Request.Params["Title"];
                model.MilitaryStatus = HttpContext.Current.Request.Params["MilitaryStatus"];
                model.Experience = HttpContext.Current.Request.Params["Experience"];
                model.Education = HttpContext.Current.Request.Params["Education"];
                model.EducationalDegree = HttpContext.Current.Request.Params["EducationalDegree"];
                /*
                 	ports
                    25, 587	(for unencrypted/TLS connections) test
                    465	(for SSL connections)
                 
                 
                 */
                if (model == null)
                {
                    return BadRequest("model Cann't Be null");
                }

                var httpRequest = HttpContext.Current.Request;
                var docfiles = new List<string>();
                if (httpRequest.Files.Count > 0)
                {

                    foreach (string file in httpRequest.Files)
                    {
                        Guid generatedId = new Guid();
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadedImages/" + generatedId + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        filePath = "UploadedImages/" + generatedId + postedFile.FileName;
                        docfiles.Add(filePath);

                    }
                }
                string Email = "amrabie6767@gmail.com";
                MailMessage ms = new MailMessage(Email,Email);
                ms.Subject = "Apply To Job";
                ms.Body = "Hey Sir I am " +model.Title + " "  + model.FullName + " " + " My PhoneNumber : " + model.ContactNumber
                    + " | My Mail Address  : " + model.EmailAddress
                     + "| years Of experience : " + model.ExperienceYearsNumber + " |milatry Status is : " + model.MaritalStatus 
                     +  "| gender : " + model.Gender ;
                //foreach (var docfil in docfiles)
                //{
                //    ms.Attachments.Add(new System.Net.Mail.Attachment("http://localhost:54816/UploadedImages/" + docfil)); ;

                //}
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "apikey",
                    Password = "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",
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(ms);
                return Ok("you message sent succssesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Route("sendWithSendGrid")]
        [HttpPost]
        public async Task<IHttpActionResult> sendWithSendGrid(string name, string Email, string st)
        {
            try
            {


                var apiKey = Environment.GetEnvironmentVariable("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");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("test@example.com", "Example User");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("amrabie6767@gmail.com", "AhmedRabie3");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return Ok("you message sent succssesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
