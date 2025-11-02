using Common.Domain.Repository;
using Shop.Domain.CommentAgg;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.CommentAgg;

public class CommentRepository : BaseRepository<Comment>, ICommentRepositrory
{
    public CommentRepository(ShopContext context) : base(context)
    {
    }

    public Task DeleteAndSave(Comment comment)
    {
        Context.Remove(comment);
        return Context.SaveChangesAsync();
    }
}
