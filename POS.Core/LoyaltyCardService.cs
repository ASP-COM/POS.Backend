using POS.DB;
using POS.DB.Models;
using POS.Core.DTO;
using POS.Core.Utilities;

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


            // calculate total amount of cards user already has (to generate a new one - we use a count as an suffix in card code)
            int cardCount = GetUserCardCount(request.UserId);

            // Create a new LoyaltyCard
            var loyaltyCard = new LoyaltyCard
            {
                CardCode = CardCodeGenerator.GenerateCardCode(DateTime.UtcNow, user.Name, user.Id, cardCount, request.LoyaltyProgramId), // Card Code generation method implemented
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

        public int GetUserCardCount(int userId)
        {
            // Retrieve the user's cards from the database
            var userCards = _context.LoyaltyCards
                .Where(card => card.User.Id == userId)
                .ToList();

            // Calculate and return the count of user cards
            return userCards.Count;
        }
    }
}
