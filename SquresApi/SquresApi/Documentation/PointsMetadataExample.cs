using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace SquaresApi.Documentation
{
    public class PointsMetadataExample : IExamplesProvider<PointsMetadata>
    {
        public PointsMetadata GetExamples()
        {
            return new PointsMetadata
            {
                Points = new List<Point>
                {
                    new Point(0, 0),
                    new Point(0, 1),
                    new Point(1, 1),
                    new Point(1, 0)
                }
            };
        }
    }
}
