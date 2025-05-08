using ErrorOr;
using Fasor.Domain.Shared.Errors;

namespace Fasor.Domain.Aggregates
{
    public class Company
    {
        public Guid Id { get; private set; }
        public string TradeName { get; private set; }
        public string Cnpj { get; private set; }
        public List<CompanyAppService>? CompanyAppServices { get; private set; }
        public bool IsActive { get; private set; }

        private Company() { }
        private Company(string tradeName, string cnpj)
        {
            Id = Guid.NewGuid();
            TradeName = tradeName;
            Cnpj = cnpj;
            IsActive = true;
        }
        public static ErrorOr<Company>Create(string tradeName, string cnpj)
        {
            List<Error> errors = new();

            var company = new Company();

            var tradeNameResult = company.SetTradeName(tradeName);
            if (tradeNameResult.IsError)
                errors.AddRange(tradeNameResult.Errors);

            var cnpjResult = company.SetCnpj(cnpj);
            if (cnpjResult.IsError)
                errors.AddRange(cnpjResult.Errors);

            if (errors.Any())
                return errors;

            company.IsActive = false;

            return company;
        }

        public void Inactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public ErrorOr<string>SetTradeName(string tradeName)
        {
            if (string.IsNullOrWhiteSpace(tradeName))
                return CompanyErrors.TradeNameIsRequired;

            return TradeName = tradeName;
        }

        public ErrorOr<string> SetCnpj(string cnpj)
        {
            if (!ValidateCnpj(cnpj))
                return CompanyErrors.CpnjFormatInvalid;

            return Cnpj = cnpj;
        }

        public void UpdateDetails(string tradeName, string cnpj)
        {
            SetTradeName(tradeName);
            SetCnpj(cnpj);
        }

        public bool ValidateCnpj(string cnpj)
        {
            return !string.IsNullOrEmpty(cnpj) && cnpj.Length == 14;
        }
    }

}
