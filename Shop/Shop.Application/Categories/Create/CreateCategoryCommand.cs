using Common.Application;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Categories.Create;

public class CreateCategoryCommand : IBaseCommand<long>
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
}
public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand,long  >
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Slug,request.Title,request.SeoData,_categoryDomainService);
        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return OperationResult<long>.Success(category.Id);
    }
}
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(r => r.Title).NotNull().Empty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug).NotNull().Empty().WithMessage(ValidationMessages.required("Slug"));
    }
}
