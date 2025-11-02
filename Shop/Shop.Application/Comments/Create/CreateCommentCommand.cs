using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.Create;

public class CreateCommentCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string Text { get; set; }
}

public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
{
    private readonly ICommentRepositrory _commentRepositrory;

    public CreateCommentCommandHandler(ICommentRepositrory commentRepositrory)
    {
        _commentRepositrory = commentRepositrory;
    }

    public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment(request.UserId,request.ProductId,request.Text);
        _commentRepositrory.Add(comment);
        await _commentRepositrory.Save();
        return OperationResult.Success();   
    }
}
public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Text).NotNull()
            .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
    }
}
