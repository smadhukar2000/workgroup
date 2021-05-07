using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
                if (!Utility.ValidateKey(key))
                {
                    return BadRequest($"Invalid Key - {key}, only alphanumeric, hyphen, period, underscore, tilde allowed");
                }
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
                if (!Utility.ValidateKey(kv.Key))
				{
					return BadRequest($"Invalid Key - {kv.Key}, only alphanumeric, hyphen, period, underscore, tilde allowed");
				}
                if (!Utility.ValidateValue(kv.Value))
                {
                    return BadRequest("Invalid Value, MAX 11024 characters allowed");
                }

                _repository.Create(kv);

				return new CreatedResult(kv.Key, kv);
			}
			catch (Exception ex)
			{
				return new BadRequestObjectResult(ex.Message);
			}
		}

		// PUT api/keyvalue
		[HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValue))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] KeyValue kv)
        {
            try
            {
                if (!Utility.ValidateKey(kv.Key))
                {
                    return BadRequest($"Invalid Key - {kv.Key}, only alphanumeric, hyphen, period, underscore, tilde allowed");
                }
                if (!Utility.ValidateValue(kv.Value))
                {
                    return BadRequest("Invalid Value, MAX 11024 characters allowed");
                }
                var result = _repository.Update(kv);
                if (result == false)
                {
                    return NotFound("Value not found");
                }

                return Ok("Value Updated");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE api/keyvalue/{guid}
        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValue))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string key)
        {
            try
            {
                if (!Utility.ValidateKey(key))
                {
                    return BadRequest($"Invalid Key - {key}, only alphanumeric, hyphen, period, underscore, tilde allowed");
                }
                var result = _repository.Delete(key);
                if (result == false)
                {
                    return NotFound("Value Not Found");
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
