using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using static PickUpSports.Models.TwilioAPIKey;
using System.Windows.Documents;
using PickUpSports.Models;


namespace PickUpSports.Controllers
{
    public class SMSController : TwilioController
    {
        public ApplicationDbContext db;
        public Player player;
        public SMSController()
        {
            db = new ApplicationDbContext();
        }
        //sends an automated message to phone
        
        public ActionResult SendSMS()
        {
            var accountSid = TwilioAcct;
            var authToken = TwilioToken;
            TwilioClient.Init(accountSid, authToken);

            
            var to = new PhoneNumber("+19202542672");
            var from = new PhoneNumber("+12562420890");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "An event in your interest has been created. Open the app to view the event.");
            return Content(message.Sid);
        }
    
    
        public ActionResult ReceiveSMS()
        {
            var response = new MessagingResponse();
            response.Message("");
            return TwiML(response);
        }

        
        public List GetPhoneNumbers(List phonenumbers) 
        {

            return phonenumbers;
        }

    }

}