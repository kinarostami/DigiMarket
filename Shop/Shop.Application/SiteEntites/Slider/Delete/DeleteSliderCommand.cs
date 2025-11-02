using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Slider.Delete;

public record DeleteSliderCommand(long Id) : IBaseCommand;

public class DeleteSliderCommandHandler : IBaseCommandHandler<DeleteSliderCommand>
{
     readonly ISliderRepository _repository;
     readonly IFileService _fileService;

    public DeleteSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.Id);
        if (slider == null)
            return OperationResult.NotFound();

        _repository.Delete(slider);
        await _repository.Save();
        _fileService.DeleteFile(Directories.SliderImage, slider.ImageName);
        return OperationResult.Success();
    }
}
