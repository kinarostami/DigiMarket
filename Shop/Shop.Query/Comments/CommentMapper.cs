using Shop.Domain.CommentAgg;
using Shop.Query.Comments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Comments;

public static class CommentMapper
{
    public static CommentDto? Map(this Comment comment)
    {
        if (comment == null)
            return null;

        return new CommentDto()
        {
            Id = comment.Id,
            ProductId = comment.ProductId,
            UserId = comment.UserId,
            Status = comment.Status,
            CreationDate = comment.CreationDate,
            Text = comment.Text,
        };
    }

    public static CommentDto MapFilterComment(this Comment comment)
    {
        if (comment == null)
            return null;

        return new CommentDto()
        {
            Id = comment.Id,
            ProductId = comment.ProductId,
            UserId = comment.UserId,
            Text = comment.Text,
            Status = comment.Status,
            CreationDate = comment.CreationDate
        };
    }
}
