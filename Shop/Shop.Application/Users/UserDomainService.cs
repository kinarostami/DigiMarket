using Shop.Domain.UserAgg.Repsitory;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsEmailExist(string email)
    {
        return _userRepository.Exists(x => x.Email == email);
    }

    public bool PhoneNumberIsExist(string phoneNumber)
    {
        return _userRepository.Exists(x => x.PhoneNumber ==  phoneNumber);
    }
}
