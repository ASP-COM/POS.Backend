﻿namespace POS.Core.DTO
{
    public class GetAvailableDaysRequest
    {
        public int? EmployeeId {get; set;}
        public int? ServiceId {get; set;}
        public DateOnly Start {get; set;}
        public DateOnly? End {get; set;}
    }
}