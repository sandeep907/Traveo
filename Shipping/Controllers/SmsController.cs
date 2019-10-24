using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shipping.ViewModels.Sms;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1")]
    public class SmsController : Controller
    {
        // GET: Sms
        public class Response
        {
            public string message_id { get; set; }
            public int message_count { get; set; }
            public double price { get; set; }
        }

        public class RootObject
        {
            public Response Response { get; set; }
            public string ErrorMessage { get; set; }
            public int Status { get; set; }
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendSMS(ConfigurationSettings sm)
        {
            string API_KEY = sm.API_KEY;
            string API_SECRET = sm.API_SECRET;
            double TO = Convert.ToDouble(sm.To_Number);
            string Message = sm.TextMessage;
            string sURL;
            sURL = "https://www.thetexting.com/rest/sms/json/message/send?api_key=" + API_KEY + "&api_secret=" + API_SECRET + "&from=test" + "&to=" + TO + "&text=" + Message + "&type=text";
            if (ModelState.IsValid)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {

                        string s = client.DownloadString(sURL);
                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(s);
                        int n = responseObject.Status;
                        if (n == 3)
                        {
                            return Content("<script>alert('Message does not Send Successfully due to invalid credentials !');location.href='/';</script>");
                        }
                        else
                        {
                            return Content("<script>alert('Message Send Successfully !');location.href='/';</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error in sending Message");
                    ex.ToString();
                }
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error in sending Message");
                return View("Index");
            }
        }
    }
}       