namespace Loto3000.Domain.Models
{
    public class Player : User
    {
        public Player() { }
        public Player(string firstName, string lastName, string username, string password, string email, double credits, DateTime dateOfBirth, IList<Ticket> tickets) 
            : base(firstName, lastName, username, password)
        {
            Email = email;
            Credits = credits;
            DateOfBirth = dateOfBirth;
            Tickets = tickets;
        }
        //public void CombinationGenerator(int[] nums)
        //{
        //    IList<Combination> combs = new List<Combination>();
            
        //    if (nums.Length < 7)
        //    {
        //        throw new Exception("Ticket combination must contain 7 numbers.");
        //    }

        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        Combination combination = new Combination();

        //        combination.Id = i;
        //        if (nums[i] < 1 || nums[i] > 37)
        //        {
        //            throw new Exception("Number is not valid, please choose a number between 1 and 37");
        //        }
        //        combination.Number = nums[i];

        //        combs.Add(combination);
        //    }

        //    Combination = combs;
        //}
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        //public IList<Combination> Combination { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
    }
}