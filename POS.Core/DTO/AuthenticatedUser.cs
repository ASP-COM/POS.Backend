namespace POS.Core.DTO
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
