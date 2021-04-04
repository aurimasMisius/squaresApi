using System;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class PointsService : IPointsService
    {
        public PointsService()
        {
        }
        
        public Guid AddNewList(List<Point> points)
        {
            Guid id = new Guid();
            return id;
        }
        
        public void AddPoint(int listId)
        {
            List<Point> points = GetPoints(listId);
        }
        
        public void RemovePoint(int listId)
        {
            List<Point> points = GetPoints(listId);
        }

        public List<Point> GetPoints(int listId)
        {
            List<Point> points = new();
            return points;
        }
    }
}