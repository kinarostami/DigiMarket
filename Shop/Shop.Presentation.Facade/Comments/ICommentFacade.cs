using Common.Application;
using MediatR;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Categories.GetById;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Comments;

public interface ICommentFacade
{
    Task<OperationResult> Create(CreateCommentCommand command);
    Task<OperationResult> Edit(EditCommentCommand command);
    Task<OperationResult> ChangeStatus(ChangeCommentCommand command);

    Task<CommentDto> GetCommentById(long commentId);
    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams);
}

public class CommentFacade : ICommentFacade
{
    private readonly IMediator _mediator;

    public CommentFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> ChangeStatus(ChangeCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams)
    {
        return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
    }

    public async Task<CommentDto> GetCommentById(long commentId)
    {
        return await _mediator.Send(new GetCommentByIdQuery(commentId));
    }
}
