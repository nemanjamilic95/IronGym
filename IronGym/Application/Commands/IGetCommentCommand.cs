using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface IGetCommentCommand : ICommand<int,GetCommentsDto>
    {
    }
}
