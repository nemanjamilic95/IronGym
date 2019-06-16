using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetRolesDto
    {
        public GetRolesDto()
        {
            Users = new List<RoleUserDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleUserDto> Users { get; set; }
    }
}
