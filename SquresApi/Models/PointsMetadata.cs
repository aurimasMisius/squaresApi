using System;
using System.Collections.Generic;
using Mongo;

namespace Models
{
    public class PointsMetadata : BaseDocument
    {
        public List<Point> Points { get; set; }
    }
}
