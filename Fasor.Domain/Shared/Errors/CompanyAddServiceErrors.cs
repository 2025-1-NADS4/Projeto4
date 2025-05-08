using ErrorOr;

namespace Fasor.Domain.Shared.Errors
{
    public class CompanyAddServiceErrors
    {
        public static Error NameServiceIsRequired { get; } =
           Error.Validation(
               code: "Validation.NameServiceIsRequired",
               description: "O Nome do serviço é obrigatorio.");
    }
}
