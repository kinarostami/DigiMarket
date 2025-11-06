using Common.Application;
using MediatR;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.ChangeWallet;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetPhoneNumber;
using Shop.Query.Users.UserTokens.GetByJwtToken;
using Shop.Query.Users.UserTokens.GetByRefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
    Task<OperationResult> ChangeWallet(ChangeUserWalletCommand command);
    Task<OperationResult> Create(CreateUserCommand command);
    Task<OperationResult> Edit(EditUserCommand command);
    Task<OperationResult> Register(RegisterUserCommand command);
    Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);

    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDto?> GetUserById(long userId);
    Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken);
    Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);
    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
}
public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangeWallet(ChangeUserWalletCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        return await _mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        return await _mediator.Send(new GetUserTokenByJwtTokenQuery(jwtToken));
    }

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
    {
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(refreshToken));
    }

    public async Task<OperationResult> Register(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }
}