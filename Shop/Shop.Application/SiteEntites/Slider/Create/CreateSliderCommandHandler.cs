using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Slider.Create;

public class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
     readonly ISliderRepository _repository;
     readonly IFileService _fileService;

    public CreateSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImage);
        var slider = new Domain.SiteEntities.Slider(request.Title,request.Link,imageName);
        
        _repository.Add(slider);
        await _repository.Save();
        return OperationResult.Success();
    }
}