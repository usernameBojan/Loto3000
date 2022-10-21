namespace Loto3000.Domain.Entities
{
    public class NonregisteredPlayerTicket : Ticket
    {
        public NonregisteredPlayerTicket() { }
        public NonregisteredPlayerTicket(NonregisteredPlayer player, Draw draw)
        {
            NonregisteredPlayer = player;
            Draw = draw;
        }
        public NonregisteredPlayer? NonregisteredPlayer { get; set; }
        public int? NonregisteredPlayerId { get; set; }
    }
}