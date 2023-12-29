using POS.Core.DTO;
using POS.Core.Utilities;
using Microsoft.AspNetCore.Http;

namespace POS.Core
{

    public class LoyaltyCardService : ILoyaltyCardService
    {
        private readonly DB.AppDbContext _context;
        private readonly DB.Models.User _user;

        public LoyaltyCardService (DB.AppDbContext context, IHttpContextAccessor httpContextAcessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Name == httpContextAcessor.HttpContext.User.Identity.Name);
        }

        public LoyaltyCard CreateLoyaltyCard(CreateLoyaltyCardRequest request)
        {
            // Validate the request if needed

            // Fetch the user and loyalty program
            var user = _context.Users.Find(_user.Id);
            var loyaltyProgram = _context.LoyaltyPrograms.Find(request.LoyaltyProgramId);

            // Check if the user and loyalty program exist
            if (user == null || loyaltyProgram == null)
            {
                // Handle the case where the user or loyalty program is not found
                // You can throw an exception or handle it based on your application's logic
                throw new InvalidOperationException("User or LoyaltyProgram not found.");
            }


            // calculate total amount of cards user already has (to generate a new one - we use a count as an suffix in card code)
            int cardCount = GetUserCardCount(_user.Id);

            // Create a new LoyaltyCard
            var loyaltyCard = new DB.Models.LoyaltyCard
            {
                CardCode = CardCodeGenerator.GenerateCardCode(DateTime.UtcNow, user.Name, user.Id, cardCount, request.LoyaltyProgramId), // Card Code generation method implemented
                User = user,
                LoyaltyPoints = 0, // It can be initialised as we want
                LoyaltyProgram = loyaltyProgram,
                LoyaltyProgramId = request.LoyaltyProgramId,
                UserId = user.Id
            };

            // Add the loyalty card to the context
            _context.LoyaltyCards.Add(loyaltyCard);

            // Save changes to the database
                _context.SaveChanges();

            return (LoyaltyCard)loyaltyCard;
        }

        public LoyaltyCard GetLoyaltyCardByCardCode(string cardCode) =>
            _context.LoyaltyCards
                .Where(l => l.CardCode == cardCode)
                .Select(l => (LoyaltyCard)l)
                .First();

        public LoyaltyCard GetLoyaltyCardById(int id) => 
            _context.LoyaltyCards
                .Where(l => l.Id == id)
                .Select(l => (LoyaltyCard)l)
                .First();

        public List<LoyaltyCard> GetLoyaltyCards() => 
            _context.LoyaltyCards
                .Select(l => (LoyaltyCard)l)
                .ToList();

        public List<LoyaltyCard> GetLoyaltyCardsByLoyaltyProgramId(int loyaltyProgramId) =>
            _context.LoyaltyCards
                .Where(l => l.LoyaltyProgramId == loyaltyProgramId)
                .Select(l => (LoyaltyCard)l)
                .ToList();

        public List<LoyaltyCard> GetLoyaltyCardsByUserId(int userId) =>
            _context.LoyaltyCards
                .Where(l => l.UserId == userId)
                .Select(l => (LoyaltyCard)l)
                .ToList();


        // Helper function implemented to support card code generation sequence
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
