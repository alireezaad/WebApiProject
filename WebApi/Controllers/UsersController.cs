using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Model.DTOs;
using WebApi.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _services;

        public UsersController(IUserServices twittRepository)
        {
            _services = twittRepository;
        }
        // GET: api/<TwittController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var task = Task.Run(() => _services.GetAll());
                var list = await task;
                //var listT = new List<User>();
                return Ok(list.ToList());
                //
                //(list.Any())
                //{
                //    return Ok(list);
                //}
                
                //return NotFound();

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/<TwittController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // var twitt = db.Get(id);
                //return Ok(twitt);
                return Ok(222);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        // POST api/<TwittController>
        //[HttpPost("{userDTO}")]
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            try
            {
                _services.Add(userDTO);
                _services.SaveChanges();
                //return CreatedAtAction(nameof(Get),"Twitt", new { Id = twitt.Id }, twitt);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/<TwittController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserDTO userDTO)
        {
            try
            {
                _services.Update(userDTO);
                _services.SaveChanges();
                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE api/<TwittController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                _services.Delete(id);
                _services.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] UserDTO userDTO)
        {
            try
            {

                _services.Delete(userDTO);
                _services.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
