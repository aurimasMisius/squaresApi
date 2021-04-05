using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Point = SquaresApi.Contracts.Point;
using Square = SquaresApi.Contracts.Square;

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
        public async Task<IEnumerable<Square>> GetSquares(Guid listId)
        {
            List<Square> squares = new();
            SquaresMetadata squaresMetadata = await _squaresService.GetSquares(listId).ConfigureAwait(false);
            squares.AddRange(squaresMetadata.Squares.Select(Map).ToList());

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
