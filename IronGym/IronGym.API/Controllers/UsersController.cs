using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using IronGym.API.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected readonly IGetUsersCommand _getUsers;
        protected readonly IGetUserCommand _getUser;
        protected readonly ICreateUserCommand _createUser;
        protected readonly IEditUserCommand _editUser;
        protected readonly IDeleteUserCommand _deleteUser;

        public UsersController(IGetUsersCommand getUsers, IGetUserCommand getUser, ICreateUserCommand createUser, IEditUserCommand editUser, IDeleteUserCommand deleteUser)
        {
            _getUsers = getUsers;
            _getUser = getUser;
            _createUser = createUser;
            _editUser = editUser;
            _deleteUser = deleteUser;
        }
        // GET: api/Users
        [HttpGet]
        public ActionResult Get([FromQuery]UserSearch search)
        {
            try
            {
                var users = _getUsers.Execute(search);
                return Ok(users);
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                return Ok(user);
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

        // POST: api/Users
        [HttpPost]
        public ActionResult Post([FromForm] UserDto u)
        {

            var ext = Path.GetExtension(u.Avatar.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
             var newFileName = Guid.NewGuid().ToString() + "_" + u.Avatar.FileName.ToString();

             var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatar", newFileName);

             u.Avatar.CopyTo(new FileStream(filePath, FileMode.Create));

             var dto = new CreateUserDto
             {
                        Username=u.Username,
                        Password=u.Password,
                        Email=u.Email,
                        FirstName=u.FirstName,
                        LastName=u.LastName,
                        Avatar=newFileName,
                        IsDeleted=false,
                        RoleId=u.RoleId
              };
                _createUser.Execute(dto);
                return StatusCode(201);
            }
            catch(EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] UserDto u)
        {
            u.Id = id;


            var ext = Path.GetExtension(u.Avatar.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {

                var newFileName = Guid.NewGuid().ToString() + "_" + u.Avatar.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatar", newFileName);

                u.Avatar.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreateUserDto
                {
                    Id=u.Id,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = newFileName,
                    IsDeleted = false,
                    RoleId = u.RoleId
                };


                _editUser.Execute(dto);
                return StatusCode(204, "User was successfuly updated.");
            }
            catch (EntityNotfoundException e)
            {
                if (e.Message == "User doesn't exist")
                {
                    return NotFound(e.Message);
                }
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured.Please try again later.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return StatusCode(204,"You deleted user with id="+id);
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
