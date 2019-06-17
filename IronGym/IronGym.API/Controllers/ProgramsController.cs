using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using System.IO;
using IronGym.API.DataTransfer;
using Application.Helpers;
using Application.Searches;

namespace IronGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly ICreateProgramCommand _createProgram;
        private readonly IGetProgramsCommand _getAll;
        private readonly IGetProgramCommand _getOne;
        private readonly IEditProgramCommand _editProgram;
        private readonly IDeleteProgramCommand _deleteProgram;

        public ProgramsController(ICreateProgramCommand createProgram, IGetProgramsCommand getAll, IGetProgramCommand getOne, IEditProgramCommand editProgram, IDeleteProgramCommand deleteProgram)
        {
            _createProgram = createProgram;
            _getAll = getAll;
            _getOne = getOne;
            _editProgram = editProgram;
            _deleteProgram = deleteProgram;
        }
        /// <summary>
        /// Returns all orders that match provided query
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get from query /Todo
        ///     {
        ///        "id": 1,
        ///        "keyword": "nesto",
        ///        "OnlyActive":true,
        ///        "perPage":1,
        ///        "PageNumber":1
        ///        
        /// 
        ///     }
        ///
        /// </remarks>
        // GET: api/Programs
        [HttpGet]
        public ActionResult Get([FromQuery] ProgramSearch src)
        {
            try
            {
                var programs = _getAll.Execute(src);
                return Ok(programs);
            }
            catch (EntityNotfoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var program = _getOne.Execute(id);
                return StatusCode(200, program);
            }
            catch (EntityNotfoundException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Creates new training program
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST From Form /Todo
        ///     {
        ///        "heading": "nesto",
        ///        "Text":"post text",
        ///        "Picture":upload a picture
        ///     }
        ///
        /// </remarks>
        // POST: api/Programs
        [HttpPost]
        public ActionResult Post([FromForm] ProgramDto p)
        {

            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {

                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "program", newFileName);

                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreateProgramDto
                {
                    Heading=p.Heading,
                    Text=p.Text,
                    Picture=newFileName,
                };
                _createProgram.Execute(dto);
                return StatusCode(201,"Program has been created.");
            }
            catch (EntityAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Updates a single training program
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT From Form /Todo
        ///     {
        ///        "heading": "nesto",
        ///        "Text":"post text",
        ///        "Picture":upload a picture
        ///     }
        ///
        /// </remarks>
        // PUT: api/Programs/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] ProgramDto p)
        {
            p.Id=id;
            var ext = Path.GetExtension(p.Picture.FileName);

            if (!PictureUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {

                var newFileName = Guid.NewGuid().ToString() + "_" + p.Picture.FileName.ToString();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "program", newFileName);

                p.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new CreateProgramDto
                {
                    Id = p.Id,
                    Heading = p.Heading,
                    Text = p.Text,
                    Picture = newFileName,
                    IsDeleted = false
                    
                };
                _editProgram.Execute(dto);
                return StatusCode(204, "Program has been updated.");
            }
            catch (EntityAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteProgram.Execute(id);
                return StatusCode(204);
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
