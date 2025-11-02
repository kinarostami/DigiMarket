using Common.Application;
using Shop.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.ChangeStatus;

public record ChangeCommentCommand(long CommentId,CommentStatus Status) : IBaseCommand
{
}
public class ChangeCommentCommandHandler : IBaseCommandHandler<ChangeCommentCommand>
{
    private readonly ICommentRepositrory _commentRepositrory;

    public ChangeCommentCommandHandler(ICommentRepositrory commentRepositrory)
    {
        _commentRepositrory = commentRepositrory;
    }

    public async Task<OperationResult> Handle(ChangeCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepositrory.GetTracking(request.CommentId);
        if (comment == null)
            return OperationResult.NotFound();

        comment.ChangeStatus(request.Status);
        await _commentRepositrory.Save();
        return OperationResult.Success();
    }
}
