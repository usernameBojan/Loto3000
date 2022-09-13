namespace Loto3000.Application.Services
{
    public interface IPasswordHasher
    {
        private const int DefaultIterations = 10000;
        string HashToString(string clearText, int iterations = DefaultIterations);
        bool Verify(string clearText, string data);
    }
}