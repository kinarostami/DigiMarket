using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntites.Banner.Create;

public class CreateBannerCommand : IBaseCommand
{
    public string Link { get; set; }
    public IFormFile ImageName { get; set; }
    public BannerPosition Position { get; set; }
}
public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IFileService _fileService;

    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageName, Directories.BannerImage);
        var banner = new Domain.SiteEntities.Banner(request.Link, imageName, request.Position);
        _bannerRepository.Add(banner);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }
}
