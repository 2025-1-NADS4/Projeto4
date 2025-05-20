using ErrorOr;
using System.Runtime.CompilerServices;

namespace Fasor.Domain.Aggregates
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public List<RideQuote>? RideQuotes { get; set; } = new();


        public User() { }
        public User(string name, string surname, string cpf, string email, DateTime dateBirth, Guid companyId)
        {

            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Cpf = cpf;
            Email = email;
            DateBirth = dateBirth;
            CompanyId = companyId;
        }
    }
}
