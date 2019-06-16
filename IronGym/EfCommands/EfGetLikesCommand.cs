using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetLikesCommand : EfBaseCommand, IGetLikesCommand
    {
        public EfGetLikesCommand(IronContext context) : base(context)
        {
        }

        public GetLikeDto Execute(int request)
        {
           var likes = Context.Likes.Include(q => q.Post).Where(q => q.PostId == request).First();
           

            if (likes == null)
                throw new EntityNotfoundException("Likes for that post");

            var likesPerPost= Context.Likes.Include(q => q.Post).Where(q => q.PostId == request).Count();

            return new GetLikeDto
            {
                PostId=likes.PostId,
                PostName=likes.Post.Heading,
                LikesPerPost=likesPerPost
            };

        }
    }
}
