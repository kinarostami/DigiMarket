using Common.Application;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.Create;

public class CreateUserCommand : IBaseCommand
{
    public CreateUserCommand(string name, string family, string phoneNumber, string email, string password, Gender gender)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }
    public string Name { get;  set; }
    public string Family { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
}