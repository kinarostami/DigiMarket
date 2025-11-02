using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CommentAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SiteEntities.Repository;
using Shop.Domain.UserAgg.Repsitory;
using Shop.Infrastructure._Utilities.MediatR;
using Shop.Infrastucture.Persistent.Dapper;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Infrastucture.Persistent.Ef.CategoryAgg;
using Shop.Infrastucture.Persistent.Ef.CommentAgg;
using Shop.Infrastucture.Persistent.Ef.OrderAgg;
using Shop.Infrastucture.Persistent.Ef.ProductAgg;
using Shop.Infrastucture.Persistent.Ef.RoleAgg;
using Shop.Infrastucture.Persistent.Ef.SellerAgg;
using Shop.Infrastucture.Persistent.Ef.SiteEntitiesAgg.Repositories;
using Shop.Infrastucture.Persistent.Ef.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture;

public class InfrstuctureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICommentRepositrory, CommentRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<ISellerRepository, SellerRepository>();
        services.AddTransient<IBannerRepository, BannerRepository>();
        services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
        services.AddTransient<ISliderRepository, SliderRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddSingleton<ICustomPublisher, CustomPublisher>();

        services.AddTransient(_ => new DapperContext(connectionString));
        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);  
        });
    }
}
