using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories;
using Shop.Application.Categories.Create;
using Shop.Application.Products;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Service;
using Shop.Domain.UserAgg.Service;
using Shop.Infrastucture;

namespace Shop.Config;

public class ShopBootstrapper
{
    public static void RegisterShopDependency(IServiceCollection services,string connectionString)
    {
        InfrstuctureBootstrapper.Init(services,connectionString);

        services.AddTransient<IProductDomainSerivce, ProductDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<ISellerDomainService, SellerDomainService>();

        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);
    }
}
