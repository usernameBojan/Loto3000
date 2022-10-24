using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.Transactions
{
    public class BuyCreditsDto
    {
        [Required]
        public string CardHolderName { get; set; } = string.Empty;
        [Required]
        [RegularExpression( //REGEX USED https://ihateregex.io/expr/credit-card/
            "(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\\d{3})\\d{11}$)",
            ErrorMessage = "Provided info doesn't match the required credit card format."
            )]
        public string CreditCardNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(\\d{3})$", ErrorMessage = "Wrong CVV2/CVC2 code format.")]
        public string CVV2CVC2Code { get; set; } = string.Empty;    
        [Required]
        public DateTime CardExpirationDate { get; set; }
        [Required]
        [RegularExpression("^-?\\d+(?:\\.\\d+)?$", ErrorMessage = "Wrong deposit format.")]
        public double DepositAmount { get; set; }
        public double Credits => DepositAmount * 10;
    }
}