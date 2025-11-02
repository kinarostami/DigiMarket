using Common.Application;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.DeleteAddress;

public record DeleteUserAddressCommand(long UserId,long AddressId) : IBaseCommand
{
}
public class DeleteUserAddressHandler : IBaseCommandHandler<DeleteUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserAddressHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        user.DeleteAddress(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}
