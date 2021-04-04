﻿using System.Collections.Generic;

namespace Models
{
    public class Square
    {
        public List<Point> Points { get; }

        public Square(Point p1, Point p2, Point p3, Point p4)
        {
            Points = new List<Point> {p1, p2, p3, p4};
        }
    }
}
