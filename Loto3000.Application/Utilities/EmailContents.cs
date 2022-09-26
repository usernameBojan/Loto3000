namespace Loto3000.Application.Utilities
{
    public static class EmailContents
    {
        public const string RegisterSubject = "Welcome to Loto3000";
        public const string ForgotPasswordSubject = "Loto3000 Password Recovery";
        public const string SuspendSubject = "Your Loto3000 account has been suspended";
        public const string SuspendBody = "<p>The account on Loto3000 connected with this email has been suspended.</p>" +
            "<p>Reasons for this suspension may be some of the following:</p>" +
            "<ul ><li>Suspicius activity</li> <li>Underage gambling</li> <Long period of inactivity</li> <li>Other</li> </ul>" +
            "<p>If you wish further details you can address this issue <a href=\"mailto:support@loto3000.com\">here</a></p>" +
            "<p>Or, you can always open new account which will follow our rules of conduct.</p>";

        public static string RegisterBody(string name, string code)
        {
            string body = $"<p>Hello {name}.</p> </p>Thank you for registering and welcome to Loto3000.</p> <p>Your verification code is <b>{code}</b></p>";
            return body;
        }
        public static string ForgotPasswordBody(string code)
        {
            string body = $"<p>Use <b>{code}</b> as your password recovery code.</p>";
            return body;
        }
    }
}