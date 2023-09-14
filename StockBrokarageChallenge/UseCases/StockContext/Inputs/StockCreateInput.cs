using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.UseCases.StockContext.Inputs
{
    public class StockCreateInput
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string Code { get; set; }

        public StockCreateInput(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
