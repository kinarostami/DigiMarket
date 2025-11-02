using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Categories.Edit;

public class EditCategoryCommand : IBaseCommand
{
    public long CategoryId { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
}
public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var user = await _categoryRepository.GetTracking(request.CategoryId);
        if (user == null)
            return OperationResult.NotFound();

        user.Edit(request.Slug, request.Title, request.SeoData, _categoryDomainService);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}
