using ErrorOr;
using Fasor.Domain.Shared.Errors;

namespace Fasor.Domain.Aggregates
{
    public class Company
    {
        public Guid Id { get; private set; }
        public string TradeName { get; private set; }
        public string Cnpj { get; private set; }
        public CompanyService CompanyServices { get; private set; }
        public bool IsActive { get; private set; }

        private Company() { }

        public Company(string tradeName, string cnpj, CompanyService? companyService)
        {
            SetTradeName(tradeName);
            SetCnpj(cnpj);
            CompanyServices = companyService;
            IsActive = true;
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
