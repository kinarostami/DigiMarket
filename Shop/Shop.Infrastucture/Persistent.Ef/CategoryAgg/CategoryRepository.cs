using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.CategoryAgg;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category = await Context.Categories
            .Include(x => x.Child)
            .ThenInclude(x => x.Child).FirstOrDefaultAsync(x => x.Id == categoryId);

        var IsExistProduct = await Context.Products.AnyAsync(x =>
                   x.CatgeoryId == categoryId || x.SubCategoryId == categoryId || x.SecondarySubCategoryId == categoryId);

        if (IsExistProduct)
            return false;

        if (category.Child.Any(x => x.Child.Any()))
        {
            Context.RemoveRange(category.Child.SelectMany(x => x.Child));
        }
        Context.RemoveRange(category.Child);
        Context.RemoveRange(category);
        return true;
    }
}
