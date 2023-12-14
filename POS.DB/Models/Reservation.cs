namespace POS.DB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ResStart { get; set; }
        public DateTime ResEnd { get; set; }
        public bool? IsReserved { get; set; }
        public User ProvidingEmployee { get; set; }
        public Item Service { get; set; }
        public User? Customer { get; set; }
    }
}
