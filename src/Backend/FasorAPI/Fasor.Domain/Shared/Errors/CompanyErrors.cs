using ErrorOr;

namespace Fasor.Domain.Shared.Errors
{
    public class CompanyErrors
    {
        public static Error TradeNameIsRequired { get; } = 
            Error.Validation(
                code: "Validation.TradeNameIsRequired",
                description: "O Nome fantasia é obrigatorio.");

        public static Error CpnjFormatInvalid { get; } =
            Error.Validation(
                code: "Validation.CpnjFormatInvalid",
                description: "O CNPJ deve conter 14 digitos.");

        public static Error CompanyNotFound { get; } =
            Error.Validation(
                code: "Validation.CompanyNotFound",
                description: "A empresa não foi encontrada.");
    }
}
