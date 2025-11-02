using Common.Application;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.AddToken;

public class AddUserTokenCommand : IBaseCommand
{
    public AddUserTokenCommand(long userId, string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        UserId = userId;
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public long UserId { get; }
    public string HashJwtToken { get; }
    public string HashRefreshToken { get; }
    public DateTime TokenExpireDate { get; }
    public DateTime RefreshTokenExpireDate { get; }
    public string Device { get; }
}
public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        user.AddToken(request.HashJwtToken,request.HashRefreshToken,request.TokenExpireDate,request.RefreshTokenExpireDate,request.Device);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}
