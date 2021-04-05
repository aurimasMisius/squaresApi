using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using SquaresApi.Contracts;
using SquaresApi.Documentation;
using Swashbuckle.AspNetCore.Filters;

namespace SquaresApi.Controllers
{
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;
        private readonly ISquaresService _squaresService;
        private readonly ILogger<PointsController> _logger;

        public PointsController(ILogger<PointsController> logger, IPointsService pointsService, ISquaresService squaresService)
        {
            _logger = logger;
            _pointsService = pointsService;
            _squaresService = squaresService;
        }

        [HttpPut("points")]
        [SwaggerRequestExample(typeof(List<Point>), typeof(PointListExample))]
        public async Task<Guid> AddPointsList([FromBody] List<Point> points)
        {
            List<Models.Point> modelPoints = points.Select(Map).ToList();
            Guid listId = await _pointsService.AddNewList(modelPoints).ConfigureAwait(false);
            _squaresService.CalculateSquares(modelPoints, listId).ConfigureAwait(false);

            return listId;
        }

        [HttpPut("points/{listId}")]
        public async Task AddPoint(Guid listId, Point point)
        {
            await _pointsService.AddPoint(listId, Map(point)).ConfigureAwait(false);
        }

        [HttpDelete("points/{listId}")]
        public async Task RemovePoint(Guid listId, Point point)
        {
            await _pointsService.RemovePoint(listId, Map(point)).ConfigureAwait(false);
        }

        private Models.Point Map(Point contractPoint)
        {
            return new Models.Point(contractPoint.X, contractPoint.Y);
        }
    }
}
