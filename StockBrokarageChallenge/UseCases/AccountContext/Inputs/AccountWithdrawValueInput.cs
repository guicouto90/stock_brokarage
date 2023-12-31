﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs
{
    public class AccountWithdrawDepositValueInput
    {
        /// <summary>
        /// Gets or sets the value to deposit or withdraw. Default value is 50.0.
        /// </summary>
        [Required(ErrorMessage = "Value is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public double Value { get; set; } = 50.0;

        [JsonIgnore]
        public int CustomerId { get; set; }
    }
}
