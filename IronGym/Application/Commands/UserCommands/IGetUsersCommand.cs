using Application.DataTransfer;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface IGetUsersCommand : ICommand<UserSearch,PagedResponse<GetUsersDto>>
    {
    }
}
