using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Comments.Create;
using Shop.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommand : IBaseCommand
{
    public long Id { get; set; }
    public string Text { get; set; }
}
public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
{
    private readonly ICommentRepositrory _commentRepositrory;

    public EditCommentCommandHandler(ICommentRepositrory commentRepositrory)
    {
        _commentRepositrory = commentRepositrory;
    }

    public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepositrory.GetTracking(request.Id);
        if (comment == null)
            return OperationResult.NotFound();

        comment.Edit(request.Text);
        await _commentRepositrory.Save();
        return OperationResult.Success();
    }
}
public class EditCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(x => x.Text).NotNull()
            .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
    }
}
