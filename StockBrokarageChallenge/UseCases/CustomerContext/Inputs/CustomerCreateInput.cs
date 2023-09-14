using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.UseCases.CustomerContext.Inputs
{
    public class CustomerCreateInput
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf {  get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
