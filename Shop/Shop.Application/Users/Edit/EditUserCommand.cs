using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Repsitory;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public EditUserCommand(long userId, IFormFile? avatar, string name, string family, string email, string phoneNumber, Gender gender)
    {
        UserId = userId;
        Avatar = avatar;
        Name = name;
        Family = family;
        Email = email;
        PhoneNumber = phoneNumber;
        Gender = gender;
    }
    public long UserId { get; set; }
    public IFormFile? Avatar { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }

}
public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IFileService _fileService;
    public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService, IFileService fileService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var oldImage = user.AvatarImage;
        user.Edit(request.Name, request.Name, request.Email, request.PhoneNumber, request.Gender);
        if (request.Avatar != null)
        {
           var imageName = await _fileService.SaveFileAndGenerateName(request.Avatar,Directories.UserAvatars);
            user.SetAvatar(imageName);
        }

        DeleteOldAvatar(request.Avatar, oldImage);
        await _userRepository.Save();
        return OperationResult.Success();
    }

    void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile == null || oldImage == "avatar.png")
            return;

        _fileService.DeleteFile(Directories.UserAvatars, oldImage);
    }
}
