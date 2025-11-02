using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repsitory;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.UserAgg;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShopContext context) : base(context)
    {
    }
}
