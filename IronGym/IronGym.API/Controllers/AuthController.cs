using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Encryption _enc;
        private readonly IAuthorizeCommand _command;

        public AuthController(Encryption enc, IAuthorizeCommand command)
        {
            _enc = enc;
            _command = command;
        }



        // POST: api/Auth
        [HttpPost]
        public ActionResult Post([FromBody]LoginDto login)
        {
            try
            {

                var logged = _command.Execute(login);

                var user = new LoggedUser
                {
                    FirstName = logged.FirstName,
                    LastName = logged.LastName,
                    Id = logged.Id,
                    Role = logged.RoleName,
                    Username = logged.Username
                };

                var stringObjekat = JsonConvert.SerializeObject(user);

                var encrypted = _enc.EncryptString(stringObjekat);

                return Ok(new { token = encrypted });
            }
            catch (EntityNotfoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured. Try again later");
            }
            
        }
        [HttpGet("decode")]
        public ActionResult Decode(string value)
        {
            var decoded = _enc.DecryptString(value);
            decoded = decoded.Substring(0, decoded.LastIndexOf("}") + 1);
            var user = JsonConvert.DeserializeObject<LoggedUser>(decoded);
            return null;
        }
    }
}
