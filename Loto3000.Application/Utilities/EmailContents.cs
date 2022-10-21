namespace Loto3000.Application.Utilities
{
    internal static class EmailContents
    {
        internal const string RegisterSubject = "Welcome to Loto3000";
        internal const string ForgotPasswordSubject = "Loto3000 Password Recovery";
        internal const string NonregisteredPlayerTicketSubject = "Loto3000 Ticket Invoice";
        internal const string SuspendSubject = "Your Loto3000 account has been suspended";
        internal const string SuspendBody = "<p>The account on Loto3000 connected with this email has been suspended.</p>" +
            "<p>Reasons for this suspension may be some of the following:</p>" +
            "<ul> <li>Suspicius activity</li> <li>Underage gambling</li> <Long period of inactivity</li> <li>Other</li> </ul>" +
            "<p>If you wish further details you can address this issue <a href=\"mailto:support@loto3000.com\">here</a></p>" +
            "<p>Or, you can always open new account which will follow our rules of conduct.</p>";
        internal static string RegisterBody(string name, string code)
        {
            string body = $"<p>Hello {name}.</p> <p>Thank you for registering and welcome to Loto3000.</p> <p>Your verification code is <b>{code}</b></p>";
            return body;
        }
        internal static string ForgotPasswordBody(string code)
        {
            string body = $"<p>Use <b>{code}</b> as your password recovery code.</p>";
            return body;
        }
        internal static string NonregisteredPlayerTicketBody(string name, string combination, string idCode, string dateCreated, string drawTime)
        {
            string body = $"<p>Hello {name}.</p> <p>Thank you for using Loto3000 online services.</p> " +
                $"<p>You have successfully created a ticked which is eligible for the draw on {drawTime}</p> <p>Ticket details: </p> " +
                $"<ul> <li>Ticket combination: {combination}</li> <li>Created on: {dateCreated}</li> <li>Ticket identification code: {idCode}</li> </ul>" + 
                "<p><b>Please save this invoice in case you have a winning combination.</b></p>" +
                "<p>We encourage you to consider creating an account to enjoy all the benefits our registered players have.</p>";
            return body;
        }
    }
}