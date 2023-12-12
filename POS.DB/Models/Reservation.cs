namespace POS.DB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ResStart { get; set; }
        public DateTime ResEnd { get; set; }
        public bool? isReserved { get; set; }
        public User ProvidingEmployee { get; set; }
        public Item Service { get; set; }
        public User? User { get; set; }
    }
}
