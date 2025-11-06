using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api.Controllers;

public class CommentController : ApiController
{
    private readonly ICommentFacade _commentFacade;

    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }

    [HttpGet]
    public async Task<ApiResult<CommentFilterResult>> GetCommentByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetCommentByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("productComments")]
    public async Task<ApiResult<CommentFilterResult>> GetProductComments(int pageId = 1,int take = 10,int productId = 1)
    {
        var result = await _commentFacade.GetCommentByFilter(new CommentFilterParams()
        {
            ProductId = productId,
            Take = take,
            PageId = pageId,
            CommentStatus = Domain.CommentAgg.CommentStatus.Accepted
        });
        return QueryResult(result);
    }

    [HttpGet("{productId}")]
    public async Task<ApiResult<CommentDto?>> GetCommentById(long productId)
    {
        var result = await _commentFacade.GetCommentById(productId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _commentFacade.Create(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditComment(EditCommentCommand command)
    {
        var result = await _commentFacade.Edit(command);
        return CommandResult(result);
    }


    [HttpPut("changeStatus")]
    public async Task<ApiResult> ChangeCommentStatus(ChangeCommentCommand command)
    {
        var result = await _commentFacade.ChangeStatus(command);
        return CommandResult(result);
    }
}
