using System;
using System.Collections.Generic;
using Mongo;

namespace Models
{
    public class SquaresMetadata : BaseDocument
    {
        public int Count { get; set; }
        public List<Square> Squares { get; set; }
    }
}
