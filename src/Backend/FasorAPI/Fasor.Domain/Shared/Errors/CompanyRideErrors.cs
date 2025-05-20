using ErrorOr;
namespace Fasor.Domain.Aggregates;

public static class CompanyRideErrors
{
    public static readonly Error TradeNameIsRequired = Error.Validation(
        code: "CompanyRide.TradeNameRequired",
        description: "O nome da empresa de corrida é obrigatório."
    );
}