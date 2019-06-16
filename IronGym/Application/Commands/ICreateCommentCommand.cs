using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ICreateCommentCommand : ICommand<CreateCommentDto>
    {
    }
}
