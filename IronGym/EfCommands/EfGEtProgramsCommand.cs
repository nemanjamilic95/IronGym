using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetProgramsCommand : EfBaseCommand, IGetProgramsCommand
    {
        public EfGetProgramsCommand(IronContext context) : base(context)
        {
        }

        public PagedResponse<GetProgramDto> Execute(ProgramSearch request)
        {
            var query = Context.Programs.AsQueryable();

            if (request.Keyword != null)
            {
                if (!Context.Programs.Any(c=>c.Heading.ToLower().Contains(request.Keyword.ToLower()) || c.Text.ToLower().Contains(request.Keyword.ToLower())))
                {
                    throw new EntityNotfoundException("Program");
                }
               
                query = query.Where(c => c.Heading.ToLower().Contains(request.Keyword.ToLower()) || c.Text.ToLower().Contains(request.Keyword.ToLower()));
            }
            if (request.OnlyActive.HasValue)
            {
                query = query.Where(c => c.IsDeleted == false);
            }

            var totalCount = query.Count();
            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PagedResponse<GetProgramDto>
            {
                TotalCount = totalCount,
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                Data = query.Select(p => new GetProgramDto
                {
                    Id = p.Id,
                    Heading = p.Heading,
                    Picture = p.Picture,
                    Text = p.Text
                })

            };
            return response;
        }
    }
}
