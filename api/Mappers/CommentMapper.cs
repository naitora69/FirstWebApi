using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Comments;
using api.Models;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Context = commentModel.Context,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateDto commentModel, int stockid)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Context = commentModel.Context,
                StockId = stockid
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateRequestDto commentModel)
        {
             return new Comment
            {
                Title = commentModel.Title,
                Context = commentModel.Context,
            };
        }

        
    }
}