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
        
        public SMSController()
        {
            db = new ApplicationDbContext();
        }
             
        public ActionResult SendSMSToPlayers() 
        {
            var accountSid = TwilioAcct;
            var authToken = TwilioToken;
            var notifyServiceSid = ServiceSidKey;
            TwilioClient.Init(accountSid, authToken);

            var phonenumbers = PhoneNumbers.PlayersPhoneNumbers;

           

            foreach (var number in phonenumbers)
            {
                var message = MessageResource.Create(
                    body: "An event in your interest has been created. Open the app to view the event.",
                    from: new PhoneNumber("+12562420890"),
                    to: new PhoneNumber(number)
                );

                Console.WriteLine($"Message to {number} has been {message.Status}.");
                
                
            }
            return View("PlayerInterestEvents", "Events");
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