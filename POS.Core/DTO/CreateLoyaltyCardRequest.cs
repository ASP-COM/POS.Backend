﻿namespace POS.Core.DTO
{
    public class CreateLoyaltyCardRequest
    {
        // public int UserId { get; set; }// no need to include UserId in request body (http context accessor usage)

        public int LoyaltyProgramId { get; set; }

        // No need to let the user pass CardCode since the code will be generated by the system
    }
}