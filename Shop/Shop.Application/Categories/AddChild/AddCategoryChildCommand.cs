using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Categories.AddChild;

public class AddCategoryChildCommand : IBaseCommand<long>
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
    public long ParentId { get; set; }
}
public class AddCategoryChildCommandHandler : IBaseCommandHandler<AddCategoryChildCommand,long>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public AddCategoryChildCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult<long>> Handle(AddCategoryChildCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.ParentId);
        if (category == null)
            return OperationResult<long>.NotFound();

        category.AddChild(request.Slug,request.Title, request.SeoData, request.ParentId, _categoryDomainService);
        await _categoryRepository.Save();
        return OperationResult<long>.Success(category.Id);
    }
}
