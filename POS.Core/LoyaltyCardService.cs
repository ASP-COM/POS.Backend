using POS.DB;
using POS.DB.Models;
using POS.Core.DTO;

namespace POS.Core
{

    public class LoyaltyCardService : ILoyaltyCardService
    {
        private readonly AppDbContext _context;

        public LoyaltyCardService (AppDbContext context)
        {
            _context = context;
        }

        public LoyaltyCard CreateLoyaltyCard(CreateLoyaltyCardRequest request)
        {
            // Validate the request if needed

            // Fetch the user and loyalty program
            var user = _context.Users.Find(request.UserId);
            var loyaltyProgram = _context.LoyaltyPrograms.Find(request.LoyaltyProgramId);

            // Check if the user and loyalty program exist
            if (user == null || loyaltyProgram == null)
            {
                // Handle the case where the user or loyalty program is not found
                // You can throw an exception or handle it based on your application's logic
                throw new InvalidOperationException("User or LoyaltyProgram not found.");
            }

            // Create a new LoyaltyCard
            var loyaltyCard = new LoyaltyCard
            {
                CardCode = GenerateCardCode(), // Method implementation needed
                User = user,
                LoyaltyPoints = 0, // It can be initialised as we want
                LoyaltyProgram = loyaltyProgram,
                LoyaltyProgramId = request.LoyaltyProgramId,
                UserId = request.UserId
            };

            // Add the loyalty card to the context
            _context.LoyaltyCards.Add(loyaltyCard);

            // Save changes to the database
                _context.SaveChanges();

            return loyaltyCard;
        }

        private string GenerateCardCode()
        {
            // Implement logic to generate a unique card code
            // This can be based on your specific requirements, such as a combination of user and program details
            return "GeneratedCardCode"; // Replace this with your actual logic
        }

        public LoyaltyCard GetLoyaltyCardByCardCode(string cardCode) => _context.LoyaltyCards.FirstOrDefault(l => l.CardCode == cardCode);

        public LoyaltyCard GetLoyaltyCardById(int id) => _context.LoyaltyCards.FirstOrDefault(l => l.Id == id);

        public List<LoyaltyCard> GetLoyaltyCards() => _context.LoyaltyCards.ToList();

        public List<LoyaltyCard> GetLoyaltyCardsByLoyaltyProgramId(int loyaltyProgramId)
        {
            return _context.LoyaltyCards.Where(l => l.LoyaltyProgramId == loyaltyProgramId).ToList();
        }

        public List<LoyaltyCard> GetLoyaltyCardsByUserId(int userId)
        {
            return _context.LoyaltyCards.Where(l => l.UserId == userId).ToList();
        }
    }
}
