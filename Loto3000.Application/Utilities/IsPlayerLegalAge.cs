namespace Loto3000.Application.Utilities
{
    internal static class IsPlayerLegalAge
    {
        internal static bool VerifyAge(DateTime dateOfBirth)
        {
            int age = 0;

            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            if(dateOfBirth.Month > month)
            {
                age = year - dateOfBirth.Year - 1;
            }

            if(dateOfBirth.Month == month && dateOfBirth.Day > day)
            {
                age = year - dateOfBirth.Year - 1;
            }

            if(dateOfBirth.Month < month)
            {
                age = year - dateOfBirth.Year;
            }

            if(dateOfBirth.Month == month && dateOfBirth.Day <= day)
            {
                age = year - dateOfBirth.Year;
            }

            if(age < 18)
            {
                return false;
            }

            return true;
        }
    }
}