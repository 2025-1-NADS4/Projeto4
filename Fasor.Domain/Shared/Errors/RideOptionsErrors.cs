using ErrorOr;

namespace Fasor.Domain.Shared.Errors
{
    public class RideOptionsErrors
    {
        public static Error InvalidQuoteTime { get; } =
            Error.Validation(
                code: "Validation.InvalidQuoteTime",
                description: "O tempo da cotação expirou. Por favor, realize uma nova cotação.");
    }
}