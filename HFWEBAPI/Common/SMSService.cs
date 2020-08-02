using Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HFWEBAPI.Common
{
    public class SMSService
    {
        private IConfiguration _config;
        
        public SMSService(IConfiguration config)
        {
            _config = config;
            string accountSID = _config.GetValue<string>("Values:TWILIO_ACCOUNT_SID"); // GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = _config.GetValue<string>("Values:TWILIO_AUTH_TOKEN"); //GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            // Initialize the TwilioClient.
            TwilioClient.Init(accountSID, authToken);
        }

        public MessageResource SendSMS(string bodymessage, string toPhone)
        {
            // Send an SMS message.
            //var bodymessage = "Reminder! You have an appointment today with " + app.modifiedBy + " at " + app.from + ". Thank you, Holistic Fitness";
            var message = MessageResource.Create(
                to: new PhoneNumber("+91" + toPhone),
                from: new PhoneNumber(_config.GetValue<string>("Values:TWILIO_PHONE_NUMBER")),
                body: bodymessage);

            return message;
        }
    }
}
