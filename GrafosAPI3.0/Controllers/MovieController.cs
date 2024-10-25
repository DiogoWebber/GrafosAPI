using Microsoft.AspNetCore.Mvc;
using GrafosAPI3._0.Services;
using System.Collections.Generic;
using GrafosAPI3._0.Models;

namespace GrafosAPI3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGraphController : ControllerBase
    {
        private readonly GraphService _graphService;

        public MovieGraphController()
        {
            _graphService = new GraphService();
        }

        [HttpGet("bfsAllPaths")]
        public ActionResult<List<string>> GetAllPaths([FromQuery] string actor1, [FromQuery] string actor2)
        {
            if (string.IsNullOrEmpty(actor1) || string.IsNullOrEmpty(actor2))
            {
                return BadRequest("Actor names cannot be empty.");
            }

            var paths = _graphService.bfsAllPaths(actor1, actor2);

            if (paths.Count == 0)
            {
                return NotFound($"No relationship found between {actor1} and {actor2}.");
            }

            return Ok(paths);
        }
        
        [HttpGet("shortestPath")]
        public ActionResult<List<PathResult>> GetShortestPath([FromQuery] string actor1, [FromQuery] string actor2)
        {
            if (string.IsNullOrEmpty(actor1) || string.IsNullOrEmpty(actor2))
            {
                return BadRequest("Actor names cannot be empty.");
            }

            var pathResults = _graphService.bfsShortestPath(actor1, actor2);

            if (pathResults.Count == 0)
            {
                return NotFound($"No relationship found between {actor1} and {actor2}.");
            }

            return Ok(pathResults);
        }

    }
}