using Fasor.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.Users.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public IEnumerable<Company> Preferences { get; set; }
    }
}
