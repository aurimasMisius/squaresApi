using SquaresApi.Contracts;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace SquaresApi.Documentation
{
    public class PointListExample : IExamplesProvider<List<Point>>
    {
        public List<Point> GetExamples()
        {
            return new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(1, 0)
            };
        }
    }
}
