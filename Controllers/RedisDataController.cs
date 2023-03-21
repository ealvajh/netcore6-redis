using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace redisApp.Controllers;

[ApiController]
[Route("RedisData")]
public class RedisDataController : ControllerBase
{
    private readonly ILogger<RedisDataController> _logger;
    private readonly IRedisHandler _redisHandler;

    public RedisDataController(
        ILogger<RedisDataController> logger, 
        IRedisHandler redisHandler)
    {
        _logger = logger;
        _redisHandler = redisHandler;
    }

    [HttpGet("Get/{key}")]
    public IActionResult Get([FromRoute] string key)
    {
        var value = _redisHandler.GetByKey(key);

        if(string.IsNullOrEmpty(value))
            return NotFound();

        return Ok(value);
    }

    [HttpPost("Post")]
    public IActionResult Post(string key, string value)
    {
        var dateTime = DateTimeOffset.Now.AddSeconds(30);

        var save = _redisHandler.SetValue(key, value, dateTime);

        if(!save)
            return BadRequest();

        return Ok(save);
    }

    [HttpPut("Delete")]
    public IActionResult Delete(string key)
    {
        var delete = _redisHandler.RemoveValue(key);

        if(!delete)
            return NotFound();

        return Ok(delete);
    }
}
