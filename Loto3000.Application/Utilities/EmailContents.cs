namespace Loto3000.Application.Utilities
{
    public static class EmailContents
    {
        public const string RegisterSubject = "Welcome to Loto3000";
        public const string ForgotPasswordSubject = "Loto3000 Password Recovery";
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