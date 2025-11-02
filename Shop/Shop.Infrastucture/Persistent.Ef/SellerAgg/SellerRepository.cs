using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastucture._Utilties;
using Shop.Infrastucture.Persistent.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.SellerAgg;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{

    private readonly DapperContext _dapperContext;
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }
    //public async Task<SellerInventory?> GetInventoryBy(long id)
    //{
    //    return await Context.Inventories.Where(r => r.Id == id)
    //        .Select(i => new InventoryResult()
    //        {
    //            Count = i.Count,
    //            Id = i.Id,
    //            Price = i.Price,
    //            ProductId = i.ProductId,
    //            SellerId = i.SellerId
    //        }).FirstOrDefaultAsync();
    //}
    public async Task<InventoryResult?> GetInventoryBy(long id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.Inventories} where Id=@inventoryId";
        return await connection.QueryFirstOrDefaultAsync<InventoryResult>
            (sql, new { InventoryId = id });
    }
}
