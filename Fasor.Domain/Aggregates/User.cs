using System.Reflection;
using System.Security.Cryptography;

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
        public IEnumerable<Company> Preferences { get; set; }


        public User(string name, string surname, string cpf, string email, DateTime DateBirth, IEnumerable<Company> Preferences)
        {

            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Cpf = cpf;
            Email = email;
            DateBirth = this.DateBirth;
            Preferences = this.Preferences;
            
        }


    }
}
