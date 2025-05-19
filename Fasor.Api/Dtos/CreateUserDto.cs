public class CreateUserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime DateBirth { get; set; }
    public Guid CompanyId { get; set; }
}
