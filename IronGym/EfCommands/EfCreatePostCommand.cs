using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfCreatePostCommand : EfBaseCommand, ICreatePostCommand
    {
        private readonly IEmailSender _emailSender;
        public EfCreatePostCommand(IronContext context,IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

        public void Execute(CreatePostDto request)
        {
            var post = Context.Posts;
            if (post.Any(p => p.Heading.ToLower() == request.Heading.ToLower()))
                throw new EntityAlreadyExistsException("Post with that heading");
            if (!Context.Users.Any(u => u.Id == request.UserId))
                throw new EntityNotfoundException("User");
            
            post.Add(new Post
            {
                Heading=request.Heading,
                Text=request.Text,
                Picture=request.Picture,
                UserId=request.UserId,
                IsDeleted=false
            });
            Context.SaveChanges();
            var email = Context.Posts.Where(p => p.UserId == request.UserId).Select(u => u.User.Email).First().ToString();
            _emailSender.Subject = "Successful post inserting";
            _emailSender.Body = "You have successfully created a post with a heading '"+request.Heading+"'";
            _emailSender.ToEmail=email;
            _emailSender.Send();
        }
    }
}
