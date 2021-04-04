using System;
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
        private readonly ISquaresService _squaresService;
        private readonly ILogger<SquaresController> _logger;

        public SquaresController(ILogger<SquaresController> logger, IPointsService pointsService)
        {
            _pointsService = pointsService;
            _logger = logger;
        }

        [HttpGet("squares")]
        public IEnumerable<Square> GetSquares(Guid listId)
        {
            List<Square> squares = new();

            List<Point> points = _pointsService.GetPoints(listId).Select(Map).ToList();
            List<Models.Square> modelSquares = _squaresService.GetSquares(points.Select(Map).ToList());
            squares.AddRange(modelSquares.Select(Map).ToList());

            return squares;
        }

        private Point Map(Models.Point modelPoint)
        {
            return new Point(modelPoint.X, modelPoint.Y);
        }

        private Models.Point Map(Point contractPoint)
        {
            return new Models.Point(contractPoint.X, contractPoint.Y);
        }

        private Square Map(Models.Square modelSquare)
        {
            List<Point> points = new List<Point>();
            foreach (Models.Point modelPoint in modelSquare.Points)
            {
                points.Add(Map(modelPoint));
            }

            return new Square { Points = points };
        }
    }
}
