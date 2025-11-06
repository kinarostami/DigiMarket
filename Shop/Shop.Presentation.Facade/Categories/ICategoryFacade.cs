using Common.Application;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult<long>> Create(CreateCategoryCommand command);
    Task<OperationResult<long>> AddChild(AddCategoryChildCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Remove(long categoryId);

    Task<CategoryDto> GetCategoryById(long id);
    Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId);
    Task<List<CategoryDto>> GetCategories();
}
