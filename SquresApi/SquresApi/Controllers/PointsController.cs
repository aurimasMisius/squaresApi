using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using SquaresApi.Contracts;

namespace SquaresApi.Controllers
{
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;
        private readonly ILogger<PointsController> _logger;

        public PointsController(ILogger<PointsController> logger, IPointsService pointsService)
        {
            _logger = logger;
            _pointsService = pointsService;
        }

        [HttpPut("points")]
        public Task<Guid> AddPointsList([FromBody] List<Point> points)
        {
            List<Models.Point> modelPoints = points.Select(Map).ToList();

            return Task.FromResult(_pointsService.AddNewList(modelPoints));
        }

        [HttpPut("points/{listId}")]
        public void AddPoint(int listId)
        {
        }

        [HttpDelete("points/{listId}")]
        public void RemovePoint(int listId)
        {
        }

        private Models.Point Map(Point contractPoint)
        {
            return new Models.Point(contractPoint.X, contractPoint.Y);
        }
    }
}
