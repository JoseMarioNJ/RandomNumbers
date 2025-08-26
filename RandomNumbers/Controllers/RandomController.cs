using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace RandomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly Random _random = new Random();

        // GET /api/random/number
        [HttpGet("number")]
        public IActionResult GetRandomNumber([FromQuery] int? min, [FromQuery] int? max)
        {
            if (min.HasValue && max.HasValue)
            {
                if (min > max)
                {
                    return BadRequest(new { error = "min no puede ser mayor que max" });
                }
                int value = _random.Next(min.Value, max.Value + 1);
                return Ok(new { result = value });
            }
            else
            {
                int value = _random.Next();
                return Ok(new { result = value });
            }
        }

        // GET /api/random/decimal
        [HttpGet("decimal")]
        public IActionResult GetRandomDecimal()
        {
            double value = _random.NextDouble();
            return Ok(new { result = value });
        }

        // GET /api/random/string?length=8
        [HttpGet("string")]
        public IActionResult GetRandomString([FromQuery] int length = 8)
        {
            if (length < 1 || length > 1024)
            {
                return BadRequest(new { error = "length debe estar entre 1 y 1024" });
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[_random.Next(chars.Length)]);
            }

            return Ok(new { result = sb.ToString() });
        }

        // POST /api/random/custom
        [HttpPost("custom")]
        public IActionResult GetCustomRandom([FromBody] CustomRandomRequest request)
        {
            if (request.Type == "number")
            {
                if (request.Min.HasValue && request.Max.HasValue)
                {
                    if (request.Min > request.Max)
                        return BadRequest(new { error = "min no puede ser mayor que max" });

                    int value = _random.Next(request.Min.Value, request.Max.Value + 1);
                    return Ok(new { result = value });
                }
                else
                {
                    return BadRequest(new { error = "Se requieren min y max para type=number" });
                }
            }
            else if (request.Type == "decimal")
            {
                int decimals = request.Decimals ?? 2;
                double value = Math.Round(_random.NextDouble(), decimals);
                return Ok(new { result = value });
            }
            else if (request.Type == "string")
            {
                int length = request.Length ?? 8;
                if (length < 1 || length > 1024)
                {
                    return BadRequest(new { error = "length debe estar entre 1 y 1024" });
                }

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    sb.Append(chars[_random.Next(chars.Length)]);
                }
                return Ok(new { result = sb.ToString() });
            }
            else
            {
                return BadRequest(new { error = "type debe ser 'number', 'decimal' o 'string'" });
            }
        }
    }

    // Modelo para el POST /random/custom
    public class CustomRandomRequest
    {
        public string Type { get; set; } = string.Empty;
        public int? Min { get; set; }
        public int? Max { get; set; }
        public int? Decimals { get; set; }
        public int? Length { get; set; }
    }
}




