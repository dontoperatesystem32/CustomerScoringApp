﻿namespace ScoringSystem_web_api.Models.CustomerModels

{
    public class Account
    {
        public int Id { get; set; }
        public required Customer Customer { get; set; }

        public ICollection<float>? Loans { get; set; }
    }
}
