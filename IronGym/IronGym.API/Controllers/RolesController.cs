using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using IronGym.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGetRolesCommand _getRoles;
        private readonly IGetRoleCommand _getOne;
        private readonly ICreateRoleCommand _createRole;
        private readonly IEditRoleCommand _editRole;
        private readonly IDeleteRoleCommand _deleteRole;

        public RolesController(IGetRolesCommand getRoles, IGetRoleCommand getOne, ICreateRoleCommand createRole, IEditRoleCommand editRole, IDeleteRoleCommand deleteRole)
        {
            _getRoles = getRoles;
            _getOne = getOne;
            _createRole = createRole;
            _editRole = editRole;
            _deleteRole = deleteRole;
        }
        // GET: api/Roles
        //[LoggedIn("Admin")]
        [HttpGet]
        public ActionResult Get([FromQuery] RoleSearch search)
        {
            try
            {
                var roles = _getRoles.Execute(search);
                return Ok(roles);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured.Please try again later.");
            }
        }

        // GET: api/Roles/5
        //[LoggedIn("Admin")]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var role = _getOne.Execute(id);
                return Ok(role);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Please try again later.");
            }
        }

        // POST: api/Roles
        //[LoggedIn("Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] CreateRoleDto dto)
        {
            try
            {
                _createRole.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Roles/5
        //[LoggedIn("Admin")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CreateRoleDto dto)
        {
            dto.Id = id;
            try
            {
                _editRole.Execute(dto);
                return StatusCode(200,"Role was successfuly edited.");
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        //[LoggedIn("Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteRole.Execute(id);
                return StatusCode(204, "You deleted role with id=" + id);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured.Please try again later.");
            }
        }
    }
}
