using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkgroupTestApi.Models;

namespace WorkgroupTestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class KeyValueController : ControllerBase
    {
        private IRepository _repository;

		//public KeyValueController(IRepository repository)
		//{
		//	_repository = repository;
		//}

		public KeyValueController()
		{
			_repository = new Repository();
		}

		// GET api/keyvalue/{guid}
		[HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string key)
        {
            try
            {
                var value = _repository.Get(key);
                if (value == null)
                {
                    return NotFound("Value Not Found");
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // POST api/keyvalue
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<KeyValue> Create([FromBody]KeyValue kv)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kv?.Key))
                {
                    return BadRequest();
                }
                var response = _repository.Create(kv);

                if (response)
                {
                    return new CreatedAtActionResult(nameof(Get), "KeyValue", new { id = kv.Key }, "Key Added Succesfully");
                }
				else
				{
                    return Ok("Key Already exist, Value Updated");
                }

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // PUT api/keyvalue/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValue))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArticle([FromBody] KeyValue kv)
        {
            try
            {
                // assume id is valid guid
                if (string.IsNullOrWhiteSpace(kv?.Key))
                {
                    return BadRequest();
                }
                var result = _repository.Update(kv);
                if (result == false)
                {
                    return NotFound("Article not found");
                }

                return Ok("Value Updated");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE api/keyvalue/{guid}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValue))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArticle(string key)
        {
            try
            {
                var result = _repository.Delete(key);
                if (result == false)
                {
                    return NotFound("Article Not Found");
                }

                return Ok("Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }

}
