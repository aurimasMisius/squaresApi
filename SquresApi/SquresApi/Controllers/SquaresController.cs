using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using SquaresApi.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SquaresApi.Controllers
{
    [ApiController]
    public class SquaresController : ControllerBase
    {
        private readonly IPointsService _pointsService;
        private readonly ILogger<SquaresController> _logger;

        public SquaresController(ILogger<SquaresController> logger, IPointsService pointsService)
        {
            _pointsService = pointsService;
            _logger = logger;
        }

        [HttpGet("squares")]
        public IEnumerable<Square> GetSquares(int listId)
        {
            List<Square> squares = new();

            List<Point> points = _pointsService.GetPoints(listId).Select(Map).ToList();

            return squares;
        }

        private Point Map(Models.Point modelPoint)
        {
            return new Point(modelPoint.X, modelPoint.Y);
        }
    }
}
