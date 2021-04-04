using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IPointsService
    {
        public Guid AddNewList(List<Point> points);
        public void AddPoint(int listId);
        public void RemovePoint(int listId);
        public List<Point> GetPoints(int listId);
    }
}
